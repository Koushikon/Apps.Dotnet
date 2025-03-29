

function callLoadCors(rurl) {

    console.log("loading Cross... load.");

    $.ajax({
        url: `${rurl}/Home/LoadCors`,
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

    console.log("End Cross loading... load.");
}

function callLoad(rurl) {

    console.log("loading Cross... load.");

    $.ajax({
        url: `${rurl}/Home/Load`,
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

    console.log("End Local Cross... load.");
}