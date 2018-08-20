var ProductListPage = {
    ProductListView: function (inputParams) {
        this.ViewName = inputParams.viewName;
        this.ViewInstanceVariableName = inputParams.viewInstanceVariableName;
        this.PageId = inputParams.pageId;
        this.ControllerName = 'ProductApi';
        this.Skip = 0;
        this.Take = 10;
        this.CountProducts = 0;
        this.PageNumber = 1;

        this.pageshow = function () {
            debugger;
            this.SetInitialData();
        }
        this.Preview = function()
        {
            this.PageNumber = this.PageNumber - 1 <= 0 ? 1 : this.PageNumber - 1;
            this.SetInitialData();
        }

        this.Next = function()
        {
            this.PageNumber = this.PageNumber + 1 >= this.CountProducts ? this.CountProducts : this.PageNumber + 1;
            this.SetInitialData();
        }

        this.SetInitialData = function ()
        {
            this.Skip = (this.PageNumber - 1) * this.Take;
            if(this.Skip >= this.Take)
                $('#preview_div').css('visibility', 'visible');
            else
                $('#preview_div').css('visibility', 'hidden');
            
            var methodName = 'GetList?skip='+this.Skip+'&take='+this.Take;
            var type = 'GET';
            var thisContext = this;
            CallLoadData(this.ControllerName, methodName, type, null, function(result)
            {
                if(result.ok)
                {
                    var table = $('<table>',
                    {
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
                    var thName = $('<th>', {
                         'text': 'Название продукта',
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
                        thName.appendTo(trHeader);
                        thPrice.appendTo(trHeader);
                        trHeader.appendTo(table);

                    result.result.products.forEach(function (item, i) {
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

                        var tdProductTitle = $('<td>');

                        var aTitle = $('<a>', {
                            'href': '/Product/Product?Id='+ item.id,
                            'id': 'aTitle'+item.id,
                            'text': item.name,
                            'css' : {
                                'color':'#7C0644',
                                'font-weight':'100',
                                'text-decoration':'none'
                            }
                        });
                        aTitle.appendTo(tdProductTitle);

                        
                        tdProductPrice = $('<td>', {
                        'text': item.price + ' ' + item.currency
                        });

                        tdNumber.appendTo(bodyTable);
                        tdProductTitle.appendTo(bodyTable);
                        tdProductPrice.appendTo(bodyTable);
                        bodyTable.appendTo(table);
                    });
                    $('#productTable').html(table);
                    thisContext.CountProducts = result.result.totalCount;
                    if(((thisContext.CountProducts - thisContext.Skip <= thisContext.Take) && thisContext.CountProducts != 0) || (thisContext.CountProducts <= thisContext.Take))
                        $('#next_div').css('visibility', 'hidden');
                    else
                        $('#next_div').css('visibility', 'visible');

                    if(thisContext.CountProducts == 0)
                        $('#pagination_context').css('display', 'none');
                    else
                        $('#pagination_context').css('display', 'block');                                            
                    $('#count_products').css('visibility', 'visible');
                    var countPage = Math.ceil(thisContext.CountProducts / 10);
                    $('#count_products').text(thisContext.PageNumber + '/' + countPage);
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
    }
}


