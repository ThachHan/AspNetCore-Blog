(function ($) {
    "use strict"

    // Mobile dropdown
    $('.has-dropdown>a').on('click', function () {
        $(this).parent().toggleClass('active');
    });

    // Aside Nav
    $(document).click(function (event) {
        if (!$(event.target).closest($('#nav-aside')).length) {
            if ($('#nav-aside').hasClass('active')) {
                $('#nav-aside').removeClass('active');
                $('#nav').removeClass('shadow-active');
            } else {
                if ($(event.target).closest('.aside-btn').length) {
                    $('#nav-aside').addClass('active');
                    $('#nav').addClass('shadow-active');
                }
            }
        }
    });

    $('.nav-aside-close').on('click', function () {
        $('#nav-aside').removeClass('active');
        $('#nav').removeClass('shadow-active');
    });


    $('.search-btn').on('click', function () {
        $('#nav-search').toggleClass('active');
    });

    $('.search-close').on('click', function () {
        $('#nav-search').removeClass('active');
    });


    $('.media-body .reply').on('click', function (e) {
        e.preventDefault();
        $('#form-comment #CommentParentId').val($(this).data('commentid'));
        $('html, body').animate({
            scrollTop: $("#form-comment").offset().top
        }, 2000);
    });


    $('#btn-login').on('click', function (e) {
        e.preventDefault();
        window.location.href = '/Identity/Account/Login';
    });

    $('#btn-send-comment').on('click', function (e) {
        e.preventDefault();
        if ($("#form-comment").valid()) {
            $.ajax({
                url: '/Comment/SaveCommentAsync',
                type: 'post',
                data: $("#form-comment").serialize(),
                success: function (data) {
                    if (data) {
                        window.location.reload();
                    } else {
                        alert("error");
                    }
                },
            });
        }
    });

    $('#submit-subscribe').on('click', function (e) {
        e.preventDefault();
        if ($("#form-subscribe").valid()) {
            $.ajax({
                url: '/NewsLetters/SaveSubscribe',
                type: 'post',
                data: $("#form-subscribe").serialize(),
                success: function (data) {
                    if (data) {
                        $("#form-subscribe #Email").val('');
                        swal("Subscribe successfully!", "Thank you very much !", "success")
                    }
                },
            });
        }
    });


    $('#submit-subscribe-footer').on('click', function (e) {
        e.preventDefault();
        if ($("#form-subscribe-footer").valid()) {
            $.ajax({
                url: '/NewsLetters/SaveSubscribe',
                type: 'post',
                data: $("#form-subscribe-footer").serialize(),
                success: function (data) {
                    if (data) {
                        $("#form-subscribe-footer #Email").val('');
                        swal("Subscribe successfully!", "Thank you very much !", "success")
                    }
                },
            });
        }
    });

    $('#auto-search').keydown(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            var searchString = $('#auto-search').val();
            if (searchString != '')
            {
                $('#form-search').submit();
            }
        }
    });

    // Parallax Background
    $.stellar({
        responsive: true
    });
})(jQuery);
