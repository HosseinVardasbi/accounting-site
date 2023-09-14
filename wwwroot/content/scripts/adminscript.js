
$(document).ready(function () {

    checkInputFull();
});

//-- Menu
$(document).on('click', '.menu > li.has-sub', function (event) {

    var subUl = $(this).find('ul');

    var liCount = subUl.find("li").length;
    var ulHeight = liCount * 35;

    if (subUl.hasClass("open-ul") && $(event.target).closest('li.has-sub > a').length) {
        $(this).removeClass("open-li");
        subUl.closest("ul").css("height", "0px").removeClass("open-ul");

    } else {
        $(this).addClass("open-li");
        subUl.css("height", ulHeight + "px").addClass("open-ul");
    }
    $('.menu > li').not(this).removeClass("open-li");
    $('.menu > li').not(this).find("ul").css("height", "0px").removeClass("open-ul");

});



$(document).on('click', '.minimize-menu', function () {
    if ($('.sidebar').hasClass('show-menu')) {
        $('.sidebar').removeClass('show-menu');
        //$('.panel-holder').removeClass('show-menu');

    }
    else {
        $('.sidebar').addClass('show-menu');
        //$('.panel-holder').addClass('show-menu');
    }
});
$(document).on('click', '.close-menu', function () {
    $('.sidebar').removeClass('show-menu');
});
//-- Input Text

$(document).on('focus', '.input-text > input, .input-text > textarea', function () {

    $(this).parent('div').addClass('is-focus');
    $(this).parent('div').removeClass('is-full');

});

$(document).on('blur', '.input-text > input, .input-text > textarea', function () {

    $(this).parent('div').removeClass('is-focus');

    if ($(this).val().length > 0) {
        $(this).parent('div').addClass('is-full');
    } else {
        $(this).parent('div').removeClass('is-full');
    }

});
$(document).on('change', '.input-text > input, .input-text > textarea', function () {
    $(this).blur();
});

$(document).on('click', '.input-text h5', function () {
    $(this).parent().children('input').focus();
});


function checkInputFull() {
    $('.input-text > input, .input-text > textarea').blur();
}

//-- File Upload

$(document).on('change', '.input-file input[type=file]', function () {
    var fileName = $(this).val();
    if (fileName.length > 0) {
        $(this).parent().parent().children('em').html(fileName);
        $(this).parent().parent().addClass('is-select');
    } else {
        $(this).parent().parent().children('em').html("یک فایل انتخاب کنید ...");
        $(this).parent().parent().removeClass('is-select');
    }

});


//-- Number

$('<div class="quantity-nav"><div class="quantity-button quantity-up"></div><div class="quantity-button quantity-down"></div></div>').insertAfter('.input-number input');
$('.input-number').each(function () {
    var spinner = jQuery(this),
        input = spinner.find('input[type="number"]'),
        btnUp = spinner.find('.quantity-up'),
        btnDown = spinner.find('.quantity-down'),
        min = input.attr('min'),
        max = input.attr('max');

    btnUp.click(function () {
        var oldValue = parseFloat(input.val());
        if (isNaN(oldValue)) {
            oldValue = 0;
        }
        if (oldValue >= max) {
            var newVal = oldValue;
        } else {
            var newVal = oldValue + 1;
        }
        spinner.find("input").val(newVal);
        spinner.find("input").trigger("change");
    });

    btnDown.click(function () {
        var oldValue = parseFloat(input.val());
        if (isNaN(oldValue)) {
            oldValue = 0;
        }
        if (oldValue <= min) {
            var newVal = oldValue;
        } else {
            var newVal = oldValue - 1;
        }
        spinner.find("input").val(newVal);
        spinner.find("input").trigger("change");
    });

});




// Time

