
(function ($) {
    $.fn.ajaxForm = function (options) {

        var settings = $.extend({

            url: '',
            method: 'POST',
            validation: true,
            JWT: true,
            additionalData: {},
            success: function (response) { },
            error: function (xhr, status, error) { }
        }, options);

        this.each(function () {

            var form = $(this);
            form.on('submit', function (e) {

                if (settings.validation && this.checkValidity() === false) {
                    e.preventDefault();
                    e.stopPropagation();
                    this.classList.add('was-validated');
                }
                else {

                    var _data = form.serialize()
                    console.log(settings.additionalData);

                    $.ajax({
                        url: settings.url || form.attr('action'),
                        method: settings.method || form.attr('method'),
                        data: _data,
                        beforeSend: function (xhr) {

                            $('#load_screen').show()

                            if (settings.Authorization) {

                                var auth = localStorage.getItem('myKey');
                                xhr.setRequestHeader('Authorization', 'Bearer ' + auth);

                            }
                        },
                        success: function (response) {

                            $('#load_screen').hide()
                            settings.success(response);

                        },
                        error: function (xhr, status, error) {
                            $('#load_screen').hide()
                            settings.error(xhr, status, error);
                        }
                    });
                }

            });

        });

        return this;
    };
})(jQuery);