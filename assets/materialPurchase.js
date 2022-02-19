var IMSMaterialPurchase = (function (scope) {
    scope.AppToken = '';
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
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            } else if ($("#Quantity").val() === "") {
                msg = "Please enter quantity.";
                bAdded = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            } else if (parseInt($("#Unit_Id").val()) === 0) {
                msg = "Please select unit.";
                bAdded = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            } else if ($("#Rate").val() === "") {
                msg = "Please Please enter rate.";
                bAdded = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
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
                cell.html($("#ItemSearch").val());
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
            if (parseInt($("#OfficeId").val()) === 0) {
                msg = "Please select office.";
                isValid = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
            if ($("#InvoiceNo").val() === "") {
                msg = "Please fill invoice no.";
                isValid = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
            if (parseInt($("#PartyId").val()) === 0) {
                msg = "Please fill party.";
                isValid = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
            if ($("#Purchase_Ref").val() === "") {
                msg = "Please fill purchase ref.";
                isValid = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
            if ($("#TransactionDate").val() === "") {
                msg = "Please select transaction date.";
                isValid = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
            if (parseInt($("#StateId").val()) === 0) {
                msg = "Please select State.";
                isValid = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
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
    };
    return scope;
})(IMSMaterialPurchase || {});
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