getPersianDate = (format) => {
    let week = new Array("يكشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه")
    let months = new Array("فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند");
    let today = new Date();
    let d = today.getDay();
    let day = today.getDate();
    let month = today.getMonth() + 1;
    let year = today.getYear();
    year = (window.navigator.userAgent.indexOf('MSIE') > 0) ? year : 1900 + year;
    if (year == 0) {
        year = 2000;
    }
    if (year < 100) {
        year += 1900;
    }
    y = 1;
    for (i = 0; i < 3000; i += 4) {
        if (year == i) {
            y = 2;
        }
    }
    for (i = 1; i < 3000; i += 4) {
        if (year == i) {
            y = 3;
        }
    }
    if (y == 1) {
        year -= ((month < 3) || ((month == 3) && (day < 21))) ? 622 : 621;
        switch (month) {
            case 1:
                (day < 21) ? (month = 10, day += 10) : (month = 11, day -= 20);
                break;
            case 2:
                (day < 20) ? (month = 11, day += 11) : (month = 12, day -= 19);
                break;
            case 3:
                (day < 21) ? (month = 12, day += 9) : (month = 1, day -= 20);
                break;
            case 4:
                (day < 21) ? (month = 1, day += 11) : (month = 2, day -= 20);
                break;
            case 5:
            case 6:
                (day < 22) ? (month -= 3, day += 10) : (month -= 2, day -= 21);
                break;
            case 7:
            case 8:
            case 9:
                (day < 23) ? (month -= 3, day += 9) : (month -= 2, day -= 22);
                break;
            case 10:
                (day < 23) ? (month = 7, day += 8) : (month = 8, day -= 22);
                break;
            case 11:
            case 12:
                (day < 22) ? (month -= 3, day += 9) : (month -= 2, day -= 21);
                break;
            default:
                break;
        }
    }
    if (y == 2) {
        year -= ((month < 3) || ((month == 3) && (day < 20))) ? 622 : 621;
        switch (month) {
            case 1:
                (day < 21) ? (month = 10, day += 10) : (month = 11, day -= 20);
                break;
            case 2:
                (day < 20) ? (month = 11, day += 11) : (month = 12, day -= 19);
                break;
            case 3:
                (day < 20) ? (month = 12, day += 10) : (month = 1, day -= 19);
                break;
            case 4:
                (day < 20) ? (month = 1, day += 12) : (month = 2, day -= 19);
                break;
            case 5:
                (day < 21) ? (month = 2, day += 11) : (month = 3, day -= 20);
                break;
            case 6:
                (day < 21) ? (month = 3, day += 11) : (month = 4, day -= 20);
                break;
            case 7:
                (day < 22) ? (month = 4, day += 10) : (month = 5, day -= 21);
                break;
            case 8:
                (day < 22) ? (month = 5, day += 10) : (month = 6, day -= 21);
                break;
            case 9:
                (day < 22) ? (month = 6, day += 10) : (month = 7, day -= 21);
                break;
            case 10:
                (day < 22) ? (month = 7, day += 9) : (month = 8, day -= 21);
                break;
            case 11:
                (day < 21) ? (month = 8, day += 10) : (month = 9, day -= 20);
                break;
            case 12:
                (day < 21) ? (month = 9, day += 10) : (month = 10, day -= 20);
                break;
            default:
                break;
        }
    }
    if (y == 3) {
        year -= ((month < 3) || ((month == 3) && (day < 21))) ? 622 : 621;
        switch (month) {
            case 1:
                (day < 20) ? (month = 10, day += 11) : (month = 11, day -= 19);
                break;
            case 2:
                (day < 19) ? (month = 11, day += 12) : (month = 12, day -= 18);
                break;
            case 3:
                (day < 21) ? (month = 12, day += 10) : (month = 1, day -= 20);
                break;
            case 4:
                (day < 21) ? (month = 1, day += 11) : (month = 2, day -= 20);
                break;
            case 5:
            case 6:
                (day < 22) ? (month -= 3, day += 10) : (month -= 2, day -= 21);
                break;
            case 7:
            case 8:
            case 9:
                (day < 23) ? (month -= 3, day += 9) : (month -= 2, day -= 22);
                break;
            case 10:
                (day < 23) ? (month = 7, day += 8) : (month = 8, day -= 22);
                break;
            case 11:
            case 12:
                (day < 22) ? (month -= 3, day += 9) : (month -= 2, day -= 21);
                break;
            default:
                break;
        }
    }
    if (format === null || format === undefined)
        return `${week[d]} ${day} ${months[month - 1]} ${year}`
    if (format === "y/m/d")
        return `${year}/${month}/${day}`;
    if (format === "d/m/y")
        return `${day}/${month}/${year}`;
}

$('.date-time').html('امروز ' + getPersianDate());


$(document).on("keyup", ".price_type", function () {

    char = $(this).val();

    $(this).val(priceFormat(char));

})

$(".price-format").each(function(index) {
    char = $(this).html();

    $(this).html(priceFormat(char) + " ریال");
});

function priceFormat(n) {

    n = n + "";
    if (n !== '') {
        var num = n.replace(/[^\d]/g, '');
        if (num.length > 3)
            num = num.replace(/\B(?=(?:\d{3})+(?!\d))/g, ',');
        return num;
    }
}




