var account =  {
    init: function () {
        account.registerEvents();
    },
    registerEvents: function() {
        $('.btnCancelOrder').off('click').on('click', function (e) {
            e.preventDefault();

            var productId = parseInt($(this).data('id'));

            account.cancelOrder(productId);
        });
    },
    cancelOrder: function (productID) {
        $.ajax({
            url: '/Account/OrderCancel',
            data: {
                id: productID
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    location.reload(true);
                }
                else {
                    alert('Lỗi');
                }
            }
        });
    }
}

account.init();
