var inputBlock;
var inputBlocksAmount = 1;
var inputDeleteIsShown = false;
var inputBlockPart;

$(function () {
    $('.dreamstory-input').hide(0);
    inputBlock = $('.input-block').html();
    inputBlockPart = $('.dreamstory-input-part').clone();
    $('.dreaminput-delete-btn').hide(0);
})

$('.input-dreams-trigger').focus(function () {
    $('.dreamstory-input').slideDown(200);
    $('.dreams-input-trigger-main').focus();
    $('.input-dreams-trigger-div').slideUp(400);
})

$('.dream-input-cancel').click(function () {
    $('.dreamstory-input').slideUp(400);
    $('.input-dreams-trigger-div').slideDown(400);
})

$(document).on('click', '.dreaminput-new-section-btn', function () {
    $(this.parentElement.parentElement).after(inputBlock);

    if (!inputDeleteIsShown) {
        $('.dreaminput-delete-btn').show(400);
        inputDeleteIsShown = true;
    }

    inputBlocksAmount++;
})

$(document).on('click', '.dreaminput-delete-btn', function () {
    if (inputBlocksAmount > 1) {
        $(this.parentElement).remove();
        inputBlocksAmount--;

        if (inputBlocksAmount < 2) {
            $('.dreaminput-delete-btn').hide(400);
            inputDeleteIsShown = false;
        }
    }
    else {
        alert('You cat not remove only one input field.');
    }

})

$('.dream-input-cancel').click(function () {
    location.reload();
})