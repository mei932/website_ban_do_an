window.onscroll = function () { myFunction() };

var header = document.getElementById("myHeader");

function myFunction() {
    if (document.body.scrollTop > 80 ||
        document.documentElement.scrollTop > 80) {
        header.classList.add("sticky");
    } else {
        header.classList.remove("sticky");
    }
}