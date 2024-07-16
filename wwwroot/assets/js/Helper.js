(function ($) {
    $.fn.ajaxForm = function (options) {

        var settings = $.extend({

            url: '',
            method: 'POST',
            validation: true,
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
                    $.ajax({
                        url: settings.url || form.attr('action'),
                        method: settings.method || form.attr('method'),
                        data: _data,
                        success: function (response) {
                            settings.success(response);
                        },
                        error: function (xhr, status, error) {
                            settings.error(xhr, status, error);
                        }
                    });
                }

            });

        });

        return this;
    };
})(jQuery);