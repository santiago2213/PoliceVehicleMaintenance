$(document).ready(function () {
    $("#VehicleModelGroup").hide();
});

$("#VehicleMakeId").change(function ()
{
    var selectedMake = $(this).val();
    if (selectedMake == "")
    {
        $("#VehicleModelGroup").hide();
    }
    else
    {
        $("#VehicleModelGroup").show();
        GetAjaxData(selectedMake);
    }
});

function GetAjaxData(selectedMake)
{
    $("#VehicleModelId").empty();
    $.ajax(
        {
            type: "GET",
            url: "GetModelsForMake",
            data: { makeId: selectedMake },
            success: function(models) {
                var items = "";
                items += "<option value =' '>None selected</option>"
                $.each(models, function (i, Model) {
                    items += "<option value='" + Model.value + "'>" + Model.text + "</option>";
                }); //end of each
                $("#VehicleModelId").html(items)
            } //end of success
        }); //end of ajax

}
        
