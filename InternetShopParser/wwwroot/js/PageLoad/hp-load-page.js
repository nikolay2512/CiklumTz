function RegisterPageEvents(pageId, callback) {
    $(document).delegate('#' + pageId, "pageshow", callback);
};

function RegisterPageEventsLoad(pageId, callback) {
    $(document).delegate('#' + pageId, "pageload", callback);
};

RegisterPageEvents('Login', function () {
    $.getScript('/js/Login/LoginView.js', function () {
        loginPage = new LoginPage.LoginView({ viewName: 'Authorization', pageId: 'Login', viewInstanceVariableName: 'loginPage' });
        loginPage.pageshow();
    });
});

RegisterPageEvents('Article', function () {
    $.getScript('/js/Article/ArticleView.js', function () {
        articlePage = new ArticlePage.ArticleView({ viewName: 'Article', pageId: 'Article', viewInstanceVariableName: 'articlePage' });
        articlePage.pageshow();
    });
});

RegisterPageEvents('ArticleAll', function () {
    $.getScript('/js/Article/ArticleAllView.js', function () {
        articleAllPage = new ArticleAllPage.ArticleAllView({ viewName: 'All', pageId: 'ArticleAll', viewInstanceVariableName: 'articleAllPage' });
        articleAllPage.pageshow();
    });
});

RegisterPageEvents('ArticlePreview', function () {
    $.getScript('/js/Article/ArticlePreview.js', function () {
        articlePreviewPage = new ArticlePreviewPage.ArticlePreviewView({ viewName: 'ArticlePreview', pageId: 'ArticlePreview', viewInstanceVariableName: 'articlePreviewPage' });
        articlePreviewPage.pageshow();
    });
});

RegisterPageEvents('UserAll', function () {
    $.getScript('/js/User/UserAllView.js', function () {
        userAllPage = new UserAllPage.UserAllView({ viewName: 'Users', pageId: 'UserAll', viewInstanceVariableName: 'userAllPage' });
        userAllPage.pageshow();
    });
});



RegisterPageEvents('ContactUs', function () {
    $.getScript('/js/ContactUs/ContactUsView.js', function () {
        userAllPage = new ContactUsPage.ContactUsView({ viewName: 'ContactUs', pageId: 'ContactUs', viewInstanceVariableName: 'contactUsPage' });
        userAllPage.pageshow();
    });
});

//$.widget("ui.tabs", $.ui.tabs, {
//    _createWidget: function (options, element) {
//        var page,
//            delayedCreate,
//            that = this;

//        if ($.mobile.page) {
//            page = $(element)
//                .parents(":jqmData(role='page'),:mobile-page")
//                .first();

//            if (page.length > 0 && !page.hasClass("ui-page-active")) {
//                delayedCreate = this._super;
//                page.one("pagebeforeshow", function () {
//                    delayedCreate.call(that, options, element);
//                });
//            }
//        } else {
//            return this._super();
//        }
//    }
//});