function create_custom_dropdowns() {
    $('.search-select').each(function (i, select) {
        if (!$(this).next().hasClass('dropdown-select')) {
            $(this).after('<div class="dropdown-select wide ' + ($(this).attr('class') || '') + '" tabindex="0"><span class="current"></span><div class="list"><ul></ul></div></div>');
            var dropdown = $(this).next();
            var options = $(select).find('option');
            var selected = $(this).find('option:selected');
            dropdown.find('.current').html(selected.data('display-text') || selected.text());
            options.each(function (j, o) {
                var display = $(o).data('display-text') || '';
                dropdown.find('ul').append('<li class="option ' + ($(o).is(':selected') ? 'selected' : '') + '" data-value="' + $(o).val() + '" data-display-text="' + display + '">' + $(o).text() + '</li>');
            });
        }
    });

    $('.dropdown-select ul').before('<div class="dd-search"><input id="txtSearchValue" autocomplete="off" onkeyup="filter()" class="dd-searchbox" type="text"></div>');
}

// Event listeners

// Open/close
$(document).on('click', '.dropdown-select', function (event) {
    if ($(event.target).hasClass('dd-searchbox')) {
        return;
    }
    $('.dropdown-select').not($(this)).removeClass('open');
    $(this).toggleClass('open');
    if ($(this).hasClass('open')) {
        $(this).find('.option').attr('tabindex', 0);
        $(this).find('.selected').focus();
    } else {
        $(this).find('.option').removeAttr('tabindex');
        $(this).focus();
    }
});

// Close when clicking outside
$(document).on('click', function (event) {
    if ($(event.target).closest('.dropdown-select').length === 0) {
        $('.dropdown-select').removeClass('open');
        $('.dropdown-select .option').removeAttr('tabindex');
    }
    event.stopPropagation();
});

function filter() {
    var valThis = $('#txtSearchValue').val();
    $('.dropdown-select ul > li').each(function () {
        var text = $(this).text();
        (text.toLowerCase().indexOf(valThis.toLowerCase()) > -1) ? $(this).show() : $(this).hide();
    });
};
// Search

// Option click
$(document).on('click', '.dropdown-select .option', function (event) {
    $(this).closest('.list').find('.selected').removeClass('selected');
    $(this).addClass('selected');
    var text = $(this).data('display-text') || $(this).text();
    $(this).closest('.dropdown-select').find('.current').text(text);
    $(this).closest('.dropdown-select').prev('select').val($(this).data('value')).trigger('change');
});

// Keyboard events
$(document).on('keydown', '.dropdown-select', function (event) {
    var focused_option = $($(this).find('.list .option:focus')[0] || $(this).find('.list .option.selected')[0]);
    // Space or Enter
    //if (event.keyCode == 32 || event.keyCode == 13) {
    if (event.keyCode == 13) {
        if ($(this).hasClass('open')) {
            focused_option.trigger('click');
        } else {
            $(this).trigger('click');
        }
        return false;
        // Down
    } else if (event.keyCode == 40) {
        if (!$(this).hasClass('open')) {
            $(this).trigger('click');
        } else {
            focused_option.next().focus();
        }
        return false;
        // Up
    } else if (event.keyCode == 38) {
        if (!$(this).hasClass('open')) {
            $(this).trigger('click');
        } else {
            var focused_option = $($(this).find('.list .option:focus')[0] || $(this).find('.list .option.selected')[0]);
            focused_option.prev().focus();
        }
        return false;
        // Esc
    } else if (event.keyCode == 27) {
        if ($(this).hasClass('open')) {
            $(this).trigger('click');
        }
        return false;
    }
});

$(document).ready(function () {
    create_custom_dropdowns();
});


$(document).on('click', '.add-payment' , function(){

    tagsHtml = '<div class="row"><div class="col-md-3"><div class="input-drop"><label> نحوه پرداخت </label><select>'+
                '<option value="0">نقدی</option><option value="1">دستگاه پوز</option><option value="2">کارت به کارت</option><option value="3">چک</option></select>'+
            '</div></div><div class="col-md-3"><div class="input-text"><label> مبلغ پرداخت</label><input type="text" class="price_type"/></div>'+
            '</div><div class="col-md-3"><div class="input-text"><label> توضیحات</label><input type="text"/></div></div>'+
    '<div class="col-md-3"><button class="btn btn-danger mt-5 remove-payment">حذف</button></div></div>';

    $('.pay-wrapper').append(tagsHtml);

})

$(document).on('click', '.remove-payment' , function(){


    $(this).closest('.row').remove();

})