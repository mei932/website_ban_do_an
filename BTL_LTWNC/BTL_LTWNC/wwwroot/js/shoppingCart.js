

$(document).ready(function () {
    getTotal();
    $("body").on("click", ".btnAddToCart", function (e) {
        e.preventDefault();

        var id = $(this).data('id');

        var quantity = $("#quantity_value").text();

        $.ajax({
            url: "/Cart/AddToCart",
            type: "POST",
            data: {
                id: id,
                quantity: quantity,
            },
            success: function (data) {
                Swal.fire({
                    icon: 'success',
                    title: 'Thêm giỏ hàng thành công',
                    showConfirmButton: false,
                    timer: 2500
                });
                console.log(data.code)
            },
            error: function () {
                Swal.fire({
                    icon: 'error',
                    title: 'Thêm giỏ hàng thất bại',
                    text: 'Vui lòng thử lại',
                    showConfirmButton: false,
                    timer: 2500
                });
            }
        })

    });

    $("body").on("click", ".btnDeleteFromCart", function (e) {
        e.preventDefault();

        var id = $(this).data('id');

        var cfDelete = confirm("Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng không?");

        if (cfDelete) {
            $.ajax({
                url: "/Cart/Delete",
                type: "POST",
                data: { id: id },
                success: function (res) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Xoá hàng thành công',
                        showConfirmButton: false,
                        timer: 2500
                    });
                    $('#trow_' + id).remove();
                    getTotal();
                },
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Xoá hàng thất bại',
                        showConfirmButton: false,
                        timer: 2500
                    });
                }
            });
        }
    })
})

setInterval(function () {
    $.ajax({
        url: "/cart/showcount",
        type: "GET",
        success: function (res) {
            $("#cart_count").html(res.quantity);
        }
    })
}, 0)

function getTotal() {
    $.ajax({
        url: "/cart/getTotalCart",
        type: "GET",
        success: function (res) {
            $("#cart_total").html(res.subtotal);
        }
    })
}



