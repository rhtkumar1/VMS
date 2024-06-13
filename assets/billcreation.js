var IMSMaterialSales = (function (scope) {
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
                msg = "Please select office!!!";
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            } else if (parseInt($("#PartyId").val()) === 0) {
                isValid = false;
                msg = "Please fill party!!!";
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            } else if (parseInt($("#StateId").val()) === 0) {
                isValid = false;
                msg = "Please select state!!!";
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
            //else if ($("#Purchase_Ref").val() === "") {
            //    isValid = false;
            //    msg = "Please fill purchase ref!!!";
            //    $('#alertModal').modal('show');
            //    $('#msg').html(msg);
            //    return false;
            //}
            else if ($("#TransactionDate").val() === "") {
                isValid = false;
                msg = "Please fill transaction date!!!";
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }

            if (isValid) {
                if ($("#tblMaterialSales TBODY TR").length > 0) {
                    $("#tblMaterialSales TBODY TR [id^='hdnItemId_']").each(function () {
                        /*debugger;*/
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
                        }
                        else {
                            if (isItemType === 16) {
                                isValid = true;
                            } else {
                                if (qty !== NaN && qty > 0 && qty > availableQty && parseInt($("#hdnLineId_" + index).val()) == 0) {
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
                            oMapping.Quantity = ($("#txtOrder_Qty_" + index).val() === "" ? "0" : $("#txtOrder_Qty_" + index).val());
                            oMapping.Rate = $("#txtRate_" + index).val();
                            oMapping.LastRate = ($("#lblLastRate_" + index).val() === "" ? "0" : $("#lblLastRate_" + index).val());
                            oMapping.Amount = $("#lblAmount_" + index).text();
                            oMapping.Discount_1 = ($("#txtDiscount1_" + index).val() === "" ? "0" : $("#txtDiscount1_" + index).val());
                            oMapping.Discount_2 = ($("#txtDiscount2_" + index).val() === "" ? "0" : $("#txtDiscount2_" + index).val());
                            oMapping.Taxable_Amount = $("#lblTaxable_Amount_" + index).text();
                            oMapping.Discount_1_Amount = ($("#hdnDiscount_1_Amt_" + index).val() === "" ? "0" : $("#hdnDiscount_1_Amt_" + index).val());
                            oMapping.Discount_2_Amount = ($("#hdnDiscount_2_Amt_" + index).val() === "" ? "0" : $("#hdnDiscount_2_Amt_" + index).val());
                            oMapping.GST = $("#lblGST_" + index).text();
                            oMapping.CGST = $("#lblCGST_" + index).text();
                            oMapping.SGST = $("#lblSGST_" + index).text();
                            oMapping.IGST = $("#lblIGST_" + index).text();
                            oMapping.Total_Amount = $("#lblTotal_Amount_Row_" + index).text();

                            oMapping.PO_Id = $("#hdnPOId_" + index).val();
                            oMapping.POLine_Id = $("#hdnPOLineId_" + index).val();
                            oMapping.IsUpdate = $("#IsUpdateMaterialSales").val();
                            //oMapping.Order_OfficeID = 0;
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
            ResetStockQty(false);
            ResetSaleOrder(false);
            $("#AvailableQuantity").val("")
        });

        $("#btnAdd").click(function (e) {
            e.preventDefault();
            let bAdded = true;
            let msg = "";
            let amount = 0.0;
            let isSpecialService = false;
            while (true) {
                if (Number($("#hdnItemType").val()) === 16) {
                    isSpecialService = true;
                }
                if (parseInt($("#Item_Id").val()) === 0 || $("#Item_Id").val() === "") {
                    msg = "Please select item.";
                    bAdded = false;
                    break;
                }
                if (!isSpecialService) {
                    if ($("#Quantity").val() === "") {
                        msg = "Please enter quantity.";
                        bAdded = false;
                        break;
                    }
                } else {
                    $("#Unit_Id").val(0)
                }
                if (parseInt($("#Unit_Id").val()) === 0 && !isSpecialService) {
                    msg = "Please select unit.";
                    bAdded = false;
                    break;
                }
                if ($("#Rate").val() === "") {
                    msg = "Please enter rate.";
                    bAdded = false;
                    break;
                }

                if (!isSpecialService && parseInt($("#Quantity").val()) > parseInt($("#AvailableQuantity").val())) {
                    msg = "Please correct quantity that must not be greater than Available Quantity!!!";
                    bAdded = false;
                    $("#Quantity").focus();
                    break;
                }
                if ($("#ddlAvailableSaleOrder").val() != "") {
                    var TempPo = $("#ddlAvailableSaleOrder option:selected");
                    let POText = TempPo.text();
                    if (TempPo.val() != "0" ) {
                        let POQty = parseInt(POText.split(":")[1]);
                        if (!isSpecialService && parseInt($("#Quantity").val()) > POQty) {
                            msg = "Please correct quantity that must not be greater than PO Quantity!!!";
                            bAdded = false;
                            $("#Quantity").focus();
                            break;
                        }
                    }
                }
                break;
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
                let htmlEditBtn = `<a class="btn btn-danger" style="padding: 2px 5px 2px 5px; margin-bottom:0px" data-item-index="${itemId}" onclick="Remove(this);" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
                cell = $(row.insertCell(-1));
                cell.append(htmlEditBtn);
                //Add Item cell.
                let BPoId = "0";
                let BPoLineId = "0";
                var BTempPo = $("#ddlAvailableSaleOrder option:selected");
                if (BTempPo.val() != "0") {
                    var BPOText = BTempPo.val();
                    var BPORecord = BPOText.split(":");
                    BPoId = BPORecord[0];
                    BPoLineId = BPORecord[1];
                }
                let lblItem = `<label id="lblItem_${itemId}">${$("#ItemSearch").val()}</label>
                              <input type="hidden" id="hdnItemId_${itemId}" name="hdnItemId_${itemId}" value="${itemId}" />
                              <input type="hidden" id="hdnLineId_${itemId}" name="hdnLineId_${itemId}" value="${0}" />
                              <input type="hidden" id="hdnPOLineId_${itemId}" name="hdnPOLineId_${itemId}" value="${BPoLineId}" />
                              <input type="hidden" id="hdnPOId_${itemId}" name="hdnLineId_${itemId}" value="${BPoId}" />
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
                let lblUnit = `<label id="lblUnit_${itemId}" data-Unit_Id="${!isSpecialService ? $("#Unit_Id").val() : "0"}" data-index="${itemId}" >${!isSpecialService ? $("#Unit_Id :selected").text() : ""}</label>`;
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
                //Add Remarks cell.
                let Test = "Na";
                let lblRemarks_ = `<label id="lblRemarks_Row_${itemId}">${Test}</label>`;
                cell = $(row.insertCell(-1));
                cell.append(lblRemarks_);

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
                ResetStockQty(false);
                ResetSaleOrder(false);
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

        $(document).on('change', '#Client_Id', function () {
            debugger;
            var id = $('#Client_Id').val();
            var Client_Id = 2300;
            IMSC.ajaxCall("GET", "/Material/GetBillcreationdata?Client_Id=" + Client_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                var res = JSON.parse(d);
                if (res !== null) {
                    BindGrid(res, 1, 0);
                }
            });
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
                            $("#VoucherNo").val(result[0].Voucher_No);
                            $("#SearchParty").val(result[0].PartyName);
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
            //$("#OfficeId").val($("#drpPartyId option:first").val());
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

        $("#StateId").change(function () {
            var Office_Id = parseInt($("#OfficeId").val());
            var SupplyState_Id = parseInt($("#StateId").val());
            if (SupplyState_Id > 0) {
                IMSC.ajaxCall("GET", "/Material/GetPOData?POId=" + 0 + "&Office_Id=" + Office_Id + "&SupplyState_Id=" + SupplyState_Id + "&AppToken=" + scope.AppToken + "&Party_Id=" + Number($("#PartyId").val()), {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length > 0) {
                        $("#tbodyid").empty();
                        BindGrid(result, 0, 1);
                    }
                });
            }
        });
        $("#ddlAvailableQty").change(function () {
            var Stockvalue = $("#ddlAvailableQty option:selected").text();
            var tempval;
            try {
                tempval = parseInt(Stockvalue.split("-")[1].split(")")[0]);
            }
            catch (ex) {
                tempval = 0;
            }
            $("#AvailableQuantity").val(tempval);

        })
        $("#SearchParty").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchParty?Party=" + request.term + "&OfficeId=" + Number($("#Billing_OfficeId").val()) + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
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
                    $("#tbodyid").empty();
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
                            //if (result.Table2 !== undefined && result.Table2.length > 0) {
                            //    $("#tbodyid").empty();
                            //    BindGrid(result.Table2, 0, 1);
                            //}
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
                            //$(result).each(function (index, value) {
                            //    $("[id^='hdnItemId_']").each(function () {
                            //        if (parseInt(value.Item_Id) === parseInt($("#" + this.id).val())) {
                            //            isValid = false;
                            //            return false;
                            //        }
                            //    });
                            //});

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
            minLength: 1,
            delay: 1000,
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
                var isValid = true;
                if (parseInt($("#OfficeId").val()) === 0) {
                    isValid = false;
                    msg = "Please select office!!!";
                    $('#alertModal').modal('show');
                    $('#msg').html(msg);
                    $("#ItemSearch").val("");
                    return false;
                } else if (parseInt($("#PartyId").val()) === 0) {
                    isValid = false;
                    msg = "Please select party!!!";
                    $('#alertModal').modal('show');
                    $('#msg').html(msg);
                    $("#ItemSearch").val("");
                    return false;
                } else if (parseInt($("#StateId").val()) === 0) {
                    isValid = false;
                    msg = "Please select state!!!";
                    $('#alertModal').modal('show');
                    $('#msg').html(msg);
                    $("#ItemSearch").val("");
                    return false;
                }
                $("#ItemSearch").val(i.item.label);
                let Item_Id = parseInt(i.item.value);
                $("#Item_Id").val(Item_Id);
                let Office_Id = parseInt($("#OfficeId").val());
                let Party_Id = parseInt($("#PartyId").val());
                let P_State_Id = parseInt($("#SupplyStateId").val());
                if (Item_Id > 0 && isValid) {
                    IMSC.ajaxCall("GET", "/Material/GetHSN_Detail_Sale?Item_Id=" + Item_Id + "&Office_Id=" + Office_Id + "&P_State_Id=" + P_State_Id + "&Party_Id=" + Party_Id + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                        var Recivedresult = JSON.parse(d);
                        var result = Recivedresult.ItemDetail
                        var POresult = Recivedresult.PODetail
                        if (result[0].HSN_SAC !== null && result[0].GST !== null && result[0].Is_SameState !== null) {
                            $("#Hsn_Sac").val(result[0].HSN_SAC);
                            $("#GST").val(result[0].GST);
                            $("#hdnIs_SameState").val(result[0].Is_SameState);
                            $("#hdnItemType").val(result[0].ItemNature);
                            $("#hdnLastRate").val(result[0].LastPurchaseRate);
                            $("#AvailableQuantity").val(result[0].Available_Qty);
                            $("#Unit_Id").val(result[0].UnitId);
                            $("#GST").change();                           
                            ResetStockQty(false);
                            $.each(result, function (data, value) {
                                if (value.Office_ID > 0) {
                                    $("#ddlAvailableQty").append($("<option></option>").val(value.Office_ID).html(value.OfficeName));
                                }
                            })
                            ResetSaleOrder(true);
                            if (POresult.length > 0) {
                                $.each(POresult, function (data, value) {
                                    $("#ddlAvailableSaleOrder").append($("<option></option>").val(value.POID).html(value.PONumber));
                                })
                            }
                            try {
                                $("#ddlAvailableQty").val(result[0].Office_ID);
                            }
                            catch (ex) {
                                // $("#ddlAvailableQty").val(result[0].Office_ID);
                            }
                        } else {
                            $("#Hsn_Sac").val("");
                            $("#GST").val("0");
                            $("#hdnIs_SameState").val("0");
                            $("#GST").change();
                        }
                    });
                }
            },
            minLength: 1,
            delay: 1000,
        });

        function ResetFromOfficeChange() {
            $("#SearchOrderNo").val("");
            $("#StateId").empty();
            $("#StateId").append($("<option></option>").val("0").html("--Select--"));
            $("#tbodyid").empty();
            $("#lblTotalAmount").text("Total : 0.00");
            $("#IsUpdateMaterialSales").val(0);
            $("#SaleAmount").val("0");
            ResetStockQty(false);
            ResetSaleOrder(false);
        }

        ResetFromOfficeChange();

        //$("[id^=txtOrder_Qty_],[id^=txtRate_],[id^=txtRate_],[id^=txtRate_]").change(function () {
        //    Calculate(this);
        //});

    };
    return scope;
})(IMSMaterialSales || {});
function ResetStockQty(Setval) {
    $('#ddlAvailableQty').empty();
    if (Setval) {
        $("#ddlAvailableQty").append($("<option></option>").val(0).html("--Select--"));
    }

}
function ResetSaleOrder(Setval) {
    $('#ddlAvailableSaleOrder').empty();
    if (Setval) {
        $("#ddlAvailableSaleOrder").append($("<option></option>").val(0).html("--Select--"));
    }

}

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
        discount_2_Amt = (amount - discount_1_Amt) * (discount_2) / 100;
        disAmu = discount_1_Amt + discount_2_Amt;

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
            discount_2_amt = (amount - discount_1_amt) * (discount_2) / 100;
            disAmu = discount_1_amt + discount_2_amt;

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
    // pass SaleId and ItemID
    let SaleId = parseInt($("#SaleId").val());
    let itemId = parseInt($(button).attr('data-item-index'));
    let apptoken = $("#hdnApptoken").val();
    $("#IsUpdateMaterialSales").val(0);
    var row = $(button).closest("TR");
    var name = $("TD", row).eq(0).text();
    var index = row[0].rowIndex;
    if (confirm("Do you want to remove this Item.", "Remove")) {
        //Get the reference of the Table.
        var table = $("#tblMaterialSales")[0];
        //Delete the Table row using it's Index.
        table.deleteRow(index);
        if (SaleId > 0) {
            IMSC.ajaxCall("GET", "/Material/DeleteMatrialSalesLine?SaleId=" + SaleId + "&ItemID=" + itemId + "&AppToken=" + apptoken, {}, "text", function (d) {
                var result = JSON.parse(d);
                if (result.IsSucceed) {
                    alert(result.ActionMsg);

                }

            });
        }
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
    debugger;
    let totalAmount = 0;
    $.each(result, function (index, value) {
        let quantity = parseFloat(value.Order_Qty);
        let rate = parseFloat(value.Order_Rate);
        //let Order_Disc1 = parseFloat(value.Order_Disc1);
        //let Order_Disc2 = parseFloat(value.Order_Disc2);
        let gst = parseFloat(value.GST !== "" ? value.GST : "0");
        let amount = 0;
        let tamount = 0;
        let gstAmount = 0;
        let cgst = 0;
        let sgst = 0;
        let igst = 0;
        let Texable_Amount = 0;
        let discount_1 = parseFloat(value.Order_Disc1);
        let discount_2 = parseFloat(value.Order_Disc2);
        let discount_1_Amt = 0;
        let discount_2_Amt = 0;

        //if (amount > 0) {
        if (sourceid === 1) {

            if (quantity > 0 && rate > 0) {
                amount = quantity * rate;

            }
            if (amount > 0) {
                let is_SameState = value.Is_SameState.toString() === "1" ? true : false;
                Texable_Amount = amount;
                if (discount_1 > 0) {
                    Texable_Amount = (amount - (amount * discount_1 / 100)).toFixed(2);
                    discount_1_Amt = parseFloat((amount * discount_1 / 100)).toFixed(2);
                }
                if (discount_2 > 0) {
                    Texable_Amount = (Texable_Amount - (Texable_Amount * discount_2 / 100)).toFixed(2);
                    discount_2_Amt = parseFloat((amount - discount_1_Amt) * discount_2 / 100).toFixed(2);
                }
                gstAmount = (Texable_Amount * gst / 100).toFixed(2);
                if (is_SameState) {
                    cgst = parseFloat(gstAmount / 2).toFixed(2)
                    sgst = parseFloat(gstAmount / 2).toFixed(2)
                    igst = parseFloat(0).toFixed(2);
                } else {
                    cgst = parseFloat(0).toFixed(2)
                    sgst = parseFloat(0).toFixed(2)
                    igst = parseFloat(gstAmount).toFixed(2);
                }

                tamount = (parseFloat(Texable_Amount) + parseFloat(gstAmount)).toFixed(2);;
                value.Discount_1_Amount = discount_1_Amt;
                value.Discount_2_Amount = discount_2_Amt;
            }
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


        //}



        //Get the reference of the Table's TBODY element.
        var tBody = $("#tblMaterialSales > TBODY")[0];
        //Add Row.
        var row = tBody.insertRow(-1);
        $(row).attr('id', 'trRow_' + value.Line_Id);
        //Add Button cell.
        //let htmlEditBtn = `<input type="button" onclick="Remove(this);" value="Remove"/>`;
        let htmlEditBtn = `<a class="btn btn-danger" style="padding: 2px 5px 2px 5px; margin-bottom:0px" data-item-index="${value.Item_Id}" onclick="Remove(this);" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
        cell = $(row.insertCell(-1));
        cell.append(htmlEditBtn);
        //Add Item cell.

        let lblItem = `<label id="lblItem_${value.GR_Id}">${value.Gr_no}</label>
                            <input type="hidden" id="hdnItemId_${value.GR_Id}" name="hdnItemId_${value.GR_Id}" value="${value.Item_Id}" />
                            <input type="hidden" id="hdnLineId_${value.GR_Id}" name="hdnLineId_${value.GR_Id}" value="${value.Line_Id === undefined ? "0" : value.Line_Id}" />
                            <input type="hidden" id="hdnPOLineId_${value.GR_Id}" name="hdnPOLineId_${value.GR_Id}" value="${value.POLine_Id}" />
                            <input type="hidden" id="hdnPOId_${value.GR_Id}" name="hdnPOId_${value.GR_Id}" value="${value.PO_Id}" />
                            <input type="hidden" id="hdnItemTypeGrid_${value.GR_Id}" name="hdnItemTypeGrid_${value.GR_Id}" value="${value.ItemNature}" />
                            <input type="hidden" id="hdnIs_SameStateGrid_${value.GR_Id}" name="hdnIs_SameStateGrid_${value.GR_Id}" value="${value.Is_SameState === undefined ? "0" : value.Is_SameState}" />
                            <input type="hidden" id="hdnDiscount_1_Amt_${value.GR_Id}" name="hdnDiscount_1_Amt_${value.GR_Id}" value="${value.Discount_1_Amount === undefined ? "0" : value.Discount_1_Amount}" />
                            <input type="hidden" id="hdnDiscount_2_Amt_${value.GR_Id}" name="hdnDiscount_2_Amt_${value.GR_Id}" value="${value.Discount_2_Amount === undefined ? "0" : value.Discount_1_Amount}" />
                            <input type="hidden" id="hdnItemRequestQty_${value.GR_Id}" name="hdnItemRequestQty_${value.GR_Id}" value="${quantity}" />
                          `;
        cell = $(row.insertCell(-1));
        cell.append(lblItem);
        //Add Order Date.
        let lblOrderDate = `<label id="lblOrderDate_${value.GR_Id}">${value.GR_Date !== undefined ? value.GR_Date : "N/A"}</label>`;
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
        let lblAvailable_Qty = `<label id="lblAvailable_Qty_${value.GR_Id}" style="background-color: lightgreen;">${value.vehicle_No === null ? "0" : value.vehicle_No}</label>`;
        cell = $(row.insertCell(-1));
        cell.append(lblAvailable_Qty);
        //Add Unit cell.
        let lblUnit = `<label id="lblUnit_${value.GR_Id}" data-Unit_Id="${value.GR_Id}" data-index="${value.GR_Id}" >${value.consignee_id}</label>`;
        cell = $(row.insertCell(-1));
        cell.append(lblUnit);
        //Add Last Rate cell.
        let htmlLastRate = `<label id="lblLastRate_${value.GR_Id}" data-index="${value.GR_Id}" >${value.origin_id === undefined ? "0" : value.origin_id}</label>`;
        cell = $(row.insertCell(-1));
        cell.append(htmlLastRate);
        //Add Rate cell.
        let htmlRate = `<label id="lblLastRate_${value.GR_Id}" data-index="${value.GR_Id}" >${value.origin_id === undefined ? "0" : value.Total_Freight}</label>`;
        cell = $(row.insertCell(-1));
        cell.append(htmlRate);
        //Add Amount cell.
        let lblAmount = `<label id="lblAmount_${value.GR_Id}">${value.other_charge}</label>`;
        cell = $(row.insertCell(-1));
        cell.append(lblAmount);
        //Add Discount_1 cell.
        //let htmlDiscount1 = `<input type="text" id="txtDiscount1_${value.Item_Id}" ${isqtydisabled == 1 ? "disabled" : "enabled"} data-index="${value.Item_Id}" name="txtDiscount1_${value.Item_Id}" onchange="Calculate(this);" value="${sourceid === 1 ? 0 : discount_1}"  tp-type="numeric" style="width:30px;"/>`;
        
        //Add GST cell.
        //let lblGST = `<label id="lblGST_${value.Item_Id}">${gst}</label>`;
        //cell = $(row.insertCell(-1));
        //cell.append(lblGST);
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
      
    });
    $("[id^='lblTotal_Amount_Row_']").each(function () {
        totalAmount += parseFloat($("#" + this.id).text());
    });
    $("#SaleAmount").val(totalAmount.toFixed(2));
    $("#lblTotalAmount").text("Total : " + totalAmount.toFixed(2));
    $("#divDeleteMatrial").css('display', 'block');
};
function Calculate(value) {
    $("#IsUpdateMaterialSales").val(1);
    let index = parseInt($('#' + value.id).attr('data-index'));
    let bIsDiscount = false;
    let POQty = parseFloat($("#hdnItemRequestQty_" + index).val() !== "" ? $("#hdnItemRequestQty_" + index).val() : 0);
    let quantity = parseFloat($("#txtOrder_Qty_" + index).val() !== "" ? $("#txtOrder_Qty_" + index).val() : 0);
    let aQuantity = parseFloat(parseInt($("#lblAvailable_Qty_" + index).text()));
    let poLineId = parseInt($("#hdnPOLineId_" + index).val());
    if (poLineId === 0) {
        setCaluValuew(index, bIsDiscount, quantity, poLineId);
    } else {
        if ((quantity <= aQuantity) && (quantity <= POQty)) {
            setCaluValuew(index, bIsDiscount, quantity, poLineId);
        }
        else if (quantity > aQuantity) {
            $('#alertModal').modal('show');
            $('#msg').html("Please check available quantity!!!");
        }
        else if (quantity > POQty) {
            $('#alertModal').modal('show');
            $('#msg').html("quantity must be less or equal to order quantity!!!");
        }
    }
}


//$("#Client_Id").change(function () {
//    //var id = $('#Client_Id').val();
//    alert(id);
//    alert("hello");

//});