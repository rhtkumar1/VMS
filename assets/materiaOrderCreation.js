var IMSMaterialOrderCreation = (function (scope) {
    scope.AppToken = '';
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
            $("#lblPendingQty").text("");
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
                    $('#alertModal').modal('show');
                    $('#msg').html(msg);
                    return false;
                }
            }
            //if (bAdded) {
            //    if (parseInt($("#txtOrderQty").val()) <= parseInt($("#lblAvailableQty").text())) {
            //        bAdded = true;
            //    } else {
            //        msg = "Order qty grater then available qty!!!";
            //        bAdded = false;
            //        $('#alertModal').modal('show');
            //        $('#msg').html(msg);
            //        return false;
            //    }
            //}
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
                //if ($("#hdnRowId").val() !== "" && parseInt($("#hdnRowId").val()) > 0) {
                //    let trId = "#trRow_" + parseInt($("#hdnRowId").val());
                //    var row = $(trId).closest("TR").find('td');
                //    row[1].textContent = $("#ItemSearch").text();
                //    row[1].setAttribute('data-item-id', $("#Item_Id").val());
                //    row[1].setAttribute('data-line-Id', $("#hdnLineId").val());
                //    row[1].setAttribute('data-index', $("#Item_Id").val());
                //    row[2].textContent = $("#Unit_Id").val();
                //    row[3].textContent = $("#lblAvailableQty").text();
                //    row[4].textContent = $("#lblLastRate").text();
                //    row[5].textContent = $("#lblLastDist1").text();
                //    row[6].textContent = $("#lblLastDist2").text();
                //    row[7].textContent = $("#lblListPrice").text();
                //    row[8].textContent = $("#txtOrderQty").val();
                //    row[9].textContent = $("#txtOrderRate").val();
                //    row[10].textContent = parseFloat(parseFloat($("#txtOrderQty").val()) * parseFloat($("#txtOrderRate").val())).toFixed(2);
                //    row[11].textContent = $("#txtRemark").val();
                //    $("#hdnRowId").val("");
                //    $("#hdnLineId").val("");
                //    $("#btnAdd").val("Add");
                //} else {
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
                cell.html($("#ItemSearch").val());
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
                //Add LastDist1 cell.
                cell = $(row.insertCell(-1));
                cell.html($("#txtDisc1").val());
                cell.attr('data-index', $("#Item_Id").val());
                //Add LastDist2 cell.
                cell = $(row.insertCell(-1));
                cell.html($("#txtDisc2").val());
                cell.attr('data-index', $("#Item_Id").val());
                //Add amount cell.
                cell = $(row.insertCell(-1));
                cell.html(Calculation(parseFloat(parseFloat($("#txtOrderQty").val()) * parseFloat($("#txtOrderRate").val())), parseFloat($("#txtDisc1").val()), parseFloat($("#txtDisc2").val())));
                cell.attr('data-index', $("#Item_Id").val());
                //Add txtRemark cell.
                cell = $(row.insertCell(-1));
                cell.html($("#txtRemark").val());
                cell.attr('data-index', $("#Item_Id").val());
                //}

                //Set Default
                $("#Item_Id").val("");
                $('#ItemSearch').val("");
                $("#Unit_Id").val($("#Unit_Id option:first").val());
                $('#Unit_Id').select2().trigger('change');
                $("#lblAvailableQty").text("");
                $("#lblLastRate").text("");
                $("#txtDisc1").val("");
                $("#txtDisc2").val("");
                $("#lblListPrice").text("");
                $("#lblPendingQty").text("");
                $("#lblLastDist1").text("");
                $("#lblLastDist2").text("");
                $("#txtOrderQty").val("");
                $("#txtOrderRate").val("");
                $("#txtRemark").val("");
                $("#hdnRowId").val("");
                $("#tblOrderCreation TBODY TR").each(function () {
                    totalAmount += parseFloat($(this).find('td')[10].innerText);
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
            if ($("#PartyId").val() === "0" || $("#PartyId").val() === "") {
                msg = "Please fill party.";
                isValid = false;
                $('#alertModal').modal('show');
                $('#msg').html(msg);
                return false;
            }
            if ($("#tblOrderCreation TBODY TR").length > 0) {
                $("#tblOrderCreation TBODY TR").each(function () {
                    let row = $(this).find('td');
                    let oMapping = {};
                    
                    oMapping.Line_Id = row.eq(1).attr('data-line-Id');
                    oMapping.Item_Id = row.eq(1).attr('data-item-id');
                    oMapping.UnitId = row.eq(2).attr('data-unit-id');
                    oMapping.Available_Qty = row[3].innerText;
                    oMapping.Last_Rate = row[4].innerText;
                    oMapping.Last_Price = row[5].innerText;
                    oMapping.Order_Qty = row[6].innerText;
                    oMapping.Order_Rate = row[7].innerText;
                    oMapping.Last_Discount_1 = row[8].innerText;
                    oMapping.Last_Discount_2 = row[9].innerText;
                    oMapping.Amount = Calculation(parseFloat(row[6].innerText) * parseFloat(row[7].innerText), parseFloat(row[8].innerText), parseFloat(row[9].innerText));
                    totalAmount += parseFloat(oMapping.Amount);
                    oMapping.Remarks = row[11].innerText
                    oMapping.IsUpdate = $("#IsUpdate").val();
                    OrderLineValues.push(oMapping);
                });
                $("#TotalAmount").val(totalAmount.toFixed(2));
                $("#MaterialLine").val(JSON.stringify(OrderLineValues));
            } else {
                msg = "Please add at least 1 matrial order.";
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

        function Calculation(Amount, Disc1, Disc2) {
            var FinalAmount = Amount;
            if (!isNaN(Disc1)) {
                FinalAmount = (Amount - (Amount * Disc1 / 100)).toFixed(2);
            }
            if (!isNaN(Disc2)) {
                FinalAmount = (FinalAmount - (FinalAmount * Disc2 / 100)).toFixed(2);
            }
            return FinalAmount;
        }

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
                
                if (parseInt($("#PartyId").val()) === 0) {
                    msg = "Please select party.";
                    isValid = false;
                    $("#ItemSearch").val("");
                    $('#alertModal').modal('show');
                    $('#msg').html(msg);
                    return false;
                }

                $("#ItemSearch").val(i.item.label);
                let Item_Id = parseInt(i.item.value);
                let PartyId = parseInt($("#PartyId").val());
                $("#Item_Id").val(Item_Id);
                if (Item_Id > 0) {
                    IMSC.ajaxCall("GET", "/Material/GetItemDetail?Item_Id=" + Item_Id + "&Party_Id=" + PartyId+ "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                        var result = JSON.parse(d);
                        if (result != null && result.length > 0) {
                            $("#lblAvailableQty").text(result[0].Available_Qty);
                            $("#lblLastRate").text(result[0].Last_Rate);
                            $("#lblLastDist1").text(result[0].Last_Disc_1);
                            $("#lblLastDist2").text(result[0].Last_Disc_2);
                            $("#lblListPrice").text(result[0].List_Price);
                            $("#lblPendingQty").text(result[0].Pending_Qty);
                            $("#Unit_Id").val(result[0].UnitId);
                            $('#Unit_Id').select2().trigger('change');
                            $("#hdnRowId").val("");
                            
                        }
                    });
                }
            },
            minLength: 1,
            delay: 1000,
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
                $("#PartyId").val(PartyId);
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
            minLength: 1,
            delay: 1000,
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
                            //Add ListPrice cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Last_Price !== undefined ? value.Last_Price : 0);
                            cell.attr('data-index', value.Item_Id);
                            //Add txtOrderQty cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Order_Qty);
                            cell.attr('data-index', value.Item_Id);
                            //Add txtOrderRate cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Order_Rate);
                            cell.attr('data-index', value.Item_Id);
                            //Add LastDist1 cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Last_Discount_1);
                            cell.attr('data-index', value.Item_Id);
                            //Add LastDist2 cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.Last_Discount_2);
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
            $("#lblPendingQty").text("");
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
    };
    return scope;
})(IMSMaterialOrderCreation || {});

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