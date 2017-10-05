(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
    value: true
});

var _createClass = (function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ('value' in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; })();

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

function _toConsumableArray(arr) { if (Array.isArray(arr)) { for (var i = 0, arr2 = Array(arr.length); i < arr.length; i++) arr2[i] = arr[i]; return arr2; } else { return Array.from(arr); } }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError('Cannot call a class as a function'); } }

var _LoggerJs = require('./Logger.js');

var _LoggerJs2 = _interopRequireDefault(_LoggerJs);

var EventDispatcher = (function () {
    function EventDispatcher() {
        _classCallCheck(this, EventDispatcher);

        this.listeners = new Map();
        this.eventsRunning = 0;
        this.logger = _LoggerJs2['default'];
    }

    _createClass(EventDispatcher, [{
        key: 'on',
        value: function on(event, callback) {
            if (!this.listeners.has(event)) {
                this.listeners.set(event, []);
            }
            this.listeners.get(event).push(callback);
        }
    }, {
        key: 'trigger',
        value: function trigger(event) {
            var args = [];
            for (var i = 1; i < arguments.length; i++) {
                args.push(arguments[i]);
            }

            this.log('event: "' + event + '" called with args:', args);

            var nbListeners = 0;
            if (this.listeners.has(event)) {
                nbListeners = this.listeners.get(event).length;
                this.eventsRunning++;
                var _iteratorNormalCompletion = true;
                var _didIteratorError = false;
                var _iteratorError = undefined;

                try {
                    for (var _iterator = this.listeners.get(event)[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
                        var listener = _step.value;

                        listener.apply(undefined, args);
                    }
                } catch (err) {
                    _didIteratorError = true;
                    _iteratorError = err;
                } finally {
                    try {
                        if (!_iteratorNormalCompletion && _iterator['return']) {
                            _iterator['return']();
                        }
                    } finally {
                        if (_didIteratorError) {
                            throw _iteratorError;
                        }
                    }
                }

                this.eventsRunning--;
            }

            this.log('event: "' + event + '", called: ' + nbListeners + ' time(s)');

            if (this.eventsRunning === 0) {
                this.log();
            }
        }
    }, {
        key: 'forward',
        value: function forward(event, eventToForwardTo) {
            var self = this;

            // Can't use arrow function here, because we need the arguments of the callback function
            this.on(event, function () {
                var args = [];
                for (var i = 0; i < arguments.length; i++) {
                    args.push(arguments[i]);
                }

                self.trigger.apply(self, [eventToForwardTo].concat(args));
            });
        }
    }, {
        key: 'getListeners',
        value: function getListeners(event) {
            return this.listeners.get(event);
        }
    }, {
        key: 'off',
        value: function off(event) {
            this.listeners.set(event, []);
        }
    }, {
        key: 'reset',
        value: function reset() {
            this.listeners = new Map();
        }
    }, {
        key: 'log',
        value: function log() {
            var message = arguments.length <= 0 || arguments[0] === undefined ? '' : arguments[0];
            var args = arguments.length <= 1 || arguments[1] === undefined ? undefined : arguments[1];

            var spacesBefore = _LoggerJs2['default'].createTabs(this.eventsRunning);
            if (args) {
                _LoggerJs2['default'].info.apply(_LoggerJs2['default'], [spacesBefore + message].concat(_toConsumableArray(args)));
            } else {
                _LoggerJs2['default'].info(spacesBefore + message);
            }
        }
    }]);

    return EventDispatcher;
})();

exports['default'] = EventDispatcher;
module.exports = exports['default'];

},{"./Logger.js":2}],2:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
    value: true
});
var Logger = {
    logLevel: 'info',

    info: function info() {
        if (this.logLevel == 'info') {
            var args = [];
            for (var i = 0; i < arguments.length; i++) {
                args.push(arguments[i]);
            }

            console.log.apply(console, ['info:'].concat(args));
        }
    },

    createTabs: function createTabs(nbTabs) {
        var tabs = "";
        var spaces = "    ";
        for (var i = 0; i < nbTabs; i++) {
            tabs += spaces;
        }
        return tabs;
    },

    setLevel: function setLevel(level) {
        this.logLevel = level;
    }
};

