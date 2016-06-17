define(["knockout"],
    function(ko)
    {
        var practiceComponent = function (params) {
            var text = params.initialText || ''
            this.text = ko.observable(text);
        };

        return practiceComponent;
    })
