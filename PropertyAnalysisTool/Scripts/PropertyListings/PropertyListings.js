
var compareList = [];
var $loading = $('.ajax-loader').hide();
var $propList = $('#property-list');
$(document)
  .ajaxStart(function () {
      $loading.show();
      $propList.hide();
  })
  .ajaxStop(function () {
      $loading.hide();
      $propList.show();
  });

function UpdateFilter(localityId, districtId, SuburbId, minBath, maxBath, minBed, maxBed, minPrice, maxPrice) {
    $.ajax({
        url: "/PropertyFilter/Index",
        type: "GET",
        data: {
            LocalityId: localityId,
            DistrictId: districtId,
            SuburbId: SuburbId,
            minBedroom: minBed,
            maxBedroom: maxBed,
            minBathroom: minBath,
            maxbathroom: maxBath,
            PriceMin: minPrice,
            PriceMax: maxPrice,
            rng: Math.random()
        }
    }).done(function (partialViewResult) {

        $("#propertyfilter-container").html(partialViewResult);

    });

    UpdateProperties(localityId, districtId, SuburbId, minBath, maxBath, minBed, maxBed, minPrice, maxPrice, 1);
}

function AddToHistory(localityId, districtId, suburbId, minBath, maxBath, minBed, maxBed, minPrice, maxPrice, page) {
    var item = {
        LocalityId: localityId,
        DistrictId: districtId,
        SuburbId: suburbId,
        MinBatch: minBath,
        MaxBath: maxBath,
        MinBed: minBed,
        MaxBed: maxBed,
        MinBed: minPrice,
        MaxPrice: maxPrice,
        Page: page,
    }
    var url = BuildUrl(localityId, districtId, suburbId, minBath, maxBath, minBed, maxBed, minPrice, maxPrice, page)
    history.replaceState(item, '', url);

}

function CheckIfInCompareList()
{
    $(".compare-button").each(function ()
    {
        var propId = $(this).val();

        if (jQuery.inArray(propId, compareList) != -1) {
            //update button colour
            $(this).removeClass("btn-primary");
            $(this).addClass("btn-danger");
            $(this).addClass("compare-delete-button");
            $(this).removeClass("compare-button");

            //update button text
            $(this).text("Remove From Compare List");
            return true;
        }

        
    })
    
}

function BuildUrl(localityId, districtId, suburbId, minBath, maxBath, minBed, maxBed, minPrice, maxPrice, page)
{
    var url = "?";
    if (localityId != 0) {
        url += "region=" + localityId + "&";
    }

    if (districtId != 0) {
        //sb.AppendFormat("&district={0}", districtId);
        url += "district=" + districtId + "&";
    }

    if (suburbId != 0) {
        //sb.AppendFormat("&suburb={0}", suburbId);
        url += "suburb=" + suburbId + "&";
    }

    if (minBed != 0) {
        //sb.AppendFormat("&bedrooms_min={0}", minBed);
        url += "bedrooms_min=" + minBed + "&";
    }

    if (maxBed != 0) {
        //sb.AppendFormat("&bedrooms_max={0}", maxBed);
        url += "bedrooms_max=" + maxBed + "&";
    }

    if (minBath != 0) {
        //sb.AppendFormat("&bathrooms_min={0}", minBath);
        url += "bathrooms_min=" + minBath + "&";
    
    }

    if (maxBath != 0) {
        //sb.AppendFormat("&bathrooms_max={0}", maxBath);
        url += "bathrooms_max=" + maxBath + "&";
    }

    if (minPrice != 0) {
        //sb.AppendFormat("&price_min={0}", priceMin);
        url += "price_min=" + minPrice + "&";
    }

    if (maxPrice != 0) {
        //sb.AppendFormat("&price_max={0}", priceMax);
        url += "price_max=" + maxPrice + "&";
    }

    url += "page=" + page;
    return url;
    
}

