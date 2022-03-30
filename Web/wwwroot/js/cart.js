function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}

function checkCookie() {
    let user = getCookie("username");
    if (user != "") {
        alert("Welcome again " + user);
    } else {
        user = prompt("Please enter your name:", "");
        if (user != "" && user != null) {
            setCookie("username", user, 365);
        }
    }
}




var productIds = [];
if (getCookie("myCookie") != null && getCookie("myCookie") !== "") {
    productIds = [...getCookie("myCookie").split("-")];

}
console.log(productIds)
$(".cart-btn").on("click", function () {
    console.log("")
    const productId = $(this).attr("pro-id");
    productIds.push(productId)
    setCookie("myCookie", productIds.join("-"), 3)
    Swal.fire(
        'Success!',
        'Product added to cart',
        'success'
    )
})


$(".cart-plus-minus .inc").click(function () {

    const productId = $(this).attr("pro-id");
    const quantity = $(this).parent().find("input").val()
    productIds = productIds.filter(p => p !== productId);
    for (let i = 0; i < quantity; i++) {
        productIds.push(productId)
    }
    setCookie("myCookie", productIds.join("-"), 3)

})

$(".cart-plus-minus .dec").click(function () {

    const productId = $(this).attr("pro-id");
    const quantity = $(this).parent().find("input").val()
    productIds = productIds.filter(p => p !== productId);
    for (let i = 0; i < quantity; i++) {
        productIds.push(productId)
    }
    setCookie("myCookie", productIds.join("-"), 3)

})



$(".product-remove").click(function (e) {
    e.preventDefault();
    $(this).parents("tr").remove();
    const productId = $(this).attr("pro-id");
    productIds = productIds.filter(p => p !== productId);
    setCookie("myCookie", productIds.join("-"), 3)
    if (productIds.length == 0) {
        $("#cartTable").addClass("d-none")
        $(".cart-content").append("<p class='alert alert-danger'>Cart is empty!</p>")
    }

})