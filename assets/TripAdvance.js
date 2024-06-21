var IMSMaterialPurchase = (function (scope) {
    scope.AppToken = '';
    scope.materialPurchase = function () {
        //$("#drpParty").select2();
        //$("#drpInvoice").select2();
       // $("#FromDate").val("");
        //$("#FromDate").attr('placeholder', 'dd/mm/yyyy');

        let VehicleNo = "";
        let DriverName = "";
        let AdvanceNo = "";
        let FromDate = "";
        let ToDate = "";
     

      
        $("#btnSearch").click(function (e) {
            debugger;
            e.preventDefault();
            //let Purchase_Id = parseInt($("#PurchaseId").val());
            // let vehicleNo = $("#VehicleNOSearch").val();
            let vehicleNo = VehicleNo;

            IMSC.ajaxCall("GET", "/Material/GetAdvanceTrip?vehicleNo=" + vehicleNo + "&DriverName=" + DriverName+ "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                var res = JSON.parse(d);
                //debugger;
                    if (res !== null) {
                        var result = res.Table;
                        //$("#StateId").empty();
                        //$("#StateId").append($("<option></option>").val(0).html("Select State"));
                        //$.each(res.Table1, function (index, value) {
                        //    $("#StateId").append($("<option></option>").val(value.State_Id).html(value.StateName));
                        //});
                        //$("#PartyId").val(result[0].Party_Id);
                        //$("#StateId").val(result[0].SupplyState_Id);
                        //$("#IsUpdateMaterialPurchase").val(1);
                        //$("#OfficeId").val(result[0].Office_Id);
                        //$("#InvoiceNo").val(result[0].Invoice_No);
                        //$("#PartyId").val(result[0].Party_Id);
                        //$("#SearchParty").val(result[0].PartyName);
                        //$("#Purchase_Ref").val(result[0].Purchase_Ref);
                        //$("#TransactionDate").val(result[0].Transaction_Date);
                        //$("#SupplyStateId").val(result[0].SupplyState_Id);
                        //$("#Remarks").val(result[0].Remarks);
                        //$("#PurchaseId").val(result[0].Purchase_Id)
                        //$("#AgentId").val(result[0].AgentId);
                        //$("#PurchaseAmount").val(result[0].Purchase_Amount);
                        $("#tblTripAdvance > TBODY").empty('');
                        let amount = 0;
                        $.each(res, function (index, value) {
                        //    debugger;
                          //  amount += parseFloat(value.Total_Amount);
                            //Get the reference of the Table's TBODY element.
                            var tBody = $("#tblTripAdvance > TBODY")[0];
                            //Add Row.
                            var row = tBody.insertRow(-1);
                            $(row).attr('id', 'trRow_' + value.TripAdvanceId);
                            $(row).attr('class', 'trRow_' + value.TripAdvanceId);
                            //Add Button cell.
                            cell = $(row.insertCell(-1));
                            let btnRemove = '<a class="btn btn-success" onclick="Edit(' + value.TripAdvanceId +');" href="javascript:void(0)"><i class="fa fa-pencil"></i></a><a class="btn btn-danger" onclick="Remove('+value.TripAdvanceId+');" href="javascript:void(0)"><i class="fa fa-trash"></i></a>';
                            cell.append(btnRemove);
                            //Add Item cell.
                            //var cell = $(row.insertCell(-1));
                            //cell.html(value.ItemTitle);
                            //cell.attr('data-item-id', value.Item_Id);
                            //cell.attr('data-line-Id', value.Line_Id);
                            //Add Hsn_Sac cell.
                            cell = $(row.insertCell(-1));
                            cell.html(value.OfficeId);
                            cell = $(row.insertCell(-1));
                            cell.html(value.AdvanceDate);
                            cell = $(row.insertCell(-1));
                            cell.html(value.VehicleNo);

                            cell = $(row.insertCell(-1));
                            cell.html(value.Driver);
                            cell = $(row.insertCell(-1));
                            cell.html(value.TripNo);
                            cell = $(row.insertCell(-1));
                            cell.html(value.TypeId);
                            cell = $(row.insertCell(-1));
                            cell.html(value.CrAccount);
                            cell = $(row.insertCell(-1));
                            cell.html(value.DrAccount);
                           




                            cell = $(row.insertCell(-1));
                            cell.html(value.Quantity);
                            cell = $(row.insertCell(-1));
                            cell.html(value.Rate);
                            cell = $(row.insertCell(-1));
                            cell.html(value.Amount);

                            cell = $(row.insertCell(-1));
                            cell.html(value.Remarks);
                            cell = $(row.insertCell(-1));
                            //cell.html(value.Image);
                            var ImageFull = "../Images/TripAdvance/" + value.Image;
                            var img1 = '<a href="' + ImageFull + '" download><img src="' + ImageFull + '" width="50" height="60" /></a>';
                        
                            //let btnImage = '<a href="javascript:void(0)"><img src="' + img1 + '"  width="50" height="60"/></a>';
                            cell.append(img1);
                            //Add Unit cell.
                           
                            //cell.attr('data-Unit_Id', value.Unit_Id);
                            ////Add Rate cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.Rate);
                            ////Add Amount cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.Amount);
                            //Add Discount_1 cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.Discount_1);
                            ////Add Discount_2 cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.Discount_2);
                            ////Add Taxable_Amount cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.Taxable_Amount);
                            ////Add GST cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.GST);
                            ////Add CGST cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.CGST);
                            ////Add SGST cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.SGST);
                            ////Add IGST cell.
                            //cell = $(row.insertCell(-1));
                            //cell.html(value.IGST);
                            ////Add Total_Amount cell.
                            //cell = $(row.insertCell(-1));
                            //cell.attr('data-row-Id', value.Line_Id);
                            //cell.html(value.Total_Amount);
                        });
                        $("#lblTotalAmount").text("Total : " + amount.toFixed(2));
                        $("#divDeleteMatrial").css('display', 'block');
                        $("td").each(function () {
                            $(this).addClass("tbl-css");
                        });
                    }
                });
            
        });

        $("#btnResetForm").click(function (e) {
            e.preventDefault();
            $("#Office_Id").val($("#Office_Id option:first").val());
            $("#Id").val(0);
            $("#ADVANCEDATE").val("");
            $("#ADVANCENO").val($("#ADVANCENO option:first").val());
            $("#VEHICLENO").val($("#VEHICLENO option:first").val());
            $("#DRIVERNAME").val($("#DRIVERNAME option:first").val());
            $("#TRIPNO").val($("#TRIPNO option:first").val());
            $("#ADVANCETYPE").val($("#ADVANCETYPE option:first").val());
            $("#DRACCOUNT").val($("#DRACCOUNT option:first").val());
            $("#CRACCOUNT").val($("#CRACCOUNT option:first").val());

            $("#QTY").val("");
            $("#RATE").val("");
            $("#AMOUNT").val("");
            $("#IMAGEPath").val("");
            $("#tblTripAdvance TBODY").empty();
            $("#REMARK").val("");


            $("#VehicleNOSearch").val("");
            $("#FromDate").val("");
            $('#ToDate').val("");
            $("#DriverNOSearch").val("");
            $('#AdvanceNOSearch').val("");
            vehicleNo = "";
            DriverName = "";



        });
        //$("#Quantity,#Rate,#Discount_1,#Discount_2,#GST").change(function () {
        //    Calculate();
        //});
        $("#QTY,#RATE").change(function (e) {
            let quantity = parseFloat($("#QTY").val() !== "" ? $("#QTY").val() : 0);
            let rate = parseFloat($("#RATE").val() !== "" ? $("#RATE").val() : 0);
            if (quantity > 0 && rate > 0) {
              let  amount = quantity * rate;
                $("#AMOUNT").val(amount);
            }
        });

        $("#VehicleNOSearch").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchVehicleNo?VehicleNo=" + request.term + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.VehicleNo, value: item.Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
               // $("#VehicleNOSearch").val(i.item.label);
                 //VehicleNo = parseInt(i.item.value);
                VehicleNo = (i.item.label);
               // alert(VehicleNo)
               // $("#SearchOrderNo").removeAttr('disabled');
               // let OfficeId = parseInt($('#OfficeId').val());

                //$("#PartyId").val(PartyId);
                //if (PartyId > 0) {
                //    $("#tbodyid").empty();
                //    //IMSC.ajaxCall("GET", "/Material/GetStateSales11?PartyId=" + PartyId + "&OfficeId=" + OfficeId + "&AppToken=" + scope.AppToken, {}, "text", function (d) {

                //    //    var result = JSON.parse(d);
                //    //    if (result !== null)
                //    //    {
                //    //        $("#StateId").empty();
                //    //        if (result.Table.length > 0) {
                //    //            $("#StateId").removeAttr('disabled');
                //    //            $("#StateId").append($("<option></option>").val("0").html("Select State"));
                //    //            $.each(result.Table, function (index, value) {
                //    //                $("#StateId").append($("<option></option>").val(value.State_Id).html(value.StateName));
                //    //            });
                //    //        } else {
                //    //            $("#StateId").append($("<option></option>").val("0").html("Select State"));
                //    //        }
                //    //        //if (result.Table2 !== undefined && result.Table2.length > 0) {
                //    //        //    $("#tbodyid").empty();
                //    //        //    BindGrid(result.Table2, 0, 1);
                //    //        //}
                //    //    }
                //    //});
                //} else {
                //    //$("#SearchOrderNo").val("");
                //    //$("#StateId").empty();
                //    //$("#StateId").append($("<option></option>").val("0").html("Select State"));
                //    //$("#tbodyid").empty();
                //    //$("#lblTotalAmount").text("Total : 0.00");
                //    //$("#IsUpdateMaterialSales").val(0);
                //    //$("#SaleAmount").val("0");
                //}
            },
            minLength: 1
        });


        $("#DriverNOSearch").autocomplete({
            source: function (request, response) {
                IMSC.ajaxCall("GET", "/Material/SearchDriverName?DriverName=" + request.term + "&AppToken=" + scope.AppToken, {}, "text", function (d) {
                    var result = JSON.parse(d);
                    if (result.length === 0) {
                        $("#empty-message").text("No results found");
                        return response({ label: "No results found", value: 0 });
                    } else {
                        response($.map(result, function (item) {
                            return { label: item.Title, value: item.Id };
                        }))
                    }
                });
            },
            select: function (e, i) {
                // $("#VehicleNOSearch").val(i.item.label);
                //VehicleNo = parseInt(i.item.value);
                DriverName = (i.item.label);
                // alert(VehicleNo)
                // $("#SearchOrderNo").removeAttr('disabled');
                // let OfficeId = parseInt($('#OfficeId').val());

                //$("#PartyId").val(PartyId);
                //if (PartyId > 0) {
                //    $("#tbodyid").empty();
                //    //IMSC.ajaxCall("GET", "/Material/GetStateSales11?PartyId=" + PartyId + "&OfficeId=" + OfficeId + "&AppToken=" + scope.AppToken, {}, "text", function (d) {

                //    //    var result = JSON.parse(d);
                //    //    if (result !== null)
                //    //    {
                //    //        $("#StateId").empty();
                //    //        if (result.Table.length > 0) {
                //    //            $("#StateId").removeAttr('disabled');
                //    //            $("#StateId").append($("<option></option>").val("0").html("Select State"));
                //    //            $.each(result.Table, function (index, value) {
                //    //                $("#StateId").append($("<option></option>").val(value.State_Id).html(value.StateName));
                //    //            });
                //    //        } else {
                //    //            $("#StateId").append($("<option></option>").val("0").html("Select State"));
                //    //        }
                //    //        //if (result.Table2 !== undefined && result.Table2.length > 0) {
                //    //        //    $("#tbodyid").empty();
                //    //        //    BindGrid(result.Table2, 0, 1);
                //    //        //}
                //    //    }
                //    //});
                //} else {
                //    //$("#SearchOrderNo").val("");
                //    //$("#StateId").empty();
                //    //$("#StateId").append($("<option></option>").val("0").html("Select State"));
                //    //$("#tbodyid").empty();
                //    //$("#lblTotalAmount").text("Total : 0.00");
                //    //$("#IsUpdateMaterialSales").val(0);
                //    //$("#SaleAmount").val("0");
                //}
            },
            minLength: 1
        });


    };
    return scope;
})(IMSMaterialPurchase || {});