function UpdateProperties(localityId, districtId, suburbId, minBath, maxBath, minBed, maxBed, minPrice, maxPrice, page) {
    AddToHistory(localityId, districtId, suburbId, minBath, maxBath, minBed, maxBed, minPrice, maxPrice, page)
    $.ajax({
        url: "/Home/UpdatePropertyListings",
        type: "GET",
        data: {
            LocalityId: localityId,
            DistrictId: districtId,
            SuburbId: suburbId,
            minBedroom: minBed,
            maxBedroom: maxBed,
            minBathroom: minBath,
            maxbathroom: maxBath,
            PriceMin: minPrice,
            PriceMax: maxPrice,
            Page: page,
            rng: Math.random()
        }
    }).done(function (partialViewResult) {
        
        window.scrollTo(0, 0);
        $("#property-list").html(partialViewResult);
        CheckIfInCompareList();

    }).error(function (partialViewResult) {
        console.log(partialViewResult);
    });
}

function BuildItemContainer(propId) {

    var itemContainer = jQuery("<div/>",
    {
        class: 'compare-item-container col-sm-12',
        id: propId,

    });

    return itemContainer;
}

function BuildTitleContainer(title) {

    var titleContainer = jQuery("<div/>",
    {
        class: 'compare-title-container',

    });

    var title = jQuery("<p/>",
        {
            text: title,
        }).appendTo(titleContainer);

    return titleContainer;
}

function BuildImgContainer(imgSrc) {

    var imgContainer = jQuery("<div/>",
        {
            class: 'compare-img-container',
        });
    var img = jQuery("<img/>",
        {
            src: imgSrc,

        }).appendTo(imgContainer);

    return imgContainer;

}

function BuildDeleteContainer(propId) {
    var deleteContainer = jQuery("<div/>",
        {
            class: "compare-delete-container",
        });

    var deleteButton = jQuery("<button/>",
    {
        class: "btn btn-danger compare-delete-button",
        text: "Remove",
        value: propId,
    }).appendTo(deleteContainer);

    return deleteContainer;
}

function ToggleComparePropertyButton() {

    if (compareList.length > 1) {
        $(".compare-properties").show();
    }
    else {
        $(".compare-properties").hide();
    }
}

function BuildCompareListItem(title, imgSrc, propId) {

    if (compareList.length >= 3) {
        alert("You can only compare a max of 3 properties at 1 time");
        return true;
    }



    if (jQuery.inArray(propId, compareList) != -1) {
        alert("You have already added this property to be compared");
        return true;
    }

    compareList.push(propId);

    ToggleComparePropertyButton();

    var compareListContainer = $(".compare-list");

    var itemContainer = BuildItemContainer(propId);
    var imgContainer = BuildImgContainer(imgSrc);
    var titleContainer = BuildTitleContainer(title);
    var deleteContainer = BuildDeleteContainer(propId);

    $(imgContainer).appendTo(itemContainer);
    $(titleContainer).appendTo(itemContainer);
    $(deleteContainer).appendTo(itemContainer);

    $(itemContainer).appendTo(compareListContainer);

}

function RemoveFromCompareList(propId) {
    var index = jQuery.inArray(propId, compareList);

    if (index != -1) {
        compareList.splice(index, 1);
        return true;
    }
}

function FilterUpdate(location, district, suburb) {

    var minBed = $("#MinBedRoom").val();
    var maxBed = $("#MaxBedRoom").val();
    var minBath = $("#MinBathRoom").val();
    var maxBath = $("#MaxBathRoom").val();
    var minPrice = $("#MinPrice").val();
    var maxPrice = $("#MaxPrice").val();

    UpdateFilter(location, district, suburb, minBath, maxBath, minBed, maxBed, minPrice, maxPrice)
}


function PropertyUpdate(page) {
    var location = $("#SelectedLocationId").val();
    var district = $("#SelectedDistrictId").val();
    var suburb = $("#SelectedSuburbId").val();
    var minBed = $("#MinBedRoom").val();
    var maxBed = $("#MaxBedRoom").val();
    var minBath = $("#MinBathRoom").val();
    var maxBath = $("#MaxBathRoom").val();
    var minPrice = $("#MinPrice").val();
    var maxPrice = $("#MaxPrice").val();

    UpdateProperties(location, district, suburb, minBath, minBath, minBed, maxBed, minPrice, maxPrice, page);

}


