var IMSMaterial = (function (scope) {
    scope.AppToken = '';
    scope.materialSales = function () {
        $("#drpInvoice").select2();
        $("#TransactionDate").val("");
        $("#TransactionDate").attr('placeholder', 'dd/mm/yyyy');
        $("#Dispatch_Date").val("");
        $("#Dispatch_Date").attr('placeholder', 'dd/mm/yyyy');
        $("#SearchOrderNo").attr('disabled', 'disabled');
        $("#StateId").append($("<option></option>").val("0").html("Select State"));
        $("#StateId").attr('disabled', 'disabled');

        $("body").on("click", "#btnSubmit", function () {
            let PurchaseLineValues = [];
            let isValid = true;
            let msg = "";

            if (parseInt($("#OfficeId").val()) === 0) {
                isValid = false;
                msg = "Please fill all mandatory fields!!!";
            } else if (parseInt($("#PartyId").val()) === 0) {
                isValid = false;
                msg = "Please fill all mandatory fields!!!";
            } else if ($("#Purchase_Ref").val() === "") {
                isValid = false;
                msg = "Please fill all mandatory fields!!!";
            } else if ($("#TransactionDate").val() === "") {
                isValid = false;
                msg = "Please fill all mandatory fields!!!";
            } else if ($("#Dispatch_Date").val() === "") {
                isValid = false;
                msg = "Please fill all mandatory fields!!!";
            } else if (parseInt($("#StateId").val()) === 0) {
                isValid = false;
                msg = "Please fill all mandatory fields!!!";
            }
            if (isValid) {
                if ($("#tblMaterialSales TBODY TR").length > 0) {
                    $("#tblMaterialSales TBODY TR [id^='hdnItemId_']").each(function () {

                        let oMapping = {};
                        let index = parseInt($("#" + $(this).context.id).val());
                        let availableQty = parseInt($("#lblAvailable_Qty_" + index).text());
                        let qty = parseInt($("#txtOrder_Qty_" + index).val());
                        let poLineId = parseInt($("#hdnPOLineId_" + index).val());
                        let isItemType = parseInt($("#hdnItemTypeGrid_" + index).val());
                        let itemname = $("#lblItem_" + index)[0].innerText
                        if (poLineId === 0) {
                            if (isItemType === 16) {
                                isValid = true;
                            } else {
                                if (qty !== NaN && qty > 0 && qty > availableQty) {
                                    msg = "Please check the quantity for Item: " + itemname + "!!!";
                                    isValid = false;
                                    return false;
                                }
                            }
                        } else {
                            if (isItemType === 16) {
                                isValid = true;
                            } else {
                                if (qty !== NaN && qty > 0 && qty > availableQty) {
                                    msg = "Please check the quantity for Item: " + itemname + "!!!";
                                    isValid = false;
                                    return false;
                                }
                            }
                        }
                        if (isValid) {

                            oMapping.Line_Id = $("#hdnLineId_" + index).val();
                            oMapping.Unit_Id = $("#lblUnit_" + index).attr("data-Unit_Id");
                            oMapping.UnitTitle = $("#lblUnit_" + index).text();
                            oMapping.Available_Qty = availableQty;
                            oMapping.Item_Id = $("#hdnItemId_" + index).val();
                            oMapping.ItemTitle = $("#lblItem_" + index).text();
                            oMapping.HSN_SAC = $("#lblHSN_SAC_" + index).text();
                            oMapping.Quantity = $("#txtOrder_Qty_" + index).val();
                            oMapping.Rate = $("#txtRate_" + index).val();
                            oMapping.LastRate = $("#lblLastRate_$" + index).val();
                            oMapping.Amount = $("#lblAmount_" + index).text();
                            oMapping.Discount_1 = $("#txtDiscount1_" + index).val();
                            oMapping.Discount_2 = $("#txtDiscount2_" + index).val();
                            oMapping.Taxable_Amount = $("#lblTaxable_Amount_" + index).text();
                            oMapping.Discount_1_Amount = $("#hdnDiscount_1_Amt_" + index).val();
                            oMapping.Discount_2_Amount = $("#hdnDiscount_2_Amt_" + index).val();
                            oMapping.GST = $("#lblGST_" + index).text();
                            oMapping.CGST = $("#lblCGST_" + index).text();
                            oMapping.SGST = $("#lblSGST_" + index).text();
                            oMapping.IGST = $("#lblIGST_" + index).text();
                            oMapping.Total_Amount = $("#lblTotal_Amount_Row_" + index).text();
                            oMapping.PO_Id = $("#hdnPOId_" + index).val();
                            oMapping.POLine_Id = $("#hdnPOLineId_" + index).val();
                            oMapping.IsUpdate = $("#IsUpdateMaterialSales").val();
                            PurchaseLineValues.push(oMapping);
                            isValid = true;
                        }
                    });
                    $("#SaleLine").val(JSON.stringify(PurchaseLineValues));
                } else {
                    isValid = false;
                    msg = "Please at least one sale material!!!";
                }
            }


            if (isValid) {
                return true;
            } else {
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
        });

        $("#btnReset").click(function (e) {
            e.preventDefault();
            $("#Item_Id").val("");
            $("#ItemSearch").val("");
            $("#Hsn_Sac").val("");
            $("#Quantity").val("");
            $("#Unit_Id").val("");
            $("#Rate").val("");
            $("#Amount").val("");
            $("#Discount_1").val("0");
            $("#Discount_2").val("0");
            $("#Taxable_Amount").val("");
            $("#GST").val("0");
            $("#CGST").val("");
            $("#SGST").val("");
            $("#IGST").val("");
            $("#Total").val("");
            $("#hdnTotalAmount").val("");
            $("#Discount_1_Amount").val("");
            $("#Discount_2_Amount").val("");

        });

        $("#btnAdd").click(function (e) {
            e.preventDefault();
            let bAdded = true;
            let msg = "";
            let amount = 0.0;
            let isSpecialService = false;
            if (Number($("#hdnItemType").val()) === 16) {
                isSpecialService = true;
            }
            if (parseInt($("#Item_Id").val()) === 0) {
                msg = "Please select item.";
                bAdded = false;
            }
            if (!isSpecialService) {
                if ($("#Quantity").val() === "") {
                    msg = "Please enter quantity.";
                    bAdded = false;
                }
            } else {
                $("#Unit_Id").val(0)
            }
            if (parseInt($("#Unit_Id").val()) === 0 && !isSpecialService) {
                msg = "Please select unit.";
                bAdded = false;
            }
            if ($("#Rate").val() === "") {
                msg = "Please enter rate.";
                bAdded = false;
            }

            if (!isSpecialService && parseInt($("#Quantity").val()) > parseInt($("#AvailableQuantity").val())) {
                msg = "Please correct quantity that must not be greater than Available Quantity!!!";
                bAdded = false;
            }

            //Reference the Party and Location ddl.
            if (bAdded) {
                if ($("#tblMaterialSales TBODY TR").length > 0) {
                    $("#tblMaterialSales TBODY TR [id^='hdnItemId_']").each(function () {
                        let itemId = parseInt($("#" + $(this).context.id).val());
                        if (parseInt($("#Item_Id").val()) === itemId) {
                            msg = "Material is already mapped!!!";
                            bAdded = false;
                        }
                    });
                }
            }

            if (bAdded) {
                //Get the reference of the Table's TBODY element.
                var tBody = $("#tblMaterialSales > TBODY")[0];
                //Add Row.
                var row = tBody.insertRow(-1);
                let itemId = $("#Item_Id").val();
                let itemType = $("#hdnItemType").val();
                let htmlEditBtn = `<a class="btn btn-danger" style="padding: 2px 5px 2px 5px; margin-bottom:0px" onclick="Remove(this);" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlEditBtn);
                //Add Item cell.
                let lblItem = `<label id="lblItem_${itemId}">${$("#ItemSearch").val()}</label>
                              <input type="hidden" id="hdnItemId_${itemId}" name="hdnItemId_${itemId}" value="${itemId}" />
                              <input type="hidden" id="hdnLineId_${itemId}" name="hdnLineId_${itemId}" value="${0}" />
                              <input type="hidden" id="hdnPOLineId_${itemId}" name="hdnPOLineId_${itemId}" value="${0}" />
                              <input type="hidden" id="hdnPOId_${itemId}" name="hdnLineId_${itemId}" value="${0}" />
                              <input type="hidden" id="hdnItemTypeGrid_${itemId}" name="hdnItemTypeGrid_${itemId}" value="${itemType}" />
                              <input type="hidden" id="hdnIs_SameStateGrid_${itemId}" name="hdnIs_SameStateGrid_${itemId}" value="${$("#hdnIs_SameState").val()}" />
                              <input type="hidden" id="hdnDiscount_1_Amt_${itemId}" name="hdnDiscount_1_Amt_${itemId}" value="${$("#Discount_1_Amount").val()}" />
                              <input type="hidden" id="hdnDiscount_2_Amt_${itemId}" name="hdnDiscount_2_Amt_${itemId}" value="${$("#Discount_2_Amount").val()}" />`;
                cell = $(row.insertCell(-1));
                cell.append(lblItem);
                //Add Order Date.
                let lblOrderDate = `<label id="lblOrderDate_${itemId}">${"N/A"}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblOrderDate);
                //Add Hsn_Sac cell.
                let lblHSN_SAC = `<label id="lblHSN_SAC_${itemId}">${$("#Hsn_Sac").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblHSN_SAC);
                //Add Quantity cell.
                let htmlOrder_Qty = `<input type="text" id="txtOrder_Qty_${itemId}" disabled="disabled" style="background-color: ${parseInt($("#Quantity").val()) <= parseInt($("#AvailableQuantity").val()) ? 'lightgreen' : 'indianred'};width:40px;" style=" width: 60px;" data-index="${itemId}"  name="txtOrder_Qty_${itemId}" onchange="Calculate(this);" value="${$("#Quantity").val()}"  tp-type="numeric" />`;
                cell = $(row.insertCell(-1));
                cell.append(htmlOrder_Qty);
                //Add Available_Qty cell.
                let lblAvailable_Qty = `<label id="lblAvailable_Qty_${itemId}">${$("#AvailableQuantity").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblAvailable_Qty);
                //Add Unit cell.
                let lblUnit = `<label id="lblUnit_${itemId}" data-Unit_Id="${$("#Unit_Id").val()}" data-index="${itemId}" >${$("#Unit_Id :selected").text()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblUnit);
                //Add Last Rate cell.
                let htmlLastRate = `<label id="lblLastRate_${itemId}" data-index="${itemId}" >${$("#hdnLastRate").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlLastRate);
                //Add Rate cell.
                let htmlRate = `<input type="text" id="txtRate_${itemId}" data-index="${itemId}" name="txtRate_${itemId}" onchange="Calculate(this);" value="${$("#Rate").val()}"  tp-type="numeric" style="width:40px;"/>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlRate);
                //Add Amount cell.
                let lblAmount = `<label id="lblAmount_${itemId}">${$("#Amount").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblAmount);
                //Add Discount_1 cell.
                let htmlDiscount1 = `<input type="text" id="txtDiscount1_${itemId}" data-index="${itemId}" name="txtDiscount1_${itemId}" onchange="Calculate(this);" value="${$("#Discount_1").val()}"  tp-type="numeric" style="width:30px;"/>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlDiscount1);
                //Add Discount_2 cell.
                let htmlDiscount2 = `<input type="text" id="txtDiscount2_${itemId}" data-index="${itemId}" name="txtDiscount2_${itemId}" onchange="Calculate(this);" value="${$("#Discount_2").val()}"  tp-type="numeric" style="width:30px;"/>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlDiscount2);
                //Add Taxable_Amount cell.
                let lblTaxable_Amount = `<label id="lblTaxable_Amount_${itemId}">${$("#Taxable_Amount").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblTaxable_Amount);
                //Add GST cell.
                let lblGST = `<label id="lblGST_${itemId}">${$("#GST").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblGST);
                //Add CGST cell.
                let lblCGST = `<label id="lblCGST_${itemId}">${$("#CGST").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblCGST);
                //Add SGST cell.
                let lblSGST = `<label id="lblSGST_${itemId}">${$("#SGST").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblSGST);
                //Add IGST cell.
                let lblIGST = `<label id="lblIGST_${itemId}">${$("#IGST").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblIGST);
                //Add Total_Amount cell.
                let lblTotal_Amount = `<label id="lblTotal_Amount_Row_${itemId}">${$("#hdnTotalAmount").val()}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblTotal_Amount);

                //Set Default
                $("#Item_Id").val("");
                $("#ItemSearch").val("");
                $("#Hsn_Sac").val("");
                $("#Quantity").val("");
                $("#Unit_Id").val($("#Unit_Id option:first").val());
                $("#Rate").val("");
                $("#Amount").val("");
                $("#Discount_1").val("0");
                $("#Discount_2").val("0");
                $("#Taxable_Amount").val("");
                $("#Discount_1_Amount").val("");
                $("#Discount_2_Amount").val("");
                $("#AvailableQuantity").val("");
                $("#GST").val("0");
                $("#CGST").val("");
                $("#SGST").val("");
                $("#IGST").val("");
                $("#Total").val("");
                $("#hdnTotalAmount").val("");
                $("#hdnRowId").val("");
                $("#hdnLastRate").val("0")

                $("[id^='lblTotal_Amount_Row_']").each(function () {
                    amount += parseFloat($("#" + this.id).text());
                });

                $("td").each(function () {
                    $(this).addClass("tbl-css");
                });
                $("#PurchaseAmount").val(amount.toFixed(2));
                $("#lblTotalAmount").text("Total : " + amount.toFixed(2));
            }

            if (bAdded) {
                return true;
            } else {
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
        });

        $("#Quantity,#Rate,#Discount_1,#Discount_2,#GST").change(function () {
            CalculateAmount();
        });

        $("#StateId").change(function () {
            $("#SupplyStateId").val(parseInt($(this).val()));
        });

        $("#drpParty").change(function () {
            let Party_Id = parseInt($('#drpParty').val());
            $('#PartyId').val(Party_Id);
            IMSC.ajaxCall("GET", "/Material/GetInvoiceSales?Party_Id=" + Party_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                var result = JSON.parse(d);
                $("#drpInvoice").empty();
                $("#drpInvoice").append($("<option></option>").val("0").html("Select Invoice"));
                if (result.length > 0) {
                    $.each(result, function (index, value) {
                        $("#drpInvoice").append($("<option></option>").val(value.Sale_Id).html(value.Invoice_No));
                    });
                }
            });
        }).change();

        $("#drpInvoice").change(function () {
            $("#SaleId").val(parseInt($("#drpInvoice").val() !== "" ? $("#drpInvoice").val() : "0"));
        });

        $("#btnPrint").click(function (e) {

            let SaleId = parseInt($("#SaleId").val());
            let BillFormatId = parseInt($("#drpBillFormat").val());
            if (SaleId == null || SaleId == 0) {
                $('#alertModal').modal('show');
                $('#msg').html("Please select inovice.");
                return;
            }
            window.open("/Reports/ReportViewer.aspx?ReportId=1&SaleId=" + SaleId + "&BillFormat=" + BillFormatId + "");
        });

        $("#btnSearch").click(function (e) {
            e.preventDefault();
            var SaleId = parseInt($("#SaleId").val());
            $("#SaleId").val(SaleId);
            if (SaleId > 0) {
                IMSC.ajaxCall("GET", "/Material/GetMatrialSales?SaleId=" + SaleId + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var res = JSON.parse(d);
                    if (res !== null) {

                        var result = res.Table;
                        if (res.Table1.length > 0) {
                            $("#StateId").empty();
                            $("#StateId").append($("<option></option>").val(0).html("Select State"));
                            $.each(res.Table1, function (index, value) {
                                $("#StateId").append($("<option></option>").val(value.State_Id).html(value.StateName));
                            });
                        }
                        if (result.length > 0) {
                            $("#OfficeId").val(result[0].Office_Id);
                            $("#InvoiceNo").val(result[0].Invoice_No);
                            $("#VoucherNumber").val(result[0].Voucher_No);
                            $("#txtParty").val(result[0].PartyName);
                            $('#PartyId').val(result[0].Party_Id);
                            $("#StateId").val(result[0].SupplyState_Id);
                            $("#SupplyStateId").val(result[0].SupplyState_Id);
                            $("#SearchOrderNo").val(result[0].PO_No);
                            $("#POId").val(result[0].PO_Id);
                            $("#TransactionDate").val(result[0].Transaction_Date);
                            $("#Dispatch_Date").val(result[0].Dispatch_Date);
                            $("#Remarks").val(result[0].Remarks);
                            $("#Transporter").val(result[0].Transporter);
                            $("#Marka").val(result[0].Marka);
                            $("#tbodyid").empty();
                            $("#SaleAmount").val("0");
                            BindGrid(result, 1, 0);
                            $("td").each(function () {
                                $(this).addClass("tbl-css");
                            });
                            $("#SearchOrderNo").attr('disabled', 'disabled');
                        }
                    }
                });
            } else {
                $('#alertModal').modal('show');
                $('#msg').html("Please select inovice.");
            }
        });

        $("#btnResetForm").click(function (e) {
            e.preventDefault();
            $("#drpParty").val($("#drpParty option:first").val());
            $('#drpParty').select2().trigger('change');
            $("#drpInvoice").empty();
            $('#drpInvoice').select2().trigger('change');
            $("#SearchOrderNo").val("");
            $("#SearchParty").val("");
            $("#SaleId").val(0);
            $("#IsUpdateMaterialSales").val(false);
            $("#SaleLine").val("");
            $("#SupplyStateId").val("");
            $("#PartyId").val("");
            $("#OfficeId").val($("#drpPartyId option:first").val());
            $("#InvoiceNo").val("");
            $("#VoucherNumber").val("");
            $("#drpPartyId").val($("#drpPartyId option:first").val());
            $("#Purchase_Ref").val("");
            $("#TransactionDate").val("");
            $("#Dispatch_Date").val("");
            $("#Remarks").val("");
            $("#Marka").val("");
            $("#Transporter").val("");
            $("#StateId").empty();
            $("#Item_Id").val("");
            $("#ItemSearch").val("");
            $("#Hsn_Sac").val("");
            $("#Quantity").val("");
            $("#Unit_Id").val($("#Unit_Id option:first").val());
            $('#Unit_Id').select2().trigger('change');
            $("#Rate").val("");
            $("#Amount").val("");
            $("#Discount_1").val("");
            $("#Discount_2").val("");
            $("#Taxable_Amount").val("");
            $("#GST").val("");
            $("#CGST").val("");
            $("#SGST").val("");
            $("#IGST").val("");
            $("#Total").val("");
            $("#hdnRowId").val("");
            $("#lblTotalAmount").text("Total : 0.00");
            $("#tblMaterialSales TBODY").empty();
            $("#divDeleteMatrial").css('display', 'none');
            $("#SearchOrderNo").attr('disabled', 'disabled');
            $("#StateId").append($("<option></option>").val("0").html("Select State"));
            $("#StateId").attr('disabled', 'disabled');
        });

        $("#btnDelete").click(function () {
            let SaleId = parseInt($("#SaleId").val());
            if (SaleId > 0) {
                window.location.href = "/Material/DeleteMatrialSales?SaleId=" + SaleId + "&AppToken=" + scope.AppToken;
            }
        });

        $("#SearchParty").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchParty?Party=" + request.term + "&OfficeId=" + Number($("#OfficeId").val()) + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.Title, value: item.Party_Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
                $("#SearchParty").val(i.item.label);
                let PartyId = parseInt(i.item.value);
                $("#SearchOrderNo").removeAttr('disabled');
                let OfficeId = parseInt($('#OfficeId').val());
                $("#PartyId").val(PartyId);
                if (PartyId > 0) {
                    IMSC.ajaxCall("GET", "/Material/GetStateSales?PartyId=" + PartyId + "&OfficeId=" + OfficeId + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                        var result = JSON.parse(d);
                        if (result !== null) {
                            $("#StateId").empty();
                            if (result.Table.length > 0) {
                                $("#StateId").removeAttr('disabled');
                                $("#StateId").append($("<option></option>").val("0").html("Select State"));
                                $.each(result.Table, function (index, value) {
                                    $("#StateId").append($("<option></option>").val(value.State_Id).html(value.StateName));
                                });
                            } else {
                                $("#StateId").append($("<option></option>").val("0").html("Select State"));
                            }
                            if (result.Table2 !== undefined && result.Table2.length > 0) {
                                $("#tbodyid").empty();
                                BindGrid(result.Table2, 0, 1);
                            }
                        }
                    });
                } else {
                    $("#SearchOrderNo").val("");
                    $("#StateId").empty();
                    $("#StateId").append($("<option></option>").val("0").html("Select State"));
                    $("#tbodyid").empty();
                    $("#lblTotalAmount").text("Total : 0.00");
                    $("#IsUpdateMaterialSales").val(0);
                    $("#SaleAmount").val("0");
                }
            },
            minLength: 1
        });

        $("#SearchOrderNo").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchOrderNo?OrderNo=" + request.term + "&PartyId=" + Number($("#PartyId").val()) + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.PO_No, value: item.PO_Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
                $("#SearchOrderNo").val(i.item.label);
                let POId = parseInt(i.item.value);
                let isValid = true;
                $("#POId").val(POId);
                var Office_Id = parseInt($("#OfficeId").val());
                var SupplyState_Id = parseInt($("#SupplyStateId").val());
                if (POId > 0) {
                    IMSC.ajaxCall("GET", "/Material/GetPOData?POId=" + POId + "&Office_Id=" + Office_Id + "&SupplyState_Id=" + SupplyState_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                        var result = JSON.parse(d);

                        if (result.length > 0) {
                            $("#SaleAmount").val("0");
                            $(result).each(function (index, value) {
                                $("[id^='hdnItemId_']").each(function () {
                                    if (parseInt(value.Item_Id) === parseInt($("#" + this.id).val())) {
                                        isValid = false;
                                        return false;
                                    }
                                });
                            });

                            if (isValid) {
                                $("#tbodyid").empty();
                                BindGrid(result, 0, 1);
                                $("td").each(function () {
                                    $(this).addClass("tbl-css");
                                });
                            } else {
                                $('#alertModal').modal('show');
                                $('#msg').html("One of the Item is already mapped in the grid. Please remove that first !!!");
                            }
                        }
                    });
                } else {
                    $("#tbodyid").empty();
                    $("#SaleAmount").val("0");
                    $("#divDeleteMatrial").css('display', 'none');
                    $("#lblTotalAmount").text("Total : 0.00");
                }
            },
            minLength: 1
        });

        $("#ItemSearch").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchItem?Item=" + request.term + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.Title, value: item.Item_Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
                $("#ItemSearch").val(i.item.label);
                let Item_Id = parseInt(i.item.value);
                $("#Item_Id").val(Item_Id);
                let Office_Id = parseInt($("#OfficeId").val());
                let P_State_Id = parseInt($("#SupplyStateId").val());
                if (Item_Id > 0) {
                    IMSC.ajaxCall("GET", "/Material/GetHSN_Detail?Item_Id=" + Item_Id + "&Office_Id=" + Office_Id + "&P_State_Id=" + P_State_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                        var result = JSON.parse(d);
                        if (result[0].HSN_SAC !== null && result[0].GST !== null && result[0].Is_SameState !== null) {
                            $("#Hsn_Sac").val(result[0].HSN_SAC);
                            $("#GST").val(result[0].GST);
                            $("#hdnIs_SameState").val(result[0].Is_SameState);
                            $("#hdnItemType").val(result[0].ItemNature);
                            $("#hdnLastRate").val(result[0].LastPurchaseRate);
                            $("#AvailableQuantity").val(result[0].Available_Qty);
                            $("#Unit_Id").val(result[0].UnitId);
                            $("#GST").change();
                        } else {
                            $("#Hsn_Sac").val("");
                            $("#GST").val("0");
                            $("#hdnIs_SameState").val("0");
                            $("#GST").change();
                        }
                    });
                }
            },
            minLength: 1
        });

        function ResetFromOfficeChange() {
            $("#SearchOrderNo").val("");
            $("#StateId").empty();
            $("#StateId").append($("<option></option>").val("0").html("--Select--"));
            $("#tbodyid").empty();
            $("#lblTotalAmount").text("Total : 0.00");
            $("#IsUpdateMaterialSales").val(0);
            $("#SaleAmount").val("0");
        }

        ResetFromOfficeChange();

        function CalculateAmount() {
            let bIsDiscount = false;
            let isValidate = true;
            let quantity = parseFloat($("#Quantity").val() !== "" ? $("#Quantity").val() : 0);
            let rate = parseFloat($("#Rate").val() !== "" ? $("#Rate").val() : 0);
            let amount = 0;
            let disAmu = 0;
            let gst = 0;
            let discount_1 = 0;
            let discount_2 = 0;
            let discount_1_Amt = 0;
            let discount_2_Amt = 0;
            let discountAmount = 0;
            let totalAmount = 0;
            let isSpecialService = false;
            if (Number($("#hdnItemType").val()) === 16) {
                isSpecialService = true;
            }
            if (isSpecialService) {
                isValidate = false;
                quantity = 1;
            }
            if (quantity > 0) {
                if (isValidate && rate > 0) {
                    amount = quantity * rate;
                    $("#Amount").val(amount.toFixed(2));
                } else {
                    amount = quantity * rate;
                    $("#Amount").val(amount.toFixed(2));
                }

            }
            if (isValidate) {
                if (amount > 0) {
                    CommonFunction(bIsDiscount, isValidate, quantity, rate, amount, disAmu, gst, discount_1, discount_2, discount_1_Amt, discount_2_Amt, discountAmount, totalAmount);
                }
            } else {
                CommonFunction(bIsDiscount, isValidate, quantity, rate, amount, disAmu, gst, discount_1, discount_2, discount_1_Amt, discount_2_Amt, discountAmount, totalAmount);
            }
        }
        function CommonFunction(bIsDiscount, isValidate, quantity, rate, amount, disAmu, gst, discount_1, discount_2, discount_1_Amt, discount_2_Amt, discountAmount, totalAmount) {
            discount_1 = parseFloat($("#Discount_1").val() !== "" ? $("#Discount_1").val() : 0);
            discount_2 = parseFloat($("#Discount_2").val() !== "" ? $("#Discount_2").val() : 0);
            if (discount_1 > 0 || discount_2 > 0) {
                bIsDiscount = true;
                //disAmu = amount * (discount_1 + discount_2) / 100;
                discount_1_Amt = amount * (discount_1) / 100;
                discount_2_Amt = amount * (discount_2) / 100;
                disAmu = discount_1_Amt; //+ discount_2_Amt;

                $("#Discount_1_Amount").val(discount_1_Amt.toFixed(2));
                $("#Discount_2_Amount").val(discount_2_Amt.toFixed(2));

                discountAmount = amount - disAmu;
                gst = discountAmount * parseFloat($("#GST").val()) / 100;
            } else {
                bIsDiscount = false;
                gst = amount * parseFloat($("#GST").val()) / 100;
            }

            let is_SameState = $("#hdnIs_SameState").val() === "1" ? true : false;
            if (is_SameState) {
                $("#CGST").val(parseFloat(gst / 2).toFixed(2));
                $("#SGST").val(parseFloat(gst / 2).toFixed(2));
                $("#IGST").val(parseFloat(0).toFixed(2));

            } else {
                $("#CGST").val(parseFloat(0).toFixed(2));
                $("#SGST").val(parseFloat(0).toFixed(2));
                $("#IGST").val(parseFloat(gst).toFixed(2));
            }
            if (bIsDiscount) {
                totalAmount = discountAmount + gst;
                $("#Taxable_Amount").val(discountAmount.toFixed(2));
            } else {
                totalAmount = amount + gst;
                $("#Taxable_Amount").val(amount.toFixed(2));
            }
            $("#Total").val(totalAmount.toFixed(2));
            $("#hdnTotalAmount").val(totalAmount.toFixed(2));
        }
        function Calculate(value) {
            $("#IsUpdateMaterialSales").val(1);
            let index = parseInt($('#' + value.id).attr('data-index'));
            let bIsDiscount = false;
            let quantity = parseFloat($("#txtOrder_Qty_" + index).val() !== "" ? $("#txtOrder_Qty_" + index).val() : 0);
            let aQuantity = parseFloat(parseInt($("#lblAvailable_Qty_" + index).text()));
            let poLineId = parseInt($("#hdnPOLineId_" + index).val());
            if (poLineId === 0) {
                setCaluValuew(index, bIsDiscount, quantity, poLineId);
            } else {
                if (quantity <= aQuantity) {
                    setCaluValuew(index, bIsDiscount, quantity, poLineId);
                }
                else {
                    $('#alertModal').modal('show');
                    $('#msg').html("Please check available quantity!!!");
                }
            }
        }
        function setCaluValuew(index, bIsDiscount, quantity, poLineId) {
            let rate = parseFloat($("#txtRate_" + index).val() !== "" ? $("#txtRate_" + index).val() : 0);
            let amount = 0;
            let disAmu = 0;
            let gst = 0;
            let discount_1 = 0;
            let discount_2 = 0;
            let discount_1_amt = 0;
            let discount_2_amt = 0;
            let discountAmount = 0;
            let totalAmount = 0;
            let sumOfTotal = 0;
            if (quantity > 0 && rate > 0) {
                amount = quantity * rate;
                $("#lblAmount_" + index).text(amount);
            }
            if (amount > 0) {
                discount_1 = parseFloat($("#txtDiscount1_" + index).val() !== "" ? $("#txtDiscount1_" + index).val() : 0);
                discount_2 = parseFloat($("#txtDiscount2_" + index).val() !== "" ? $("#txtDiscount2_" + index).val() : 0);
                if (discount_1 > 0 || discount_2 > 0) {
                    bIsDiscount = true;
                    //disAmu = amount * (discount_1 + discount_2) / 100;
                    discount_1_amt = amount * (discount_1) / 100;
                    discount_2_amt = amount * (discount_2) / 100;
                    disAmu = discount_1_amt;//+ discount_2_amt;

                    $("#hdnDiscount_1_Amt_" + index).val(discount_1_amt.toFixed(2));
                    $("#hdnDiscount_2_Amt_" + index).val(discount_2_amt.toFixed(2));

                    discountAmount = amount - disAmu;
                    gst = discountAmount * parseFloat($("#lblGST_" + index).text()) / 100;
                } else {
                    bIsDiscount = false;
                    gst = amount * parseFloat($("#lblGST_" + index).text()) / 100;
                }

                let is_SameState = $("#hdnIs_SameStateGrid_" + index).val() === "1" ? true : false;
                if (is_SameState) {
                    $("#lblCGST_" + index).text(parseFloat(gst / 2).toFixed(2));
                    $("#lblSGST_" + index).text(parseFloat(gst / 2).toFixed(2));
                    $("#lblIGST_" + index).text(parseFloat(0).toFixed(2));

                } else {
                    $("#lblCGST_" + index).text(parseFloat(0).toFixed(2));
                    $("#lblSGST_" + index).text(parseFloat(0).toFixed(2));
                    $("#lblIGST_" + index).text(parseFloat(gst).toFixed(2));
                }
                if (bIsDiscount) {
                    totalAmount = discountAmount + gst;
                    $("#lblTaxable_Amount_" + index).text(discountAmount.toFixed(2));
                } else {
                    totalAmount = amount + gst;
                    $("#lblTaxable_Amount_" + index).text(amount.toFixed(2));
                }
                $("#lblTotal_Amount_Row_" + index).text(totalAmount.toFixed(2));
                if (poLineId > 0) {
                    $("#txtOrder_Qty_" + index).css('background-color', parseInt($("#txtOrder_Qty_" + index).val()) <= parseInt($("#lblAvailable_Qty_" + index).text()) ? "lightgreen" : "indianred");
                }
                $("[id^='lblTotal_Amount_Row_']").each(function () {
                    sumOfTotal += parseFloat($("#" + this.id).text());
                });
                $("#SaleAmount").val(sumOfTotal.toFixed(2));
                $("#lblTotalAmount").text("Total : " + sumOfTotal.toFixed(2));
            }
        }
        function Remove(button) {
            //Determine the reference of the Row using the Button.
            $("#IsUpdateMaterialSales").val(0);
            var row = $(button).closest("TR");
            var name = $("TD", row).eq(0).text();
            if (confirm("Do you want to remove this purchase.", "Remove")) {
                //Get the reference of the Table.
                var table = $("#tblMaterialSales")[0];
                //Delete the Table row using it's Index.
                table.deleteRow(row[0].rowIndex);
            }
            let sumOfTotal = 0;
            $("#tblMaterialSales TBODY TR").each(function () {
                var row = $(this);
                let td = row.find("TD");
                sumOfTotal += parseFloat(td[14].innerText);
            });
            $("#lblTotalAmount").text("Total : " + sumOfTotal.toFixed(2));
            $("#IsUpdateMaterialSales").val(1);
        };
        function BindGrid(result, isqtydisabled, sourceid) {
            let totalAmount = 0;
            $.each(result, function (index, value) {

                let quantity = parseFloat(value.Order_Qty);
                let rate = parseFloat(value.Order_Rate);
                let gst = parseFloat(value.GST !== "" ? value.GST : "0");
                let amount = 0;
                let tamount = 0;
                let gstAmount = 0;
                let cgst = 0;
                let sgst = 0;
                let igst = 0;
                let Texable_Amount = 0;
                let discount_1 = 0;
                let discount_2 = 0;
                if (quantity > 0 && rate > 0) {
                    amount = quantity * rate;
                }

                if (amount > 0) {
                    gstAmount = amount * gst / 100;
                    if (sourceid === 1) {
                        let is_SameState = value.Is_SameState.toString() === "1" ? true : false;
                        if (is_SameState) {
                            cgst = parseFloat(gstAmount / 2).toFixed(2)
                            sgst = parseFloat(gstAmount / 2).toFixed(2)
                            igst = parseFloat(0).toFixed(2);
                        } else {
                            cgst = parseFloat(0).toFixed(2)
                            sgst = parseFloat(0).toFixed(2)
                            igst = parseFloat(gstAmount).toFixed(2);
                        }
                        Texable_Amount = amount;
                        tamount = amount + gstAmount;
                    }
                    else {
                        cgst = value.CGST;
                        sgst = value.SGST;;
                        igst = value.IGST;;
                        discount_1 = parseInt(value.Discount_1);
                        discount_2 = parseInt(value.Discount_2);
                        amount = value.Amount;
                        //let disamt = amount * (discount_1 + discount_2) / 100;
                        //Texable_Amount = amount - disamt;
                        Texable_Amount = value.Taxable_Amount;
                        tamount = value.TotalAmount;
                    }


                }



                //Get the reference of the Table's TBODY element.
                var tBody = $("#tblMaterialSales > TBODY")[0];
                //Add Row.
                var row = tBody.insertRow(-1);
                $(row).attr('id', 'trRow_' + value.Line_Id);
                //Add Button cell.
                //let htmlEditBtn = `<input type="button" onclick="Remove(this);" value="Remove"/>`;
                let htmlEditBtn = `<a class="btn btn-danger" style="padding: 2px 5px 2px 5px; margin-bottom:0px" onclick="Remove(this);" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlEditBtn);
                //Add Item cell.

                let lblItem = `<label id="lblItem_${value.Item_Id}">${value.ItemName}</label>
                            <input type="hidden" id="hdnItemId_${value.Item_Id}" name="hdnItemId_${value.Item_Id}" value="${value.Item_Id}" />
                            <input type="hidden" id="hdnLineId_${value.Item_Id}" name="hdnLineId_${value.Item_Id}" value="${value.Line_Id === undefined ? "0" : value.Line_Id}" />
                            <input type="hidden" id="hdnPOLineId_${value.Item_Id}" name="hdnPOLineId_${value.Item_Id}" value="${value.POLine_Id}" />
                            <input type="hidden" id="hdnPOId_${value.Item_Id}" name="hdnPOId_${value.Item_Id}" value="${value.PO_Id}" />
                            <input type="hidden" id="hdnItemTypeGrid_${value.Item_Id}" name="hdnItemTypeGrid_${value.Item_Id}" value="${value.ItemNature}" />
                            <input type="hidden" id="hdnIs_SameStateGrid_${value.Item_Id}" name="hdnIs_SameStateGrid_${value.Item_Id}" value="${value.Is_SameState === undefined ? "0" : value.Is_SameState}" />
                            <input type="hidden" id="hdnDiscount_1_Amt_${value.Item_Id}" name="hdnDiscount_1_Amt_${value.Item_Id}" value="${value.Discount_1_Amount === undefined ? "0" : value.Discount_1_Amount}" />
                            <input type="hidden" id="hdnDiscount_2_Amt_${value.Item_Id}" name="hdnDiscount_2_Amt_${value.Item_Id}" value="${value.Discount_2_Amount === undefined ? "0" : value.Discount_1_Amount}" />
                          `;
                cell = $(row.insertCell(-1));
                cell.append(lblItem);
                //Add Order Date.
                let lblOrderDate = `<label id="lblOrderDate_${value.Item_Id}">${value.OrderDate !== undefined ? value.OrderDate : "N/A"}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblOrderDate);
                //Add Hsn_Sac cell.
                let lblHSN_SAC = `<label id="lblHSN_SAC_${value.Item_Id}">${value.HSN_SAC}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblHSN_SAC);

                //Add Quantity cell.
                let htmlOrder_Qty = `<input type="text" id="txtOrder_Qty_${value.Item_Id}" ${isqtydisabled == 1 ? "disabled" : "enabled"} data-index="${value.Item_Id}" style="background-color: ${value.Order_Qty <= value.Available_Qty ? 'lightgreen' : 'indianred'};width:40px;" name="txtOrder_Qty_${value.Item_Id}" onchange="Calculate(this);" value="${quantity}"  tp-type="numeric" />`;
                cell = $(row.insertCell(-1));
                cell.append(htmlOrder_Qty);
                //Add Available_Qty cell.
                let lblAvailable_Qty = `<label id="lblAvailable_Qty_${value.Item_Id}" style="background-color: lightgreen;">${value.Available_Qty === null ? "0" : value.Available_Qty}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblAvailable_Qty);
                //Add Unit cell.
                let lblUnit = `<label id="lblUnit_${value.Item_Id}" data-Unit_Id="${value.UnitId}" data-index="${value.Item_Id}" >${value.Unit}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblUnit);
                //Add Last Rate cell.
                let htmlLastRate = `<label id="lblLastRate_${value.Item_Id}" data-index="${value.Item_Id}" >${value.LastPurchaseRate === undefined ? "0" : value.LastPurchaseRate}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlLastRate);
                //Add Rate cell.
                let htmlRate = `<input type="text" id="txtRate_${value.Item_Id}" ${isqtydisabled == 1 ? "disabled" : "enabled"} data-index="${value.Item_Id}" name="txtRate_${value.Item_Id}" onchange="Calculate(this);" value="${rate}"  tp-type="numeric" style="width:40px;"/>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlRate);
                //Add Amount cell.
                let lblAmount = `<label id="lblAmount_${value.Item_Id}">${amount}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblAmount);
                //Add Discount_1 cell.
                let htmlDiscount1 = `<input type="text" id="txtDiscount1_${value.Item_Id}" ${isqtydisabled == 1 ? "disabled" : "enabled"} data-index="${value.Item_Id}" name="txtDiscount1_${value.Item_Id}" onchange="Calculate(this);" value="${sourceid === 1 ? 0 : discount_1}"  tp-type="numeric" style="width:30px;"/>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlDiscount1);
                //Add Discount_2 cell.
                let htmlDiscount2 = `<input type="text" id="txtDiscount2_${value.Item_Id}" ${isqtydisabled == 1 ? "disabled" : "enabled"} data-index="${value.Item_Id}" name="txtDiscount2_${value.Item_Id}" onchange="Calculate(this);" value="${sourceid === 1 ? 0 : discount_2}"  tp-type="numeric" style="width:30px;"/>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlDiscount2);
                //Add Taxable_Amount cell.
                let lblTaxable_Amount = `<label id="lblTaxable_Amount_${value.Item_Id}">${Texable_Amount}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblTaxable_Amount);
                //Add GST cell.
                let lblGST = `<label id="lblGST_${value.Item_Id}">${gst}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblGST);
                //Add CGST cell.
                let lblCGST = `<label id="lblCGST_${value.Item_Id}">${cgst}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblCGST);
                //Add SGST cell.
                let lblSGST = `<label id="lblSGST_${value.Item_Id}">${sgst}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblSGST);
                //Add IGST cell.
                let lblIGST = `<label id="lblIGST_${value.Item_Id}">${igst}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblIGST);
                //Add Total_Amount cell.
                let lblTotal_Amount = `<label id="lblTotal_Amount_Row_${value.Item_Id}">${tamount}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblTotal_Amount);

            });
            $("[id^='lblTotal_Amount_Row_']").each(function () {
                totalAmount += parseFloat($("#" + this.id).text());
            });
            $("#SaleAmount").val(totalAmount.toFixed(2));
            $("#lblTotalAmount").text("Total : " + totalAmount.toFixed(2));
            $("#divDeleteMatrial").css('display', 'block');
        };
    };
    scope.materialPurchase = function () {
        $("#drpParty").select2();
        $("#drpInvoice").select2();
        $("#TransactionDate").val("");

        $("#TransactionDate").attr('placeholder', 'dd/mm/yyyy');

        $("body").on("click", "#btnReset", function (e) {
            e.preventDefault();
            $("#Item_Id").val("");
            $('#ItemSearch').val("");
            $("#Hsn_Sac").val("");
            $("#Quantity").val("");
            $("#Unit_Id").val($("#Unit_Id option:first").val());
            $("#Rate").val("");
            $("#Amount").val("");
            $("#Discount_1").val("0");
            $("#Discount_2").val("0");
            $("#Taxable_Amount").val("");
            $("#GST").val("0");
            $("#CGST").val("");
            $("#SGST").val("");
            $("#IGST").val("");
            $("#Total").val("");
            $("#hdnTotalAmount").val("");
            $("#hdnRowId").val("");
            $("#btnAdd").val("Add");
        });

        $("#btnAdd").click(function (e) {
            e.preventDefault();
            let bAdded = true;
            let msg = "";
            let amount = 0.0;

            if (parseInt($("#Item_Id").val()) === 0) {
                msg = "Please select item.";
                bAdded = false;
            } else if ($("#Quantity").val() === "") {
                msg = "Please enter quantity.";
                bAdded = false;
            } else if (parseInt($("#Unit_Id").val()) === 0) {
                msg = "Please select unit.";
                bAdded = false;
            } else if ($("#Rate").val() === "") {
                msg = "Please Please enter rate.";
                bAdded = false;
            }
            //Reference the Party and Location ddl.
            if (bAdded) {
                if ($("#tblMaterialPurchase TBODY TR").length > 0) {
                    $("#tblMaterialPurchase TBODY TR").each(function () {
                        var row = $(this);
                        let td = row.find("TD");
                        let itemId = parseInt(td.eq(1).attr('data-item-id'));
                        if (parseInt($("#Item_Id").val()) === itemId) {
                            msg = "Material is already mapped!!!";
                            bAdded = false;
                        }
                    });
                }
            }

            if (bAdded) {
                var tBody = $("#tblMaterialPurchase > TBODY")[0];
                //Add Row.
                var row = tBody.insertRow(-1);
                //Add Button cell.
                cell = $(row.insertCell(-1));
                let btnRemove = `<a class="btn btn-danger" onclick="Remove(this);" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
                cell.append(btnRemove);

                //Add Item cell.
                var cell = $(row.insertCell(-1));
                cell.html($("#ItemSearch").text());
                cell.attr('data-item-id', $("#Item_Id").val());
                cell.attr('data-line-Id', 0);

                //Add Hsn_Sac cell.
                cell = $(row.insertCell(-1));
                cell.html($("#Hsn_Sac").val());
                //Add Quantity cell.
                cell = $(row.insertCell(-1));
                cell.html($("#Quantity").val());
                //Add Unit cell.
                cell = $(row.insertCell(-1));
                cell.html($("#Unit_Id :selected").text());
                cell.attr('data-Unit_Id', $("#Unit_Id").val());
                //Add Rate cell.
                cell = $(row.insertCell(-1));
                cell.html($("#Rate").val());
                //Add Amount cell.
                cell = $(row.insertCell(-1));
                cell.html($("#Amount").val());
                //Add Discount_1 cell.
                cell = $(row.insertCell(-1));
                cell.html($("#Discount_1").val());
                //Add Discount_2 cell.
                cell = $(row.insertCell(-1));
                cell.html($("#Discount_2").val());
                //Add Taxable_Amount cell.
                cell = $(row.insertCell(-1));
                cell.html($("#Taxable_Amount").val());
                //Add GST cell.
                cell = $(row.insertCell(-1));
                cell.html($("#GST").val());
                //Add CGST cell.
                cell = $(row.insertCell(-1));
                cell.html($("#CGST").val());
                //Add SGST cell.
                cell = $(row.insertCell(-1));
                cell.html($("#SGST").val());
                //Add IGST cell.
                cell = $(row.insertCell(-1));
                cell.html($("#IGST").val());
                //Add Total_Amount cell.
                cell = $(row.insertCell(-1));
                cell.html($("#hdnTotalAmount").val());
                //}

                //Set Default
                $("#Item_Id").val("");
                $('#ItemSearch').val("");
                $("#Hsn_Sac").val("");
                $("#Quantity").val("");
                $("#Unit_Id").val($("#Unit_Id option:first").val());
                $("#Rate").val("");
                $("#Amount").val("");
                $("#Discount_1").val("0");
                $("#Discount_2").val("0");
                $("#Taxable_Amount").val("");
                $("#GST").val("0");
                $("#CGST").val("");
                $("#SGST").val("");
                $("#IGST").val("");
                $("#Total").val("");
                $("#hdnTotalAmount").val("");
                $("#hdnRowId").val("");

                $("#tblMaterialPurchase TBODY TR").each(function () {
                    var row = $(this);
                    let td = row.find("TD");
                    amount += parseFloat(td[14].innerText);
                });
                $("td").each(function () {
                    $(this).addClass("tbl-css");
                });
                $("#PurchaseAmount").val(amount.toFixed(2));
                $("#lblTotalAmount").text("Total : " + amount.toFixed(2));
            }

            if (bAdded) {
                return true;
            } else {
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
        });

        $("body").on("click", "#btnSubmit", function () {
            //Loop through the Table rows and build a JSON array.
            let PurchaseLineValues = [];
            let isValid = true;
            let msg = "";
            if ($("#tblMaterialPurchase TBODY TR").length > 0) {
                $("#tblMaterialPurchase TBODY TR").each(function () {
                    let row = $(this).find('td');
                    let oMapping = {};
                    oMapping.Line_Id = row.eq(1).attr('data-line-Id');
                    oMapping.Item_Id = row.eq(1).attr('data-item-id');
                    oMapping.HSN_SAC = row[2].innerText;
                    oMapping.Quantity = row[3].innerText;
                    oMapping.Unit_Id = row.eq(4).attr('data-unit-id');
                    oMapping.UnitTitle = row[4].innerText;
                    oMapping.Rate = row[5].innerText;
                    oMapping.Amount = row[6].innerText;
                    oMapping.Discount_1 = row[7].innerText;
                    oMapping.Discount_2 = row[8].innerText;
                    oMapping.Taxable_Amount = row[9].innerText;
                    oMapping.GST = row[10].innerText;
                    oMapping.CGST = row[11].innerText;
                    oMapping.SGST = row[12].innerText;
                    oMapping.IGST = row[13].innerText;
                    oMapping.Total_Amount = row[14].innerText;
                    PurchaseLineValues.push(oMapping);
                });
                $("#PurchaseLine").val(JSON.stringify(PurchaseLineValues));
            } else {
                msg = "Please add at least 1 matrial purchase.";
                isValid = false;
            }
            if (parseInt($("#OfficeId").val()) === 0 || $("#InvoiceNo").val() === "" || parseInt($("#PartyId").val()) === 0
                || $("#Purchase_Ref").val() === "" || $("#TransactionDate").val() === "" || parseInt($("#StateId").val()) === 0) {
                msg = "Please fill all mandatory fields.";
                isValid = false;
            }

            if (isValid) {
                return true;
            } else {
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
        });

        $("#Item_Id").change(function () {
            let Item_Id = parseInt($("#Item_Id").val());
            let Office_Id = parseInt($("#OfficeId").val());
            let P_State_Id = parseInt($("#SupplyStateId").val());
            if (Item_Id > 0) {
                IMSC.ajaxCall("GET", "/Material/GetHSN_Detail?Item_Id=" + Item_Id + "&Office_Id=" + Office_Id + "&P_State_Id=" + P_State_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result[0].HSN_SAC !== null && result[0].GST !== null && result[0].Is_SameState !== null) {
                        $("#Hsn_Sac").val(result[0].HSN_SAC);
                        $("#GST").val(result[0].GST);
                        $("#hdnIs_SameState").val(result[0].Is_SameState);
                        $("#GST").change();
                        $("#Unit_Id").val(result[0].UnitId);

                    } else {
                        $("#Hsn_Sac").val("");
                        $("#GST").val("0");
                        $("#hdnIs_SameState").val(0);
                        $("#GST").change();
                    }
                });
            }
        });

        $("#ItemSearch").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchItem?Item=" + request.term + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.Title, value: item.Item_Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
                $("#ItemSearch").val(i.item.label);
                let Item_Id = parseInt(i.item.value);
                $("#Item_Id").val(Item_Id);
                let Office_Id = parseInt($("#OfficeId").val());
                let P_State_Id = parseInt($("#SupplyStateId").val());
                if (Item_Id > 0) {
                    IMSC.ajaxCall("GET", "/Material/GetHSN_Detail?Item_Id=" + Item_Id + "&Office_Id=" + Office_Id + "&P_State_Id=" + P_State_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                        var result = JSON.parse(d);
                        if (result[0].HSN_SAC !== null && result[0].GST !== null && result[0].Is_SameState !== null) {
                            $("#Hsn_Sac").val(result[0].HSN_SAC);
                            $("#GST").val(result[0].GST);
                            $("#hdnIs_SameState").val(result[0].Is_SameState);
                            $("#GST").change();
                            $("#Unit_Id").val(result[0].UnitId);

                        } else {
                            $("#Hsn_Sac").val("");
                            $("#GST").val("0");
                            $("#hdnIs_SameState").val(0);
                            $("#GST").change();
                        }
                    });
                }
            },
            minLength: 1
        });

        $("#drpPartyId").change(function () {
            let PartyId = parseInt($('#drpPartyId').val());
            $("#PartyId").val(PartyId);
            IMSC.ajaxCall("GET", "/Material/GetState?PartyId=" + PartyId + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                var result = JSON.parse(d);
                $("#StateId").empty();
                if (result !== null && result.length > 0) {
                    $("#StateId").append($("<option></option>").val("0").html("Select State"));
                    $.each(result, function (index, value) {
                        $("#StateId").append($("<option></option>").val(value.State_Id).html(value.StateName));
                    });
                } else {
                    $("#StateId").append($("<option></option>").val("").html("Select State"));
                }
            });
        });

        $("#SearchParty").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchParty?Party=" + request.term + "&OfficeId=" + Number($("#OfficeId").val()) + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.Title, value: item.Party_Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
                $("#SearchParty").val(i.item.label);
                let PartyId = parseInt(i.item.value);
                $("#PartyId").val(PartyId);
                IMSC.ajaxCall("GET", "/Material/GetState?PartyId=" + PartyId + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    $("#StateId").empty();
                    if (result !== null && result.length > 0) {
                        $("#StateId").append($("<option></option>").val("0").html("Select State"));
                        $.each(result, function (index, value) {
                            $("#StateId").append($("<option></option>").val(value.State_Id).html(value.StateName));
                        });
                    } else {
                        $("#StateId").append($("<option></option>").val("").html("Select State"));
                    }
                });
            },
            minLength: 1
        });

        $("#StateId").change(function () {
            $("#SupplyStateId").val(parseInt($(this).val()));
        });

        $("#drpParty").change(function () {
            let Party_Id = parseInt($('#drpParty').val());
            $("#PartyId").val(Party_Id);
            IMSC.ajaxCall("GET", "/Material/GetInvoice?Party_Id=" + Party_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                var result = JSON.parse(d);
                $("#drpInvoice").empty();
                $("#PurchaseId").val(0);
                if (result.length > 0) {
                    if (result !== null) {
                        $.each(result, function (index, value) {
                            $("#drpInvoice").append($("<option></option>").val(value.Purchase_Id).html(value.Invoice_No));
                        });
                        $("#drpInvoice").change();
                    }
                } else {
                    $("#drpInvoice").empty();
                    $("#PurchaseId").val(0);
                }
            });
        }).change();

        $("#drpInvoice").change(function () {
            $("#PurchaseId").val(parseInt($("#drpInvoice").val()));
        });

        $("#btnSearch").click(function (e) {
            e.preventDefault();
            let Purchase_Id = parseInt($("#PurchaseId").val());
            if (Purchase_Id > 0) {
                IMSC.ajaxCall("GET", "/Material/GetMatrialPurchase?Purchase_Id=" + Purchase_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var res = JSON.parse(d);
                    if (res !== null) {
                        var result = res.Table;
                        $("#StateId").empty();
                        $("#StateId").append($("<option></option>").val(0).html("Select State"));
                        $.each(res.Table1, function (index, value) {
                            $("#StateId").append($("<option></option>").val(value.State_Id).html(value.StateName));
                        });
                        $("#PartyId").val(result[0].Party_Id);
                        $("#StateId").val(result[0].SupplyState_Id);
                        $("#IsUpdateMaterialPurchase").val(1);
                        $("#OfficeId").val(result[0].Office_Id);
                        $("#InvoiceNo").val(result[0].Invoice_No);
                        $("#PartyId").val(result[0].Party_Id);
                        $("#SearchParty").val(result[0].PartyName);
                        $("#Purchase_Ref").val(result[0].Purchase_Ref);
                        $("#TransactionDate").val(result[0].Transaction_Date);
                        $("#SupplyStateId").val(result[0].SupplyState_Id);
                        $("#Remarks").val(result[0].Remarks);
                        $("#PurchaseId").val(result[0].Purchase_Id)
                        $("#tbodyid").empty(Purchase_Id);
                        let amount = 0;
                        $.each(result, function (index, value) {
                            amount += parseFloat(value.Total_Amount);
                            //Get the reference of the Table's TBODY element.
                            var tBody = $("#tblMaterialPurchase > TBODY")[0];
                            //Add Row.
                            var row = tBody.insertRow(-1);
                            $(row).attr('id', 'trRow_' + value.Line_Id);
                            //Add Button cell.
                            cell = $(row.insertCell(-1));
                            let btnRemove = `<a class="btn btn-danger" onclick="Remove(this);" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
                            cell.append(btnRemove);
                            //Add Item cell.
                            var cell = $(row.insertCell(-1));
                            cell.html(value.ItemTitle);
                            cell.attr('data-item-id', value.Item_Id);
                            cell.attr('data-line-Id', value.Line_Id);
                            //Add Hsn_Sac cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.HSN_SAC);
                            //Add Quantity cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Quantity);
                            //Add Unit cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.UnitTitle);
                            cell.attr('data-Unit_Id', value.Unit_Id);
                            //Add Rate cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Rate);
                            //Add Amount cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Amount);
                            //Add Discount_1 cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Discount_1);
                            //Add Discount_2 cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Discount_2);
                            //Add Taxable_Amount cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Taxable_Amount);
                            //Add GST cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.GST);
                            //Add CGST cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.CGST);
                            //Add SGST cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.SGST);
                            //Add IGST cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.IGST);
                            //Add Total_Amount cell.
                            cell = $(row.insertCell(-1));
                            cell.attr('data-row-Id', value.Line_Id);
                            cell.html(value.Total_Amount);
                        });
                        $("#lblTotalAmount").text("Total : " + amount.toFixed(2));
                        $("#divDeleteMatrial").css('display', 'block');
                        $("td").each(function () {
                            $(this).addClass("tbl-css");
                        });
                    }
                });
            } else {
                $('#alertModal').modal('show');
                $('#msg').html("Please select inovice.");
            }
        });

        $("#Quantity,#Rate,#Discount_1,#Discount_2,#GST").change(function () {
            Calculate();
        });

        $("#btnResetForm").click(function (e) {
            e.preventDefault();
            $("#drpParty").val($("#drpParty option:first").val());
            $('#drpParty').select2().trigger('change');
            $("#drpInvoice").select2({
                placeholder: "Select Invoice",
                allowClear: true
            });
            $("#PurchaseId").val(0);
            $("#IsUpdateMaterialPurchase").val(false);
            $("#PurchaseLine").val("");
            $("#SupplyStateId").val("");
            $("#PartyId").val("");
            $("#OfficeId").val($("#OfficeId option:first").val());
            $("#InvoiceNo").val("");
            $("#PartyId").val("");
            $("#SearchParty").val("");
            $("#Purchase_Ref").val("");
            $("#TransactionDate").val("");
            $("#StateId").empty();
            $("#Item_Id").val("");
            $('#ItemSearch').val();
            $("#Hsn_Sac").val("");
            $("#Quantity").val("");
            $("#Unit_Id").val($("#Unit_Id option:first").val());
            $("#Rate").val("");
            $("#Amount").val("");
            $("#Discount_1").val("0");
            $("#Discount_2").val("0");
            $("#Taxable_Amount").val("");
            $("#GST").val("0");
            $("#CGST").val("");
            $("#SGST").val("");
            $("#IGST").val("");
            $("#Total").val("");
            $("#hdnTotalAmount").val("");
            $("#hdnRowId").val("");
            $("#btnAdd").val("Add");
            $("#hdnIs_SameState").val("");
            $("#tblMaterialPurchase TBODY").empty();
            $("#divDeleteMatrial").css('display', 'none');
        });

        $("#btnDelete").click(function () {
            let Purchase_Id = parseInt($("#PurchaseId").val());
            if (Purchase_Id > 0) {
                window.location.href = "/Material/DeleteMatrialPurchase?Purchase_Id=" + Purchase_Id + "&AppToken=" + scope.AppToken;
            } else {
                $('#alertModal').modal('show');
                $('#msg').html("Please select inovice.");
            }
        });

        function Calculate() {
            let bIsDiscount = false;
            let quantity = parseFloat($("#Quantity").val() !== "" ? $("#Quantity").val() : 0);
            let rate = parseFloat($("#Rate").val() !== "" ? $("#Rate").val() : 0);
            let amount = 0;
            let disAmu = 0;
            let gst = 0;
            let discount_1 = 0;
            let discount_2 = 0;
            let discountAmount = 0;
            let totalAmount = 0;
            if (quantity > 0 && rate > 0) {
                amount = quantity * rate;
                $("#Amount").val(amount.toFixed(2));
            }
            if (amount > 0) {
                discount_1 = parseFloat($("#Discount_1").val() !== "" ? $("#Discount_1").val() : 0);
                discount_2 = parseFloat($("#Discount_2").val() !== "" ? $("#Discount_2").val() : 0);
                if (discount_1 > 0 || discount_2 > 0) {
                    bIsDiscount = true;
                    disAmu = amount * (discount_1 + discount_2) / 100;
                    discountAmount = amount - disAmu;
                    gst = discountAmount * parseFloat($("#GST").val()) / 100;
                } else {
                    bIsDiscount = false;
                    gst = amount * parseFloat($("#GST").val()) / 100;
                }

                let is_SameState = $("#hdnIs_SameState").val() === "1" ? true : false;
                if (is_SameState) {
                    $("#CGST").val(parseFloat(gst / 2).toFixed(2));
                    $("#SGST").val(parseFloat(gst / 2).toFixed(2));
                    $("#IGST").val(parseFloat(0).toFixed(2));

                } else {
                    $("#CGST").val(parseFloat(0).toFixed(2));
                    $("#SGST").val(parseFloat(0).toFixed(2));
                    $("#IGST").val(parseFloat(gst).toFixed(2));
                }
                if (bIsDiscount) {
                    totalAmount = discountAmount + gst;
                    $("#Taxable_Amount").val(discountAmount.toFixed(2));
                } else {
                    totalAmount = amount + gst;
                    $("#Taxable_Amount").val(amount.toFixed(2));
                }
                $("#Total").val(totalAmount.toFixed(2));
                $("#hdnTotalAmount").val(totalAmount.toFixed(2));
            }
        }
        function Remove(button) {
            //Determine the reference of the Row using the Button.
            $("#IsUpdateMaterialPurchase").val(0);
            var row = $(button).closest("TR");
            var name = $("TD", row).eq(0).text();
            if (confirm("Do you want to remove this purchase.", "Remove")) {
                //Get the reference of the Table.
                var table = $("#tblMaterialPurchase")[0];
                //Delete the Table row using it's Index.
                table.deleteRow(row[0].rowIndex);
            }
            let sumOfTotal = 0;
            $("#tblMaterialPurchase TBODY TR").each(function () {
                var row = $(this);
                let td = row.find("TD");
                sumOfTotal += parseFloat(td[13].innerText);
            });
            $("#PurchaseAmount").val(sumOfTotal.toFixed(2));
            $("#lblTotalAmount").text("Total : " + sumOfTotal.toFixed(2));
        };
    };
    scope.materialOrderCreation = function () {
        $("#drpParty").select2();
        $("#drpOrder").select2();

        $("body").on("click", "#btnReset", function (e) {
            e.preventDefault();
            $("#Item_Id").val("");
            $('#ItemSearch').val("");
            $("#Unit_Id").val($("#Unit_Id option:first").val());
            $('#Unit_Id').select2().trigger('change');
            $("#lblAvailableQty").text("");
            $("#lblLastRate").text("");
            $("#lblLastDist1").text("");
            $("#lblLastDist2").text("");
            $("#lblListPrice").text("");
            $("#txtOrderQty").val("");
            $("#txtOrderRate").val("");
            $("#txtRemark").val("");
            $("#btnAdd").val("Add");
        });

        $("#btnAdd").click(function (e) {
            e.preventDefault();
            $("#IsUpdate").val(1);
            let bAdded = true;
            let msg = "";
            let totalAmount = 0.0;
            if (bAdded) {
                if (parseInt($("#Item_Id").val()) === 0 || $("#txtOrderQty").val() === "" || $("#txtOrderRate").val() === "") {
                    msg = "Please select all mandatory fields to add material in list.";
                    bAdded = false;
                }
            }
            if (bAdded) {
                if (parseInt($("#txtOrderQty").val()) <= parseInt($("#lblAvailableQty").text())) {
                    bAdded = true;
                } else {
                    msg = "Order qty grater then available qty!!!";
                    bAdded = false;
                }
            }
            if (bAdded) {
                //Reference the Party and Location ddl.
                if ($("#tblOrderCreation TBODY TR").length > 0) {
                    $("#tblOrderCreation TBODY TR").each(function () {
                        var td = $(this).closest("TR").find('td');
                        let itemId = parseInt(td[1].attributes["data-item-id"].value);
                        if (parseInt($("#Item_Id").val()) === itemId) {
                            msg = "Order is already mapped!!!";
                            bAdded = false;
                        }
                        if ($("#hdnRowId").val() !== "" && parseInt($("#hdnRowId").val()) > 0) {
                            if (itemId === parseInt($("#hdnRowId").val())) {
                                bAdded = true;
                            }
                        }
                    });
                }
            }

            if (bAdded) {
                if ($("#hdnRowId").val() !== "" && parseInt($("#hdnRowId").val()) > 0) {
                    let trId = "#trRow_" + parseInt($("#hdnRowId").val());
                    var row = $(trId).closest("TR").find('td');
                    row[1].textContent = $("#ItemSearch").text();
                    row[1].setAttribute('data-item-id', $("#Item_Id").val());
                    row[1].setAttribute('data-line-Id', $("#hdnLineId").val());
                    row[1].setAttribute('data-index', $("#Item_Id").val());
                    row[2].textContent = $("#Unit_Id").val();
                    row[3].textContent = $("#lblAvailableQty").text();
                    row[4].textContent = $("#lblLastRate").text();
                    row[5].textContent = $("#lblLastDist1").text();
                    row[6].textContent = $("#lblLastDist2").text();
                    row[7].textContent = $("#lblListPrice").text();
                    row[8].textContent = $("#txtOrderQty").val();
                    row[9].textContent = $("#txtOrderRate").val();
                    row[10].textContent = parseFloat(parseFloat($("#txtOrderQty").val()) * parseFloat($("#txtOrderRate").val())).toFixed(2);
                    row[11].textContent = $("#txtRemark").val();
                    $("#hdnRowId").val("");
                    $("#hdnLineId").val("");
                    $("#btnAdd").val("Add");
                } else {
                    //Get the reference of the Table's TBODY element.
                    var tBody = $("#tblOrderCreation > TBODY")[0];
                    //Add Row.
                    var row = tBody.insertRow(-1);
                    //Edit Button cell.
                    cell = $(row.insertCell(-1));
                    let btnRemove = `<a class="btn btn-danger" style="padding: 2px 5px 2px 5px; margin-bottom:0px" onclick="Remove(this);" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
                    cell.append(btnRemove);
                    //Add Item cell.
                    cell = $(row.insertCell(-1));
                    cell.html($("#ItemSearch").text());
                    cell.attr('data-item-id', $("#Item_Id").val());
                    cell.attr('data-line-id', 0);
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add Unit.
                    cell = $(row.insertCell(-1));
                    cell.html($("#Unit_Id option:selected").text());
                    cell.attr('data-unit-id', $("#Unit_Id").val());
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add AvailableQty.
                    cell = $(row.insertCell(-1));
                    cell.html($("#lblAvailableQty").text());
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add Last Rate.
                    cell = $(row.insertCell(-1));
                    cell.html($("#lblLastRate").text());
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add LastDist1 cell.
                    cell = $(row.insertCell(-1));
                    cell.html($("#lblLastDist1").text());
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add LastDist2 cell.
                    cell = $(row.insertCell(-1));
                    cell.html($("#lblLastDist2").text());
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add ListPrice cell.
                    cell = $(row.insertCell(-1));
                    cell.html($("#lblListPrice").text() !== "" ? $("#lblListPrice").text() : 0);
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add txtOrderQty cell.
                    cell = $(row.insertCell(-1));
                    cell.html($("#txtOrderQty").val());
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add txtOrderRate cell.
                    cell = $(row.insertCell(-1));
                    cell.html($("#txtOrderRate").val());
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add amount cell.
                    cell = $(row.insertCell(-1));
                    cell.html(parseFloat(parseFloat($("#txtOrderQty").val()) * parseFloat($("#txtOrderRate").val())).toFixed(2));
                    cell.attr('data-index', $("#Item_Id").val());
                    //Add txtRemark cell.
                    cell = $(row.insertCell(-1));
                    cell.html($("#txtRemark").val());
                    cell.attr('data-index', $("#Item_Id").val());
                }

                //Set Default
                $("#Item_Id").val("");
                $('#ItemSearch').val("");
                $("#Unit_Id").val($("#Unit_Id option:first").val());
                $('#Unit_Id').select2().trigger('change');
                $("#lblAvailableQty").text("");
                $("#lblLastRate").text("");
                $("#lblLastDist1").text("");
                $("#lblLastDist2").text("");
                $("#lblListPrice").text("");
                $("#txtOrderQty").val("");
                $("#txtOrderRate").val("");
                $("#txtRemark").val("");
                $("#hdnRowId").val("");
                $("#tblOrderCreation TBODY TR").each(function () {
                    totalAmount += parseFloat($(this).find('td')[9].innerText);
                });
                $("td").each(function () {
                    $(this).addClass("tbl-css");
                });
                $("#TotalAmount").val(totalAmount.toFixed(2));
                $("#lblTotalAmount").text("Total : " + totalAmount.toFixed(2));
            }

            if (bAdded) {
                return true;
            } else {
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
        });

        $("body").on("click", "#btnSubmit", function () {
            //Loop through the Table rows and build a JSON array.
            let OrderLineValues = [];
            let isValid = true;
            totalAmount = 0.0;
            let msg = "";
            if ($("#tblOrderCreation TBODY TR").length > 0) {
                $("#tblOrderCreation TBODY TR").each(function () {
                    let row = $(this).find('td');
                    let oMapping = {};

                    oMapping.Line_Id = row.eq(1).attr('data-line-Id');
                    oMapping.Item_Id = row.eq(1).attr('data-item-id');
                    oMapping.UnitId = row.eq(2).attr('data-unit-id');
                    oMapping.Available_Qty = row[3].innerText;
                    oMapping.Last_Rate = row[4].innerText;
                    oMapping.Last_Discount_1 = row[5].innerText;
                    oMapping.Last_Discount_2 = row[6].innerText;
                    oMapping.Last_Price = row[7].innerText;
                    oMapping.Order_Qty = row[8].innerText;
                    oMapping.Order_Rate = row[9].innerText;
                    oMapping.Amount = parseFloat(row[8].innerText) * parseFloat(row[9].innerText);
                    totalAmount += parseFloat(row[8].innerText) * parseFloat(row[9].innerText);
                    oMapping.Remarks = row[11].innerText
                    oMapping.IsUpdate = $("#IsUpdate").val();
                    OrderLineValues.push(oMapping);
                });
                $("#TotalAmount").val(totalAmount.toFixed(2));
                $("#MaterialLine").val(JSON.stringify(OrderLineValues));
            } else {
                msg = "Please add at least 1 matrial order.";
                isValid = false;
            }
            if (parseInt($("#PartyId").val()) === 0) {
                msg = "Please fill all mandatory fields.";
                isValid = false;
            }

            if (isValid) {
                return true;
            } else {
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
        });

        $("#ItemSearch").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchItem?Item=" + request.term + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.Title, value: item.Item_Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
                $("#ItemSearch").val(i.item.label);
                let Item_Id = parseInt(i.item.value);
                $("#Item_Id").val(Item_Id);
                if (Item_Id > 0) {
                    IMSC.ajaxCall("GET", "/Material/GetItemDetail?Item_Id=" + Item_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                        var result = JSON.parse(d);
                        if (result != null && result.length > 0) {
                            $("#lblAvailableQty").text(result[0].Available_Qty);
                            $("#lblLastRate").text(result[0].Last_Rate);
                            $("#lblLastDist1").text(result[0].Last_Disc_1);
                            $("#lblLastDist2").text(result[0].Last_Disc_2);
                            $("#lblListPrice").text(result[0].List_Price);
                            $("#Unit_Id").val(result[0].UnitId);
                            $("#hdnRowId").val("");
                        }
                    });
                }
            },
            minLength: 1
        });

        $("#SearchParty").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchPartyOrderCreation?Party=" + request.term + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.Title, value: item.Party_Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
                $("#SearchParty").val(i.item.label);
                let PartyId = parseInt(i.item.value);
                $("#PartyId").text(PartyId);
                if (PartyId > 0) {
                    IMSC.ajaxCall("GET", "/Material/GetParty?PartyId=" + PartyId + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                        var result = JSON.parse(d);
                        if (result !== null && result.length > 0) {
                            $("#lblContactNo").text(result[0].ContactNo);
                            $("#lblOverDueAmount").text(result[0].OverdueAmount);
                            $("#lblCashBalance").text(result[0].CashBalance);
                            $("#lblLedgerBalance").text(result[0].LedgerBalance);
                            $("#lblPartyCategory").text(result[0].Category === null ? "" : result[0].Category);
                            $("#txtAddress").val(result[0].Address);
                        }
                    });
                } else {
                    $("#lblContactNo").text("");
                    $("#lblOverDueAmount").text("");
                    $("#lblCashBalance").text("");
                    $("#lblLedgerBalance").text("");
                    $("#lblPartyCategory").text("");
                    $("#txtAddress").val("");
                }
            },
            minLength: 1
        });

        $("#drpParty").change(function () {
            let Party_Id = parseInt($('#drpParty').val());
            let PO_No = $('#PONo').val();
            $("#PartyId").val(Party_Id);
            IMSC.ajaxCall("GET", "/Material/GetOrderInvoice?Party_Id=" + Party_Id + "&PO_No=" + PO_No + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                var result = JSON.parse(d);
                $("#drpOrder").empty();
                $("#drpOrder").append($("<option></option>").val("").html("Select Invoice"));
                if (result !== null) {
                    $.each(result, function (index, value) {
                        $("#drpOrder").append($("<option></option>").val(value.PO_Id).html(value.PO_No));
                    });
                } else {
                    $("#POId").val(0);
                    $("#PONo").val("");
                }
            });
        }).change();

        $("#drpOrder").change(function () {
            $("#POId").val(parseInt($("#drpOrder").val()));
            $("#PO_No").val(parseInt($("#drpOrder").text()));
        });

        $("#btnSearch").click(function (e) {
            e.preventDefault();
            let PO_Id = parseInt($("#POId").val());
            if (PO_Id > 0) {
                IMSC.ajaxCall("GET", "/Material/SearchInvoice?PO_Id=" + PO_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var res = JSON.parse(d);
                    if (res !== null) {
                        var result = res.Table;
                        let totalAmount = 0.0;
                        $("#POId").val(result[0].PO_Id);
                        $("#PONo").val(result[0].PO_No);
                        $("#PartyId").val(result[0].Party_Id);
                        $("#SearchParty").val(result[0].PartyName);
                        $("#lblLedgerBalance").text(res.Table1[0].LedgerBalance);
                        $("#lblCashBalance").text(res.Table1[0].CashBalance);
                        $("#lblOverDueAmount").text(res.Table1[0].OverdueAmount);
                        $("#lblPartyCategory").text(res.Table1[0].Category === null ? "" : res.Table1[0].Category);
                        $("#txtAddress").val(res.Table1[0].Address);
                        $("#lblContactNo").text(res.Table1[0].ContactNo);
                        $("#tbodyid").empty();
                        $.each(res.Table, function (index, value) {

                            //Get the reference of the Table's TBODY element.
                            var tBody = $("#tblOrderCreation > TBODY")[0];
                            //Add Row.
                            var row = tBody.insertRow(-1);
                            $(row).attr('id', 'trRow_' + value.Item_Id);
                            //Edit Button cell.
                            cell = $(row.insertCell(-1));
                            let btnEdit = `<a class="btn btn-success" onclick="Edit(this);" style="visibility: hidden;padding: 2px 5px 2px 5px; margin-bottom:0px;margin-right: 5px;" href="javascript:void(0)"><i class="fa fa-edit"></i></a>`;
                            let btnRemove = `<a class="btn btn-danger" onclick="Remove(this);" style="padding: 2px 5px 2px 5px; margin-bottom:0px" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
                            cell.append(btnEdit).append(btnRemove);
                            //Add Item cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.ItemName);
                            cell.attr('data-item-id', value.Item_Id);
                            cell.attr('data-line-id', value.Line_Id);
                            cell.attr('data-index', value.Item_Id);
                            //Add Unit
                            cell = $(row.insertCell(-1));
                            cell.html(value.Unit);
                            cell.attr('data-unit-id', value.UnitId);
                            cell.attr('data-index', value.UnitId);
                            //Add AvailableQty.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Available_Qty);
                            cell.attr('data-index', value.Item_Id);
                            //Add Last Rate.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Last_Rate);
                            cell.attr('data-index', value.Item_Id);
                            //Add LastDist1 cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Last_Discount_1);
                            cell.attr('data-index', value.Item_Id);
                            //Add LastDist2 cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Last_Discount_2);
                            cell.attr('data-index', value.Item_Id);
                            //Add ListPrice cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.List_Price !== undefined ? value.List_Price : 0);
                            cell.attr('data-index', value.Item_Id);
                            //Add txtOrderQty cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Order_Qty);
                            cell.attr('data-index', value.Item_Id);
                            //Add txtOrderRate cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Order_Rate);
                            cell.attr('data-index', value.Item_Id);
                            //Add amount cell.
                            cell = $(row.insertCell(-1));
                            cell.html(parseFloat(value.Amount).toFixed(2));
                            cell.attr('data-index', value.Item_Id);
                            totalAmount += value.Amount;
                            //Add txtRemark cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.LineRemarks);
                            cell.attr('data-index', value.Item_Id);
                        });

                        $("#lblTotalAmount").text("Total : " + parseFloat(totalAmount).toFixed(2));
                        $("#divDeleteMatrial").css('display', 'block');
                        $("td").each(function () {
                            $(this).addClass("tbl-css");
                        });
                    }
                });
            } else {
                $('#alertModal').modal('show');
                $('#msg').html("Please select inovice.");
            }
        });

        $("#btnResetForm").click(function (e) {
            e.preventDefault();
            $("#Item_Id").val("");
            $('#ItemSearch').val("");
            $("#Unit_Id").val($("#Unit_Id option:first").val());
            $('#Unit_Id').select2().trigger('change');
            $("#lblAvailableQty").text("");
            $("#lblLastRate").text("");
            $("#lblLastDist1").text("");
            $("#lblLastDist2").text("");
            $("#lblListPrice").text("");
            $("#txtOrderQty").val("");
            $("#txtOrderRate").val("");
            $("#txtRemark").val("");
            $("#PartyId").val("");
            $('#SearchParty').val("");
            $("#lblContactNo").text("");
            $("#lblOverDueAmount").text("");
            $("#lblCashBalance").text("");
            $("#lblLedgerBalance").text("");
            $("#lblPartyCategory").text("");
            $("#txtAddress").val("");
            $("#hdnRowId").val("");
            $("#btnAdd").val("Add");
            $("#tblOrderCreation TBODY").empty();
            $("#divDeleteMatrial").css('display', 'none');
            $("#lblTotalAmount").text("Total : 0.00");
            $("#drpOrder").val($("#drpOrder option:first").val());
            $('#drpOrder').select2().trigger('change');
        });

        $("#btnDelete").click(function () {
            let PO_Id = parseInt($("#POId").val());
            if (PO_Id > 0) {
                window.location.href = "/Material/DeleteOrder?PO_Id=" + PO_Id + "&AppToken=" + scope.AppToken;
            } else {
                $('#alertModal').modal('show');
                $('#msg').html("Please select invoice.");
            }
        });

        function Edit(button) {
            //Determine the reference of the Row using the Button.
            $("#btnAdd").val("Update");
            $("#IsUpdate").val(1);
            var row = $(button).closest("TR").find('td');
            $("#Item_Id").val("");
            $('#ItemSearch').val("");
            $("#Unit_Id").val(parseInt(row.eq(2).attr('data-unit-id')));
            $('#Unit_Id').select2().trigger('change');
            $("#txtOrderQty").val(row[8].innerText);
            $("#txtOrderRate").val(row[9].innerText);
            $("#txtRemark").val(row[11].innerText);
            $("#hdnRowId").val(parseInt(row.eq(1).attr('data-item-id')));
            $("#hdnLineId").val(parseInt(row.eq(1).attr('data-line-id')));

        };

        function Remove(button) {
            //Determine the reference of the Row using the Button.
            $("#IsUpdate").val(1);
            var row = $(button).closest("TR");
            //var name = $("TD", row).eq(0).text();
            if (confirm("Do you want to remove this purchase.", "Remove")) {
                //Get the reference of the Table.
                var table = $("#tblOrderCreation")[0];
                //Delete the Table row using it's Index.
                table.deleteRow(row[0].rowIndex);
            }
            let sumOfTotal = 0;
            $("#tblOrderCreation TBODY TR").each(function () {
                sumOfTotal += parseFloat($(this).find('td')[9].innerText);
            });
            $("#TotalAmount").val(sumOfTotal.toFixed(2));
            $("#lblTotalAmount").text("Total : " + sumOfTotal.toFixed(2));
        };
    };
    return scope;
})(IMSMaterial || {});
