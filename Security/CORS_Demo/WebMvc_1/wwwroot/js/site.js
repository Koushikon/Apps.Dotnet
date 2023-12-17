

function callLoadCors() {

    console.log("loading Local... load.");

    $.ajax({
        url: "/Home/LoadCors",
        type: "GET",
        data: {},
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            console.log(data);
        }
    });

    console.log("End Local loading... load.");
}

function callLoad() {

    console.log("loading Local... load.");

    $.ajax({
        url: "/Home/Load",
        type: "GET",
        data: {},
        dataType: "json",
        traditional: true,
        contentType: "application/json; charset=utf-8",
        async: false,
        success: function (data) {
            console.log(data);
        }
    });

    console.log("End Local loading... load.");
}