$(function () {

    

    $("#propertyfilter-container").on('change', '#SelectedLocationId', function () {

        var location = $("#SelectedLocationId").val();
        FilterUpdate(location, 0, 0);

    });

    $("#propertyfilter-container").on('change', '#SelectedDistrictId', function () {
        var location = $("#SelectedLocationId").val();
        var district = $("#SelectedDistrictId").val();

        FilterUpdate(location, district, 0);

    });

    $("#propertyfilter-container").on('change', '#SelectedSuburbId', function () {
        var location = $("#SelectedLocationId").val();
        var district = $("#SelectedDistrictId").val();
        var suburb = $("#SelectedSuburbId").val();

        FilterUpdate(location, district, suburb);
    });

    $("#propertyfilter-container").on('change', '#MinBedRoom', function () {

        PropertyUpdate(1);
    });



    $("#propertyfilter-container").on('change', '#MaxBedRoom', function () {

        PropertyUpdate(1);
    });

    $("#propertyfilter-container").on('change', '#MinBathRoom', function () {

        PropertyUpdate(1);
    });

    $("#propertyfilter-container").on('change', '#MaxBathRoom', function () {

        PropertyUpdate(1);
    });

    $("#propertyfilter-container").on('change', '#MinPrice', function () {

        PropertyUpdate(1);
    });

    $("#propertyfilter-container").on('change', '#MaxPrice', function () {

        PropertyUpdate(1);
    });

    $("#property-list").on("click", ".page", function (e) {

        e.preventDefault();

        var page = $(this).val();

        PropertyUpdate(page);

    });

    $("#property-list").on("click", "#previous", function (e) {
        e.preventDefault();

        var page = $(".active").val();

        page--;
        PropertyUpdate(page);




    });

    $("#property-list").on("click", "#next", function (e) {
        e.preventDefault();

        var location = $("#SelectedLocationId").val();
        var district = $("#SelectedDistrictId").val();
        var suburb = $("#SelectedSuburbId").val();
        var minBed = $("#MinBedRoom").val();
        var maxBed = $("#MaxBedRoom").val();
        var minBath = $("#MinBathRoom").val();
        var maxBath = $("#MaxBathRoom").val();
        var minPrice = $("#MinPrice").val();
        var maxPrice = $("#MaxPrice").val();
        var page = $(".active").val();

        page++;
        PropertyUpdate(page);




    });

    $("#property-list").on("click", ".compare-button", function (e) {

        e.preventDefault();

        var id = $(this).val();
        var title = $(this).siblings().children("a").children(".prop-title").text();
        var image = $(this).siblings().children("a").children(".prop-img").prop("src");

        BuildCompareListItem(title, image, id);

        //update button colour
        $(this).removeClass("btn-primary");
        $(this).addClass("btn-danger");
        $(this).addClass("compare-delete-button");
        $(this).removeClass("compare-button");

        //update button text
        $(this).text("Remove From Compare List");

        return true;

    });

    $("#property-list").on("click", ".compare-delete-button", function (e) {
        var propId = $(this).val();
        //remove entire container
        var selector = "#" + propId;

        $(".compare-list").find(selector).remove();

        //remove item for compareList
        RemoveFromCompareList(propId);

        //hide compare button
        ToggleComparePropertyButton();

        //update button colour
        $(this).removeClass("btn-danger");
        $(this).addClass("btn-primary");
        $(this).addClass("compare-button");
        $(this).removeClass("compare-delete-button");

        //update button text
        $(this).text("Add To Compare List");
    });

    $(".compare-list").on('click', '.compare-delete-button', function () {

        var propId = $(this).val();
        //remove entire container
        var selector = "#" + propId;

        $(".compare-list").find(selector).remove();

        //remove item for compareList
        RemoveFromCompareList(propId);

        //hide compare button
        ToggleComparePropertyButton();

        var propListButton = $("#property-list").find(selector);

        //update button colour
        $(propListButton).removeClass("btn-danger");
        $(propListButton).addClass("btn-primary");
        $(propListButton).addClass("compare-button");
        $(propListButton).removeClass("compare-delete-button");

        //update button text
        $(propListButton).text("Add To Compare List");

    });

    $(".compare-properties").click(function () {

        var url = "/home/compare?";

        for (var i = 0; i < compareList.length; i++) {
            url = url.concat("id" + (i + 1) + "=" + compareList[i] + "&");
        }

        window.location = url;
    });

});