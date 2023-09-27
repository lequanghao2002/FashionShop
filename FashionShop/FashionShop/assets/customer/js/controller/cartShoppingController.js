var cart = {
    init: function () {
        this.registerEvent();
        this.loadData();
    },
    registerEvent: function () {
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));

            if (quantity >= 1) {
                cart.updateAll();

                var quantityChange = parseInt($(this).val());
                var totalMoney = quantityChange * price;

                $('#TotalMoney_' + productId).text(numeral(totalMoney).format('0,0') + '₫');
            }
            else {
                $('#quantityProduct_' + productId).val(1);

                var quantityChange = parseInt($(this).val());
                var totalMoney = quantityChange * price;

                $('#TotalMoney_' + productId).text(numeral(totalMoney).format('0,0') + '₫');
            }

            $('#tempTotalMoney').text(numeral(cart.getTempTotalMoney()).format('0,0') + '₫');

        })
        $('.txtQuantity').off('change').on('change', function () {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));

            if (quantity >= 1) {
                cart.updateAll();

                var quantityChange = parseInt($(this).val());
                var totalMoney = quantityChange * price;

                $('#TotalMoney_' + productId).text(numeral(totalMoney).format('0,0') + '₫');
            }
            else {
                $('#quantityProduct_' + productId).val(1);
                var quantityChange = parseInt($(this).val());
                var totalMoney = quantityChange * price;

                $('#TotalMoney_' + productId).text(numeral(totalMoney).format('0,0') + '₫');
            }

            $('#tempTotalMoney').text(numeral(cart.getTempTotalMoney()).format('0,0') + '₫');
        });

    },
    addItem: function (productID) {
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productID: productID
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();
                }
            }
        });
    },
    updateAll: function () {
        var cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductID: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            data: {
                cartData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    //cart.loadData();
                }
            }
        });
    },
    deleteItem: function (productID) {
        $.ajax({
            url: '/ShoppingCart/Delete',
            data: {
                productID: productID
            },
            type: 'POST',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    cart.loadData();

                    $('.txtQuantity').trigger('keyup');
                }
            }
        });
    },
    getTempTotalMoney: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });

        return total;
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        if (item.product.discount > 0) {
                            html += Mustache.render(template, {
                                ProductId: item.productID,
                                Image: item.product.image,
                                Name: item.product.name,
                                Price: item.product.price - (item.product.price * item.product.discount / 100),
                                PriceFormat: numeral(item.product.price - (item.product.price * item.product.discount / 100)).format('0,0'),
                                PriceInitial: numeral(item.product.price).format('0,0'),
                                Quantity: item.quantity,
                                TotalMoney: numeral((item.product.price - (item.product.price * item.product.discount / 100))).format('0,0'),
                            });
                        }
                        else {
                            html += Mustache.render(template, {
                                ProductId: item.productID,
                                Image: item.product.image,
                                Name: item.product.name,
                                Price: item.product.price,
                                PriceFormat: numeral(item.product.price).format('0,0'),
                                PriceInitial: '',
                                Quantity: item.quantity,
                                TotalMoney: numeral(item.quantity * item.product.price).format('0,0'),
                            });
                        }

                    });

                    if (html == '') {
                        $('.cartContent').html('<h2>Chưa có sản phẩm nào trong giỏ hàng, <a href="Home" style="color: #E94F37">bấm vào đây</a> để mua hàng</h2>');
                    }
                    else {
                        $('#cartBody').html(html);
                        $('#tempTotalMoney').text(numeral(cart.getTempTotalMoney()).format('0,0') + '₫');
                    }

                    $('.cartLength').text(data.length);


                    cart.registerEvent();
                }
            }
        })
    }
}

cart.init();