exports['default'] = Logger;
module.exports = exports['default'];

},{}],3:[function(require,module,exports){
'use strict';

Object.defineProperty(exports, '__esModule', {
    value: true
});

function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { 'default': obj }; }

var _EventDispatcherJs = require('./EventDispatcher.js');

var _EventDispatcherJs2 = _interopRequireDefault(_EventDispatcherJs);

exports['default'] = angular.module('angular-event-dispatcher', []).factory('EventDispatcher', function () {
    return new _EventDispatcherJs2['default']();
});
module.exports = exports['default'];

},{"./EventDispatcher.js":1}]},{},[3])
//# sourceMappingURL=data:application/json;charset:utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm5vZGVfbW9kdWxlcy9icm93c2VyaWZ5L25vZGVfbW9kdWxlcy9icm93c2VyLXBhY2svX3ByZWx1ZGUuanMiLCIvaG9tZS9tYW51L1dlYnN0b3JtUHJvamVjdHMvYW5ndWxhci1ldmVudC1kaXNwYXRjaGVyL3NyYy9FdmVudERpc3BhdGNoZXIuanMiLCIvaG9tZS9tYW51L1dlYnN0b3JtUHJvamVjdHMvYW5ndWxhci1ldmVudC1kaXNwYXRjaGVyL3NyYy9Mb2dnZXIuanMiLCIvaG9tZS9tYW51L1dlYnN0b3JtUHJvamVjdHMvYW5ndWxhci1ldmVudC1kaXNwYXRjaGVyL3NyYy9hbmd1bGFyLWV2ZW50LWRpc3BhdGNoZXIuanMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7Ozs7Ozs7Ozs7Ozs7Ozt3QkNBbUIsYUFBYTs7OztJQUUxQixlQUFlO0FBQ04sYUFEVCxlQUFlLEdBQ0g7OEJBRFosZUFBZTs7QUFFYixZQUFJLENBQUMsU0FBUyxHQUFHLElBQUksR0FBRyxFQUFFLENBQUM7QUFDM0IsWUFBSSxDQUFDLGFBQWEsR0FBRyxDQUFDLENBQUM7QUFDdkIsWUFBSSxDQUFDLE1BQU0sd0JBQVMsQ0FBQztLQUN4Qjs7aUJBTEMsZUFBZTs7ZUFPZixZQUFDLEtBQUssRUFBRSxRQUFRLEVBQUU7QUFDaEIsZ0JBQUksQ0FBQyxJQUFJLENBQUMsU0FBUyxDQUFDLEdBQUcsQ0FBQyxLQUFLLENBQUMsRUFBRTtBQUM1QixvQkFBSSxDQUFDLFNBQVMsQ0FBQyxHQUFHLENBQUMsS0FBSyxFQUFFLEVBQUUsQ0FBQyxDQUFDO2FBQ2pDO0FBQ0QsZ0JBQUksQ0FBQyxTQUFTLENBQUMsR0FBRyxDQUFDLEtBQUssQ0FBQyxDQUFDLElBQUksQ0FBQyxRQUFRLENBQUMsQ0FBQztTQUM1Qzs7O2VBRU0saUJBQUMsS0FBSyxFQUFFO0FBQ1gsZ0JBQUksSUFBSSxHQUFHLEVBQUUsQ0FBQztBQUNkLGlCQUFJLElBQUksQ0FBQyxHQUFDLENBQUMsRUFBRSxDQUFDLEdBQUMsU0FB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QUFDLEdBQUMsU0FBUyxDQUFDLE1BQU0sRUFBRSxDQUFDLEVBQUUsRUFBRTtBQUNsQyxvQkFBSSxDQUFDLElBQUksQ0FBQyxTQUFTLENBQUMsQ0FBQyxDQUFDLENBQUMsQ0FBQzthQUMzQjs7QUFFRCxtQkFBTyxDQUFDLEdBQUcsTUFBQSxDQUFYLE9BQU8sR0FBSyxPQUFPLFNBQUssSUFBSSxFQUFDLENBQUM7U0FDakM7S0FDSjs7QUFFRCxjQUFVLEVBQUMsb0JBQUMsTUFBTSxFQUFFO0FBQ2hCLFlBQUksSUFBSSxHQUFHLEVBQUUsQ0FBQztBQUNkLFlBQUksTUFBTSxHQUFHLE1BQU0sQ0FBQztBQUNwQixhQUFJLElBQUksQ0FBQyxHQUFDLENBQUMsRUFBRSxDQUFDLEdBQUMsTUFBTSxFQUFFLENBQUMsRUFBRSxFQUFFO0FBQ3hCLGdCQUFJLElBQUksTUFBTSxDQUFDO1NBQ2xCO0FBQ0QsZUFBTyxJQUFJLENBQUM7S0FDZjs7QUFFRCxZQUFRLEVBQUEsa0JBQUMsS0FBSyxFQUFDO0FBQ1gsWUFBSSxDQUFDLFFBQVEsR0FBRyxLQUFLLENBQUM7S0FDekI7Q0FDSixDQUFDOztxQkFFYSxNQUFNOzs7Ozs7Ozs7Ozs7aUNDNUJPLHNCQUFzQjs7OztxQkFFbkMsT0FBTyxDQUFDLE1BQU0sQ0FBQywwQkFBMEIsRUFBRSxFQUFFLENBQUMsQ0FDeEQsT0FBTyxDQUFDLGlCQUFpQixFQUFFO1dBQU0sb0NBQW1CO0NBQUEsQ0FBQyIsImZpbGUiOiJnZW5lcmF0ZWQuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlc0NvbnRlbnQiOlsiKGZ1bmN0aW9uIGUodCxuLHIpe2Z1bmN0aW9uIHMobyx1KXtpZighbltvXSl7aWYoIXRbb10pe3ZhciBhPXR5cGVvZiByZXF1aXJlPT1cImZ1bmN0aW9uXCImJnJlcXVpcmU7aWYoIXUmJmEpcmV0dXJuIGEobywhMCk7aWYoaSlyZXR1cm4gaShvLCEwKTt2YXIgZj1uZXcgRXJyb3IoXCJDYW5ub3QgZmluZCBtb2R1bGUgJ1wiK28rXCInXCIpO3Rocm93IGYuY29kZT1cIk1PRFVMRV9OT1RfRk9VTkRcIixmfXZhciBsPW5bb109e2V4cG9ydHM6e319O3Rbb11bMF0uY2FsbChsLmV4cG9ydHMsZnVuY3Rpb24oZSl7dmFyIG49dFtvXVsxXVtlXTtyZXR1cm4gcyhuP246ZSl9LGwsbC5leHBvcnRzLGUsdCxuLHIpfXJldHVybiBuW29dLmV4cG9ydHN9dmFyIGk9dHlwZW9mIHJlcXVpcmU9PVwiZnVuY3Rpb25cIiYmcmVxdWlyZTtmb3IodmFyIG89MDtvPHIubGVuZ3RoO28rKylzKHJbb10pO3JldHVybiBzfSkiLCJpbXBvcnQgTG9nZ2VyIGZyb20gJy4vTG9nZ2VyLmpzJztcblxuY2xhc3MgRXZlbnREaXNwYXRjaGVyIHtcbiAgICBjb25zdHJ1Y3RvcigpIHtcbiAgICAgICAgdGhpcy5saXN0ZW5lcnMgPSBuZXcgTWFwKCk7XG4gICAgICAgIHRoaXMuZXZlbnRzUnVubmluZyA9IDA7XG4gICAgICAgIHRoaXMubG9nZ2VyID0gTG9nZ2VyO1xuICAgIH1cblxuICAgIG9uKGV2ZW50LCBjYWxsYmFjaykge1xuICAgICAgICBpZiAoIXRoaXMubGlzdGVuZXJzLmhhcyhldmVudCkpIHtcbiAgICAgICAgICAgIHRoaXMubGlzdGVuZXJzLnNldChldmVudCwgW10pO1xuICAgICAgICB9XG4gICAgICAgIHRoaXMubGlzdGVuZXJzLmdldChldmVudCkucHVzaChjYWxsYmFjayk7XG4gICAgfVxuXG4gICAgdHJpZ2dlcihldmVudCkge1xuICAgICAgICBsZXQgYXJncyA9IFtdO1xuICAgICAgICBmb3IobGV0IGk9MTsgaTxhcmd1bWVudHMubGVuZ3RoOyBpKyspIHtcbiAgICAgICAgICAgIGFyZ3MucHVzaChhcmd1bWVudHNbaV0pO1xuICAgICAgICB9XG5cbiAgICAgICAgdGhpcy5sb2coJ2V2ZW50OiBcIicgKyBldmVudCArICdcIiBjYWxsZWQgd2l0aCBhcmdzOicsIGFyZ3MpO1xuXG4gICAgICAgIGxldCBuYkxpc3RlbmVycyA9IDA7XG4gICAgICAgIGlmKHRoaXMubGlzdGVuZXJzLmhhcyhldmVudCkpIHtcbiAgICAgICAgICAgIG5iTGlzdGVuZXJzID0gdGhpcy5saXN0ZW5lcnMuZ2V0KGV2ZW50KS5sZW5ndGg7XG4gICAgICAgICAgICB0aGlzLmV2ZW50c1J1bm5pbmcrKztcbiAgICAgICAgICAgIGZvciAobGV0IGxpc3RlbmVyIG9mIHRoaXMubGlzdGVuZXJzLmdldChldmVudCkpIHtcbiAgICAgICAgICAgICAgICBsaXN0ZW5lciguLi5hcmdzKTtcbiAgICAgICAgICAgIH1cbiAgICAgICAgICAgIHRoaXMuZXZlbnRzUnVubmluZy0tO1xuICAgICAgICB9XG5cbiAgICAgICAgdGhpcy5sb2coJ2V2ZW50OiBcIicgKyBldmVudCArICdcIiwgY2FsbGVkOiAnICsgbmJMaXN0ZW5lcnMgKyAnIHRpbWUocyknKTtcblxuICAgICAgICBpZih0aGlzLmV2ZW50c1J1bm5pbmcgPT09IDApIHtcbiAgICAgICAgICAgIHRoaXMubG9nKCk7XG4gICAgICAgIH1cbiAgICB9XG5cbiAgICBmb3J3YXJkIChldmVudCwgZXZlbnRUb0ZvcndhcmRUbykge1xuICAgICAgICBsZXQgc2VsZiA9IHRoaXM7XG5cbiAgICAgICAgLy8gQ2FuJ3QgdXNlIGFycm93IGZ1bmN0aW9uIGhlcmUsIGJlY2F1c2Ugd2UgbmVlZCB0aGUgYXJndW1lbnRzIG9mIHRoZSBjYWxsYmFjayBmdW5jdGlvblxuICAgICAgICB0aGlzLm9uKGV2ZW50LCBmdW5jdGlvbigpIHtcbiAgICAgICAgICAgIGxldCBhcmdzID0gW107XG4gICAgICAgICAgICBmb3IobGV0IGk9MDsgaTxhcmd1bWVudHMubGVuZ3RoOyBpKyspIHtcbiAgICAgICAgICAgICAgICBhcmdzLnB1c2goYXJndW1lbnRzW2ldKTtcbiAgICAgICAgICAgIH1cblxuICAgICAgICAgICAgc2VsZi50cmlnZ2VyKGV2ZW50VG9Gb3J3YXJkVG8sIC4uLmFyZ3MpXG4gICAgICAgIH0pO1xuICAgIH1cblxuICAgIGdldExpc3RlbmVycyhldmVudCkge1xuICAgICAgICByZXR1cm4gdGhpcy5saXN0ZW5lcnMuZ2V0KGV2ZW50KTtcbiAgICB9XG5cbiAgICBvZmYoZXZlbnQpIHtcbiAgICAgICAgdGhpcy5saXN0ZW5lcnMuc2V0KGV2ZW50LCBbXSk7XG4gICAgfVxuXG4gICAgcmVzZXQoKSB7XG4gICAgICAgIHRoaXMubGlzdGVuZXJzID0gbmV3IE1hcCgpO1xuICAgIH1cblxuICAgIGxvZyhtZXNzYWdlPScnLCBhcmdzPXVuZGVmaW5lZCkge1xuICAgICAgICB2YXIgc3BhY2VzQmVmb3JlID0gTG9nZ2VyLmNyZWF0ZVRhYnModGhpcy5ldmVudHNSdW5uaW5nKTtcbiAgICAgICAgaWYoYXJncykge1xuICAgICAgICAgICAgTG9nZ2VyLmluZm8oc3BhY2VzQmVmb3JlICsgbWVzc2FnZSwgLi4uYXJncyk7XG4gICAgICAgIH1cbiAgICAgICAgZWxzZSB7XG4gICAgICAgICAgICBMb2dnZXIuaW5mbyhzcGFjZXNCZWZvcmUgKyBtZXNzYWdlKTtcbiAgICAgICAgfVxuICAgIH1cbn1cblxuZXhwb3J0IGRlZmF1bHQgRXZlbnREaXNwYXRjaGVyOyIsImxldCBMb2dnZXIgPSB7XG4gICAgbG9nTGV2ZWw6ICdpbmZvJyxcblxuICAgIGluZm8gKCl7XG4gICAgICAgIGlmKHRoaXMubG9nTGV2ZWwgPT0gJ2luZm8nKSB7XG4gICAgICAgICAgICBsZXQgYXJncyA9IFtdO1xuICAgICAgICAgICAgZm9yKGxldCBpPTA7IGk8YXJndW1lbnRzLmxlbmd0aDsgaSsrKSB7XG4gICAgICAgICAgICAgICAgYXJncy5wdXNoKGFyZ3VtZW50c1tpXSk7XG4gICAgICAgICAgICB9XG5cbiAgICAgICAgICAgIGNvbnNvbGUubG9nKCdpbmZvOicsIC4uLmFyZ3MpO1xuICAgICAgICB9XG4gICAgfSxcblxuICAgIGNyZWF0ZVRhYnMgKG5iVGFicykge1xuICAgICAgICB2YXIgdGFicyA9IFwiXCI7XG4gICAgICAgIHZhciBzcGFjZXMgPSBcIiAgICBcIjtcbiAgICAgICAgZm9yKHZhciBpPTA7IGk8bmJUYWJzOyBpKyspIHtcbiAgICAgICAgICAgIHRhYnMgKz0gc3BhY2VzO1xuICAgICAgICB9XG4gICAgICAgIHJldHVybiB0YWJzO1xuICAgIH0sXG5cbiAgICBzZXRMZXZlbChsZXZlbCl7XG4gICAgICAgIHRoaXMubG9nTGV2ZWwgPSBsZXZlbDtcbiAgICB9XG59O1xuXG5leHBvcnQgZGVmYXVsdCBMb2dnZXI7IiwiaW1wb3J0IEV2ZW50RGlzcGF0Y2hlciBmcm9tICcuL0V2ZW50RGlzcGF0Y2hlci5qcyc7XG5cbmV4cG9ydCBkZWZhdWx0IGFuZ3VsYXIubW9kdWxlKCdhbmd1bGFyLWV2ZW50LWRpc3BhdGNoZXInLCBbXSlcbiAgICAuZmFjdG9yeSgnRXZlbnREaXNwYXRjaGVyJywgKCkgPT4gbmV3IEV2ZW50RGlzcGF0Y2hlcilcbjsiXX0=
