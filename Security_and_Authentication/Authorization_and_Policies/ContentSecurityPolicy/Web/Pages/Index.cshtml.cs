using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        /***
         * Perfect Scenario:
		 * https://localhost:7019?UserId=1400
		 * If we run the app with above URL our app correctly displays “User Id: 1400” on the page.
		 * This would be a common example if we had a link on our site, or maybe in an email.
		 * 
		 * But what happens if we open the URL:
		 * https://localhost:7019/?UserId=%3Cscript%20type=%22text/javascript%22%3E%20var%20adr%20=%20alert(escape(document.cookie));%20%3C/script%3E
		 * This return us with another alter now imagine instead of alert it could be accessing cookie code
		 * 
		 * Uh oh. The browser has just alerted our cookies. Imagine a malicious actor sent an email to one of
		 * our users pretending to be our site, then included the same link, but instead of alerting the browser,
		 * it did a POST to their own server. The user’s credentials in the cookie are submitted to an attacker,
		 * which can use a browser to log in as that user and perform actions. 
		 */

        // GET: Index
        public void OnGet()
        {
            /***
			 * With "Content-Security-Policy" header above url script will never run
			 * 
			 * The browser did not execute the malicious inline script, because it did not come from our origin.
			 * We have in one quick swoop enhanced our website and prevented an XSS exploit from being possible. 
			 */
            //Response.Headers.Add("Content-Security-Policy", "default-src 'self';");

            // Or
            //Response.Headers.Append("Content-Security-Policy", "default-src 'self';");
            //Response.Headers.Append("Content-Security-Policy", "default-src 'self' 'unsafe-inline';");

            //Response.Headers.Append("Content-Security-Policy", "base-uri 'self'; default-src 'self'; img-src data: https:; object-src 'none'; script-src 'self' 'wasm-unsafe-eval'; style-src 'self'; upgrade-insecure-requests;");

            // Or
            //Response.Headers.Append("Content-Security-Policy", "default-src 'self' http://localhost:58580/ ws://localhost:58580/ wss://localhost:44326/; script-src 'self' 'unsafe-inline'; style-src 'self' 'unsafe-inline'; img-src 'self' 'unsafe-inline'; frame-ancestors 'none'; report-uri /csp-violations");

            Response.Headers.Append("Content-Security-Policy", "default-src 'self' http://localhost:58580/ ws://localhost:58580/ wss://localhost:44326/; script-src 'self' 'unsafe-inline'; style-src 'self' 'unsafe-inline'; img-src 'self' 'unsafe-inline'; frame-ancestors 'none'; report-uri /csp-violations");
        }
    }
}
