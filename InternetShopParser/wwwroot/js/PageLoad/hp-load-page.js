function RegisterPageEvents(pageId, callback) {
    $(document).delegate('#' + pageId, "pageshow", callback);
};

function RegisterPageEventsLoad(pageId, callback) {
    $(document).delegate('#' + pageId, "pageload", callback);
};

RegisterPageEvents('CurrentProduct', function () {
    $.getScript('/js/Product/ProductView.js', function () {
        productPage = new ProductPage.ProductView({ viewName: 'Product', pageId: 'CurrentProduct', viewInstanceVariableName: 'productPage' });
        productPage.pageshow();
    });
});
RegisterPageEvents('ProductAll', function () {
    $.getScript('/js/Product/ProductListView.js', function () {
        productListPage = new ProductListPage.ProductListView({ viewName: 'ProductList', pageId: 'ProductAll', viewInstanceVariableName: 'productListPage' });
        productListPage.pageshow();
    });
});
