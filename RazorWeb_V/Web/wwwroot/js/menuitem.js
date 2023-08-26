var dataTable;

$(document).ready(function () {
    dataTable = $(`#dtTable`).DataTable({
        "ajax": {
            "url": "/api/MenuItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            {
                "data": "image",
                "render": function (data) {
                    return `<div class="d-flex justify-content-center">
                                <img src="${(data.length > 0 ? `/images/MenuItems/${data}` : "/images/default-foodpic.png")}" height="50" alt="upload image" />
                            </div>`;
                },
                "width": "10%"
            },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "20%" },
            { "data": "foodType.name", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                                <a href="/Admin/MenuItems/UpSert?id=${data}" class="btn btn-success mx-2" style="cursor: pointer;">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onClick=Delete('/api/MenuItem/${data}') class="btn btn-danger mx-2" style="cursor: pointer;">
                                    <i class="bi bi-trash-fill"></i>
                                </a>
                            </div>`;
                },
                "width": "20%"
            },
        ],
        "width": "100%"
    });
});

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}