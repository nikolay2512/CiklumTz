var ProductPage = {
    ProductView: function (inputParams) {
        this.ViewName = inputParams.viewName;
        this.ViewInstanceVariableName = inputParams.viewInstanceVariableName;
        this.ControllerName = 'ProductApi';
        this.PageId = inputParams.pageId;
        this.Skip = 0;
        this.Take = 10;
        this.CountProductPrices = 0;
        this.PageNumber = 1;

        this.Currency = '';

        this.pageshow = function () {
           this.SetInitialData();
           this.GetUpdatePrices();
        }

        this.GetProductId = function()
        {
            var pageUrl = document.URL;
            return GetParametrFromUrl(pageUrl, 'Id');
        }
        this.SetInitialData = function ()
        {
            if(this.Skip >= this.Take)
                $('#prices_preview_div').css('visibility', 'visible');
            else
                $('#prices_preview_div').css('visibility', 'hidden');
            var productId = this.GetProductId()
            
            var methodName = 'GetInfo?id='+ productId;
            var type = 'GET';
            var thisContext = this;
            CallLoadData(this.ControllerName, methodName, type, null, function(result)
            {
                if(result.ok)
                {
                    var productModel = result.result
                    let product = $('#product');
                    product.empty();
                    var pTitle = $('<h1>', {
                            'text': productModel.name,
                            'with': '100%',
                            'css' : {
                                'color':'#7C0644',
                                'text-align': 'center',
                                'margin-bottom':'50px',
                            }
                        });
                    var contentDiv = $('<div>', {
                            'width': '100%',
                            'css' : {
                                'display': 'block'
                            }
                        });
                    var imgDiv = $('<div>', {
                            'width': '45%',
                            'css' : {
                                'display': 'inline-block'
                            }
                        });
                    var img = $('<img>', {
                            'src': productModel.imageSource,
                            'width': '65%',
                        });
                     var textDiv = $('<div>', {
                            'width': '45%',
                            'css' : {
                                'display': 'inline-block'
                            }
                        });
                     img.appendTo(imgDiv);
                     imgDiv.appendTo(contentDiv);
                     var divPrice = $('<div>', {
                            'text': 'Цена: ' + productModel.price + ' ' + productModel.currency,
                            'css' : {
                                'color':'#7C0644',
                                'font-weight':'bold'
                            }
                        });
                    thisContext.Currency = productModel.currency;
                    var descriptionArray = productModel.descriptions.split(';');
                    var divDescription = $('<div>', {
                            'css' : {
                                'color':'#7C0644',
                            }
                        });
                    descriptionArray.forEach(function (item, i) 
                    {
                        var pDescription = $('<p>', {
                            'text': item,
                            'with': '100%',
                            'css' : {
                                'color':'#7C0644',
                            }
                        });
                        pDescription.appendTo(divDescription);
                    });

                    pTitle.appendTo(product);
                    divPrice.appendTo(textDiv);
                    divDescription.appendTo(textDiv);
                    textDiv.appendTo(contentDiv);
                    contentDiv.appendTo(product);
                }
                else
                {
                    if(result.errors != null)
                    {
                        result.errors.forEach(function (item, i) {
                            ShowToast(item.message);
                        });
                    }
                    else
                    {
                        ShowToast(result.message);
                    }
                }
            },
            function (e) {
                ShowToast(e.responseText);
            });
        }
        this.Preview = function() {
            this.PageNumber = this.PageNumber - 1 <= 0 ? 1 : this.PageNumber - 1;
            this.GetUpdatePrices();
        }

        this.Next = function() {
            this.PageNumber = this.PageNumber + 1 >= this.CountProductPrices ? this.CountProductPrices : this.PageNumber + 1;
            this.GetUpdatePrices();
        }

        this.GetUpdatePrices = function()
        {
            this.Skip = (this.PageNumber - 1) * this.Take;
            if(this.Skip >= this.Take)
                $('#prices_preview_div').css('visibility', 'visible');
            else
                $('#prices_preview_div').css('visibility', 'hidden');
            debugger;
            var productId = this.GetProductId()
            var productId = this.GetProductId();
            var methodName = 'GetUpdatePricesList?productId='+ productId + '&skip=' + this.Skip + '&take=' + this.Take;
            var type = 'GET';
            var thisContext = this;

            CallLoadData(this.ControllerName, methodName, type, null, function(result) {
                if(result.ok) {

                    var table = $('<table>', {
                        'class':'tableProducts'
                    });
                    var trHeader = $('<tr>');
                    var thNumber = $('<th>', {
                         'text': 'Номер записи',
                         'css' : {
                                'color':'#7C0644',
                                'font-weight':'bold'
                            }
                        });
                    var thDateUpdate = $('<th>', {
                         'text': 'Дата обновления',
                         'css' : {
                                'color':'#7C0644',
                                'font-weight':'bold'
                            }
                        });
                    var thPrice = $('<th>', {
                         'text': 'Цена',
                         'css' : {
                                'color':'#7C0644',
                                'font-weight':'bold'
                            }
                        });

                    thNumber.appendTo(trHeader);
                        thDateUpdate.appendTo(trHeader);
                        thPrice.appendTo(trHeader);
                        trHeader.appendTo(table);
                    result.result.productUpdetePrices.forEach(function(item, i) {
                        var bodyTable = $('<tr>',{
                            'id': 'tr' + item.id
                        });

                        var numberTr;

                        if(thisContext.PageNumber > 1)
                        {
                            numberTr = thisContext.PageNumber - 1 + '' + (i + 1);
                            if(i == 9)
                            {
                                numberTr = thisContext.PageNumber + '0';
                            }
                        }
                        else
                        {
                            numberTr = i + 1;
                        }
                        var tdNumber = $('<td>', {
                         'text': numberTr
                        });
                        
                        var tdDateUpdate = $('<td>', {
                         'text': item.dateUpdateStr
                        });

                        var tdPrice = $('<td>', {
                         'text': item.priceUpdate + ' ' + thisContext.Currency
                        });

                        tdNumber.appendTo(bodyTable);
                        tdDateUpdate.appendTo(bodyTable);
                        tdPrice.appendTo(bodyTable);
                        bodyTable.appendTo(table);
                    });

                    $('#table_div').html(table);
                    thisContext.CountProductPrices = result.result.totalCount;
                    if(((thisContext.CountProductPrices - thisContext.Skip <= thisContext.Take) && thisContext.CountProductPrices != 0) || (thisContext.CountProductPrices <= thisContext.Take))
                        $('#prices_next_div').css('visibility', 'hidden');
                    else
                        $('#prices_next_div').css('visibility', 'visible');

                    if(thisContext.CountProductPrices == 0)
                        $('#pagination_prices').css('display', 'none');
                    else
                        $('#pagination_prices').css('display', 'block');                                            
                    $('#count_prices').css('visibility', 'visible');
                    var countPage = Math.ceil(thisContext.CountProductPrices / 10);
                    $('#count_prices').text(thisContext.PageNumber + '/' + countPage);
                } else {
                    if(result.errors != null) {
                        result.errors.forEach(function (item, i) {
                            ShowToast(item.message);
                        });
                    } else {
                        ShowToast(result.message);
                    }
                }
            },
            function(e) {
                ShowToast(e.responseText);
            });
        }    
    }
}