function Remove(TripAdvanceId) {
    //debugger;
    let apptoken = $("#hdnApptoken").val();
    document.getElementById("trRow_" + TripAdvanceId).remove();

    if (confirm("Do you want to remove this Item.", "Remove")) {

        //var table = $("#tblTripAdvance")[0];

        if (TripAdvanceId > 0) {
            IMSC.ajaxCall("GET", "/Material/DeleteTripAdvance?TripAdvanceId=" + TripAdvanceId + "&AppToken=" + apptoken, {}, "text", function (d) {
                var result = JSON.parse(d);
                debugger;
                if (result.IsSucceed) {
                    alert(result.ActionMsg);
                }
            });
        }
    }
}


function Edit(TripAdvanceId) {
    //debugger;
    let apptoken = $("#hdnApptoken").val();
  
        if (TripAdvanceId > 0) {
            IMSC.ajaxCall("GET", "/Material/EditTripAdvance?TripAdvanceId=" + TripAdvanceId + "&AppToken=" + apptoken, {}, "text", function (d) {
                var result = JSON.parse(d);
               
                if (result.Id > 0)
                {
                    //debugger;
                    // alert('Data Found');
                    
                    $('#Id').val(result.Id)
                    $('#Office_Id').val(result.Office_Id)
                   // $('#ADVANCEDATE').val(result.AdvanceDateString)
                    $('#QTY').val(result.QTY)
                    $('#RATE').val(result.RATE)
                    $('#AMOUNT').val(result.AMOUNT)
                    $('#ADVANCENO').val(result.ADVANCENO)
                    $('#VEHICLENO').val(result.VEHICLENO)

                    $('#DRACCOUNT').val(result.DRACCOUNT)
                    $('#CRACCOUNT').val(result.CRACCOUNT)
                    $('#REMARK').val(result.REMARK)

                    $('#ADVANCETYPE').val(result.ADVANCETYPE)
                    $('#TRIPNO').val(result.TRIPNO)
                    $('#DRIVERNAME').val(result.DRIVERNAME)
                    $('#IMAGEPath').val(result.IMAGEPath)

                    $('#btnSubmit').text('Update')
                   // console.log($('#ADVANCEDATE').val(new Date(result.AdvanceDateString)))
                    //alert(result.AdvanceDateString)
                    var now = new Date(result.AdvanceDateString);
                    now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
                    document.getElementById('ADVANCEDATE').value = now.toISOString().slice(0, 16);

                }
            });
        }
    
}

   

