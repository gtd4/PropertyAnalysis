requirejs.config(
    {
        baseUrl: 'scripts',
        shim: {
            bootstrap: { 'deps': ['jquery'] },
            jqueryValidate: { 'deps': ["jquery"] },
            jqueryValidateUnobtrusive: { 'deps': ["jquery", "jqueryValidate"] },
            knockout: {
                'deps': ['jquery'],
            }
        },
        paths: {
            jquery: '/scripts/jquery-1.10.2',
            jqueryValidate: '/scripts/jquery.validate',
            jqueryValidateUnobtrusive: "/scripts/jquery.validate.unobtrusive",
            knockout: '/scripts/knockout-3.4.0.debug',
            koMapping: '/scripts/knockout.mapping-latest.debug',
            bootstrap: '/scripts/bootstrap',
            viewModel: '/scripts/ViewModel',
            koBindings: '/scripts/CustomKOBindings',
            text: '/scripts/text',
            components: '/scripts/Components'
        },
    });

requirejs(['jquery', 'bootstrap'],
    function ($) {

       
    });