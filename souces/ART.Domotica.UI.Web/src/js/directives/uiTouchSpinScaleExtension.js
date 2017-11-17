angular.module('app')
    .directive('uiTouchSpinScaleExtension', function ($timeout, $parse) {
        return {
            link: function (scope, element, attr) {
                var postFixElement = $(element);
                scope.$watch(attr.uiTouchSpinScaleExtension, function (newValue, oldValue) {
                    if (newValue) {
                        var postfixElement = element.parent().find('.bootstrap-touchspin-postfix');
                        if (postfixElement.length > 0) {
                            postfixElement.text(newValue.symbol);
                        }
                    }
                });
            }
        };
    });