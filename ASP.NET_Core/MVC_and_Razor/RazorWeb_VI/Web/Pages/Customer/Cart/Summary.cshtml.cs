using AppDataAccess.Repository.IRepository;
using AppModels;
using AppUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Security.Claims;

namespace Web.Pages.Customer.Cart;

[BindProperties]
[Authorize]
public class SummaryModel : PageModel
{
	private readonly IUnitOfWork _unitOfWork;

	public SummaryModel(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
		OrderHeader = new OrderHeader();
	}

	public IEnumerable<ShoppingCart> ShoppingCartList { get; set; } = default!;
	public OrderHeader OrderHeader { get; set; }

	public void OnGet()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity!;
		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!;

		if (claim != null)
		{
			ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
				includeProperties: "MenuItem,MenuItem.Category,MenuItem.FoodType");

			foreach (var item in ShoppingCartList)
			{
				OrderHeader.OrderTotal += (item.MenuItem.Price * item.Count);
			}

			ApplicationUser appUser = _unitOfWork.ApplicationUser.GetById(u => u.Id == claim.Value);
			OrderHeader.PickUpName = $"{appUser.FirstName} {appUser.LastName}";
			OrderHeader.PhoneNumber = appUser.PhoneNumber!;
		}
	}

	public IActionResult OnPost()
	{
		var claimsIdentity = (ClaimsIdentity)User.Identity!;
		var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!;

		if (claim != null)
		{
			ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
				includeProperties: "MenuItem");

			foreach (var item in ShoppingCartList)
			{
				OrderHeader.OrderTotal += (item.MenuItem.Price * item.Count);
			}
			OrderHeader.Status = SD.StatusPending;
			OrderHeader.OrderDate = DateTime.Now;
			OrderHeader.UserId = claim.Value;
			OrderHeader.PickUpTime = Convert.ToDateTime($"{OrderHeader.PickUpDate.ToShortDateString()} {OrderHeader.PickUpTime.ToShortTimeString()}");

			// Save Order header data
			_unitOfWork.OrderHeader.Add(OrderHeader);
			_unitOfWork.Save();

			// Saves Users Shopping Cart List data
			foreach (var item in ShoppingCartList)
			{
				OrderDetails orderDetails = new()
				{
					MenuItemId = item.MenuItem.Id,
					OrderId = OrderHeader.Id,
					Name = item.MenuItem.Name,
					Price = item.MenuItem.Price,
					Count = item.Count
				};

				// Save Order Details data
				_unitOfWork.OrderDetails.Add(orderDetails);

				/***
                 * We can remove this save because we are adding data one by one
                 * And if we use the next save under ShoppingCart.RemoveRange() method
                 * 
                 * This will do the both work for us
                 * all the add operation will be happed in single database call
                 */
				//_unitOfWork.Save();   // Don't need now
			}

			// int quantity = ShoppingCartList.ToList().Count; // not need in new code

			/***
			 * Clear out the current order Shopping Cart list
			 * because of later code we're getting all the items in the ShoppingCartList
			 * if we delete all the items here we'll never found any item
			 */
			//_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);				

			#region Stripe Code
			var domain = "https://localhost:7080/";
			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>(),

				/***
			 * With This Configuration it shows:
			 * Product name: "Shopper Food Order"
			 * Price: __
			 * Total Distinct Item: __
			 * Qty: __, _ each
			 */
				/*
			LineItems = new List<SessionLineItemOptions>
			{
					new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = Convert.ToInt64(OrderHeader.OrderTotal * 100),
						Currency = "usd",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = "Shopper Food Order",
							Description = $"Total distinct Item {quantity}"
						}
					},
					Quantity = quantity,

				},
			},
			*/
				PaymentMethodTypes = new List<string>
				{
					"card"
				},
				Mode = "payment",
				SuccessUrl = domain + $"Customer/Cart/OrderConfirmation?id={OrderHeader.Id}",
				CancelUrl = domain + $"Customer/Cart/Index",
			};


			// Add line items
			foreach (var item in ShoppingCartList)
			{
				// With this we're adding every individual items at a time with their name, quantity
				var sessionLineItem = new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = Convert.ToInt64(item.MenuItem.Price * 100),
						Currency = "usd",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = item.MenuItem.Name
						}
					},
					Quantity = item.Count
				};
				options.LineItems.Add(sessionLineItem);
			}
			#endregion

			var service = new SessionService();
			Session session = service.Create(options);

			Response.Headers.Add("Location", session.Url);

			/***
		 * Payment Intent Id for:
		 * This is the id on which payment will be captured
		 * If we want to see anything about the payment or like refund
		 * Here we are storing 2 Ids related to Stripe
		 */

			OrderHeader.SessionId = session.Id;
			OrderHeader.PaymentIntentId = session.PaymentIntentId;
			_unitOfWork.Save();

			return new StatusCodeResult(303);
		}

		return Page();
	}
}
