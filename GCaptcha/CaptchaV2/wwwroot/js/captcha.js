function checkResponse(captchaResponse) {

    let userObj = JSON.parse(captchaResponse);

    if (userObj.success) {
        $(`#hfCaptcha`).val(userObj.success);
        $(`#rvCaptcha`).hide();

    } else {
        $(`#hfCaptcha`).val(``);
        $(`#rvCaptcha`).show();
        var error = userObj[`error-codes`][0];
        $(`#rvCaptcha`).html(`RECaptcha error. ` + error);
    }
}

$(function () {
    $(`#btnSubmit`).click(function (event) {
        $(`#rvCaptcha`).hide();
        let data = $(`#hfCaptcha`).val();

        if (!data) {
            $("#rvCaptcha").show();
            event.preventDefault();
        }
    });
});