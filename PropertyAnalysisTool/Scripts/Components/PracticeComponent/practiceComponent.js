define(['knockout', 'viewModel/practiceComponentViewModel', 'text!/scripts/components/PracticeComponent/practiceComponentTemplate.html'],
    function (ko, componentViewModel, template) {

        ko.components.register('message-editor', {
            viewModel: componentViewModel,
            template: template
        });

        //ko.applyBindings();
    });