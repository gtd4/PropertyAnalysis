requirejs.config(
    {
        baseUrl: 'scripts',
        shim:{
            bootstrap : {'deps':['jquery']}
        },
        paths: {
            jquery: '/scripts/jquery-1.10.2',
            jqueryValidate: '/scripts/jquery.validate',
            jqueryValidateUnobtrusive: "/scripts/jquery.validate.unobtrusive",
            ko: '/scripts/knockout-3.4.0.debug',
            koMapping: '/scripts/knockout.mapping-latest.debug',
            bootstrap: '/scripts/bootstrap',
            
            
        }
    });
requirejs(['jquery', 'bootstrap', 'ko'],
    function (ko,$) {

    });