﻿var IMSConsignment = (function (scope) {
    scope.AppToken = '';
    scope.Consignment = function () {
        $("#Oem_Id").select2();
        //$("#Client_Id").select2();
        //$("#GR_OfficeId").select2();
        //$("#Consignee_Id").select2();
        //$("#Billing_OfficeId").select2();
        //$("#Consigner_Id").select2();
        //$("#Consigner_Id").select2();
        //$("#Origin_Id").select2();
        //$("#GR_No").select2();
        //$("#Destination_Id").select2();
        //$("#Vehicle_Id").select2();
        //$("#Contract_Id").select2();
        //$("#TransactionDate").val("");

        //$("#GR_Date").attr('placeholder', 'dd/mm/yyyy');
        //$("#Advance_Date").attr('placeholder', 'dd/mm/yyyy');

        //$("#ItemSearch").autocomplete({
        //    source: function (request, response) {
        //        IMSC.ajaxCall("GET", "/Material/SearchItem?Item=" + request.term + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
        //            var result = JSON.parse(d);
        //            if (result.length === 0) {
        //                $("#empty-message").text("No results found");
        //                return response({ label: "No results found", value: 0 });
        //            } else {
        //                response($.map(result, function (item) {
        //                    return { label: item.Title, value: item.Item_Id };
        //                    $("#Item_Id").val(Item_Id);
        //                }))
        //            }
        //        });
        //    },
        //});

        //$("#btnAdd").click(function (e) {
        //    e.preventDefault();
        //    let bAdded = true;
        //    let msg = "";
        //    let amount = 0.0;

        //    //if ($("#Item_Id").val() === "0" || $("#Item_Id").val() === "" || $("#ItemSearch").val() === "") {
        //    //    msg = "Please select Material.";
        //    //    bAdded = false;
        //    //    $('#alertModal').modal('show');
        //    //    $('#msg').html(msg);
        //    //    return false;
        //    //} else if ($("#Actual_Weight").val() === "") {
        //    //    msg = "Please enter Actual Weight.";
        //    //    bAdded = false;
        //    //    $('#alertModal').modal('show');
        //    //    $('#msg').html(msg);
        //    //    return false;
        //    //} else if (parseInt($("#Unit").val()) === 0) {
        //    //    msg = "Please select Unit.";
        //    //    bAdded = false;
        //    //    $('#alertModal').modal('show');
        //    //    $('#msg').html(msg);
        //    //    return false;
        //    //} else if ($("#Charge_Weight").val() === "") {
        //    //    msg = "Please Please enter Charge Weight.";
        //    //    bAdded = false;
        //    //    $('#alertModal').modal('show');
        //    //    $('#msg').html(msg);
        //    //    return false;
        //    //} else if ($("#Pcs").val() === "") {
        //    //    msg = "Please Please enter Pcs.";
        //    //    bAdded = false;
        //    //    $('#alertModal').modal('show');
        //    //    $('#msg').html(msg);
        //    //    return false;
        //    //} else if ($("#Rate_Type").val() === "") {
        //    //    msg = "Please Please enter Rate Type.";
        //    //    bAdded = false;
        //    //    $('#alertModal').modal('show');
        //    //    $('#msg').html(msg);
        //    //    return false;
        //    //} else if ($("#Rate").val() === "") {
        //    //    msg = "Please Please enter Rate.";
        //    //    bAdded = false;
        //    //    $('#alertModal').modal('show');
        //    //    $('#msg').html(msg);
        //    //    return false;
        //    //} else if ($("#Basic_Freight").val() === "") {
        //    //    msg = "Please Please enter Basic Freight.";
        //    //    bAdded = false;
        //    //    $('#alertModal').modal('show');
        //    //    $('#msg').html(msg);
        //    //    return false;
        //    //} 

        //    //Reference the Party and Location ddl.
        //    if (bAdded) {
        //        if ($("#tblConsignment TBODY TR").length > 0) {
        //            $("#tblConsignment TBODY TR").each(function () {
        //                var row = $(this);
        //                let td = row.find("TD");
        //                let itemId = parseInt(td.eq(1).attr('data-item-id'));
        //                if (parseInt($("#Item_Id").val()) === itemId) {
        //                    msg = "Material is already mapped!!!";
        //                    bAdded = false;
        //                }
        //            });
        //        }
        //    }

        //    if (bAdded) {
        //        var tBody = $("#tblConsignment > TBODY")[0];
        //        //Add Row.
        //        var row = tBody.insertRow(-1);
        //        //Add Button cell.
        //        cell = $(row.insertCell(-1));
        //        let btnRemove = `<a class="btn btn-danger" onclick="Remove(this);" href="javascript:void(0)"><i class="fa fa-trash"></i></a>`;
        //        cell.append(btnRemove);

        //        //Add Item cell.
        //        var cell = $(row.insertCell(-1));
        //        cell.html($("#ItemSearch").val());
        //        cell.attr('data-item-id', $("#Item_Id").val());
        //        cell.attr('data-line-Id', 0);

        //        //Add Actual_Weight cell.
        //        cell = $(row.insertCell(-1));
        //        cell.html($("#Actual_Weight").val());
        //        //Add Charge_Weight cell.
        //        cell = $(row.insertCell(-1));
        //        cell.html($("#Charge_Weight").val());
        //        //Add Pcs cell.
        //        cell = $(row.insertCell(-1));
        //        cell.html($("#Pcs").val());
        //        //Add Unit cell.
        //        cell = $(row.insertCell(-1));
        //        cell.html($("#Unit :selected").text());
        //        cell.attr('data-Unit', $("#Unit").val());
        //        //Add Rate_Type cell.
        //        cell = $(row.insertCell(-1));
        //        cell.html($("#Rate_Type").val());
        //        //Add Rate cell.
        //        cell = $(row.insertCell(-1));
        //        cell.html($("#Rate").val());
        //        //Add Basic_Freight cell.
        //        cell = $(row.insertCell(-1));
        //        cell.html($("#Basic_Freight").val());
                
        //        cell = $(row.insertCell(-1));
        //        cell.html($("#hdnTotalAmount").val());
        //        //}

        //        //Set Default
        //        $("#Item_Id").val("");
        //        $('#ItemSearch').val("");
        //        $("#Actual_Weight").val("");
        //        $("#Charge_Weight").val("");
        //        $("#Pcs").val("");
        //        $("#Unit").val($("#Unit option:first").val());
        //        $("#Rate_Type").val("");
        //        $("#Rate").val("0");
        //        $("#Basic_Freight").val("0");
        //        $("#hdnTotalAmount").val("");
        //        $("#hdnRowId").val("");

        //        $("#tblConsignment TBODY TR").each(function () {
        //            var row = $(this);
        //            let td = row.find("TD");
        //            amount += parseFloat(td[14].innerText);
        //        });
        //        $("td").each(function () {
        //            $(this).addClass("tbl-css");
        //        });
        //        $("#PurchaseAmount").val(amount.toFixed(2));
        //        $("#lblTotalAmount").text("Total : " + amount.toFixed(2));
        //    }

        //    if (bAdded) {
        //        return true;
        //    } else {
        //        $('#alertModal').modal('show');
        //        $('#msg').html(msg);
        //        return false;
        //    }
        //});

        //$("body").on("click", "#btnSubmit", function () {
        //    //Loop through the Table rows and build a JSON array.
        //    let OrderLineValues = [];
        //    let isValid = true;
        //    totalAmount = 0.0;
        //    let msg = "";
        //    if ($("#PartyId").val() === "0" || $("#PartyId").val() === "") {
        //        msg = "Please fill party.";
        //        isValid = false;
        //        $('#alertModal').modal('show');
        //        $('#msg').html(msg);
        //        return false;
        //    }
        //    if ($("#tblConsignment TBODY TR").length > 0) {
        //        $("#tblConsignment TBODY TR").each(function () {
        //            let row = $(this).find('td');
        //            let oMapping = {};

        //            oMapping.Line_Id = row.eq(1).attr('data-line-Id');
        //            oMapping.Item_Id = row.eq(1).attr('data-item-id');
                    
        //            oMapping.Item_Id = "1";
        //            oMapping.GR_Id = "1";
        //            oMapping.Actual_Weights = row.eq(2).attr('data-unit-id');
        //            oMapping.Actual_Weight = row[2].innerText;
        //            oMapping.Charge_Weight = row[3].innerText;
        //            oMapping.Stock_Office_Id = row.eq(3).attr('data-StockOffice-id');

        //            oMapping.Quantity = row[4].innerText;
        //           // oMapping.Unit_Id = row[5].innerText;

        //            oMapping.Unit_Id = "2";

        //            oMapping.Rate_TypeId = row[6].innerText;
        //            oMapping.Rate = row[7].innerText;
        //           // oMapping. = row[8].innerText;
        //            oMapping.Basic_Freight = row[8].innerText;

        //            //oMapping.Amount = Calculation(parseFloat(row[6].innerText) * parseFloat(row[7].innerText), parseFloat(row[8].innerText), parseFloat(row[9].innerText));
        //            //totalAmount += parseFloat(oMapping.Amount);
        //          //  oMapping.Remarks = row[11].innerText
        //            oMapping.IsUpdate = $("#IsUpdate").val();
        //            OrderLineValues.push(oMapping);
        //        });
        //        $("#TotalAmount").val(totalAmount.toFixed(2));
        //        $("#ConsignmentLines").val(JSON.stringify(OrderLineValues));
        //    } else {
        //        msg = "Please add at least 1 matrial order.";
        //        isValid = false;
        //        $('#alertModal').modal('show');
        //        $('#msg').html(msg);
        //        return false;
        //    }

        //    if (isValid) {
        //        return true;
        //    } else {
        //        $('#alertModal').modal('show');
        //        $('#msg').html(msg);
        //        return false;
        //    }
        //});

    };
    return scope;
})(IMSConsignment || {});