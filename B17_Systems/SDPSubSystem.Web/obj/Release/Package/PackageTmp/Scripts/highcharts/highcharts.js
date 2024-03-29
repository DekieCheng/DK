﻿/*
 Highcharts JS v10.1.0 (2022-05-20)

 (c) 2009-2021 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (Z, L) { "object" === typeof module && module.exports ? (L["default"] = L, module.exports = Z.document ? L(Z) : L) : "function" === typeof define && define.amd ? define("highcharts/highcharts", function () { return L(Z) }) : (Z.Highcharts && Z.Highcharts.error(16, !0), Z.Highcharts = L(Z)) })("undefined" !== typeof window ? window : this, function (Z) {
    function L(a, D, f, F) { a.hasOwnProperty(D) || (a[D] = F.apply(null, f), "function" === typeof CustomEvent && Z.dispatchEvent(new CustomEvent("HighchartsModuleLoaded", { detail: { path: D, module: a[D] } }))) }
    var f = {}; L(f, "Core/Globals.js", [], function () {
        var a; (function (a) {
            a.SVG_NS = "http://www.w3.org/2000/svg"; a.product = "Highcharts"; a.version = "10.1.0"; a.win = "undefined" !== typeof Z ? Z : {}; a.doc = a.win.document; a.svg = a.doc && a.doc.createElementNS && !!a.doc.createElementNS(a.SVG_NS, "svg").createSVGRect; a.userAgent = a.win.navigator && a.win.navigator.userAgent || ""; a.isChrome = -1 !== a.userAgent.indexOf("Chrome"); a.isFirefox = -1 !== a.userAgent.indexOf("Firefox"); a.isMS = /(edge|msie|trident)/i.test(a.userAgent) && !a.win.opera;
            a.isSafari = !a.isChrome && -1 !== a.userAgent.indexOf("Safari"); a.isTouchDevice = /(Mobile|Android|Windows Phone)/.test(a.userAgent); a.isWebKit = -1 !== a.userAgent.indexOf("AppleWebKit"); a.deg2rad = 2 * Math.PI / 360; a.hasBidiBug = a.isFirefox && 4 > parseInt(a.userAgent.split("Firefox/")[1], 10); a.hasTouch = !!a.win.TouchEvent; a.marginNames = ["plotTop", "marginRight", "marginBottom", "plotLeft"]; a.noop = function () { }; a.supportsPassiveEvents = function () {
                var f = !1; if (!a.isMS) {
                    var D = Object.defineProperty({}, "passive", {
                        get: function () {
                            f =
                            !0
                        }
                    }); a.win.addEventListener && a.win.removeEventListener && (a.win.addEventListener("testPassive", a.noop, D), a.win.removeEventListener("testPassive", a.noop, D))
                } return f
            }(); a.charts = []; a.dateFormats = {}; a.seriesTypes = {}; a.symbolSizes = {}; a.chartCount = 0
        })(a || (a = {})); ""; return a
    }); L(f, "Core/Utilities.js", [f["Core/Globals.js"]], function (a) {
        function f(d, q, h, l) {
            var z = q ? "Highcharts error" : "Highcharts warning"; 32 === d && (d = "" + z + ": Deprecated member"); var m = p(d), c = m ? "" + z + " #" + d + ": www.highcharts.com/errors/" + d + "/" :
                d.toString(); if ("undefined" !== typeof l) { var e = ""; m && (c += "?"); w(l, function (b, d) { e += "\n - ".concat(d, ": ").concat(b); m && (c += encodeURI(d) + "=" + encodeURI(b)) }); c += e } r(a, "displayError", { chart: h, code: d, message: c, params: l }, function () { if (q) throw Error(c); b.console && -1 === f.messages.indexOf(c) && console.warn(c) }); f.messages.push(c)
        } function B(b, d) {
            var z = {}; w(b, function (q, h) { if (J(b[h], !0) && !b.nodeType && d[h]) q = B(b[h], d[h]), Object.keys(q).length && (z[h] = q); else if (J(b[h]) || b[h] !== d[h] || h in b && !(h in d)) z[h] = b[h] });
            return z
        } function F(b, d) { return parseInt(b, d || 10) } function y(b) { return "string" === typeof b } function I(b) { b = Object.prototype.toString.call(b); return "[object Array]" === b || "[object Array Iterator]" === b } function J(b, d) { return !!b && "object" === typeof b && (!d || !I(b)) } function C(b) { return J(b) && "number" === typeof b.nodeType } function x(b) { var d = b && b.constructor; return !(!J(b, !0) || C(b) || !d || !d.name || "Object" === d.name) } function p(b) { return "number" === typeof b && !isNaN(b) && Infinity > b && -Infinity < b } function g(b) {
            return "undefined" !==
                typeof b && null !== b
        } function e(b, d, h) { var z = y(d) && !g(h), q, l = function (d, h) { g(d) ? b.setAttribute(h, d) : z ? (q = b.getAttribute(h)) || "class" !== h || (q = b.getAttribute(h + "Name")) : b.removeAttribute(h) }; y(d) ? l(h, d) : w(d, l); return q } function c(b, d) { var z; b || (b = {}); for (z in d) b[z] = d[z]; return b } function n() { for (var b = arguments, d = b.length, h = 0; h < d; h++) { var l = b[h]; if ("undefined" !== typeof l && null !== l) return l } } function k(b, d) {
            a.isMS && !a.svg && d && g(d.opacity) && (d.filter = "alpha(opacity=".concat(100 * d.opacity, ")")); c(b.style,
                d)
        } function t(b) { return Math.pow(10, Math.floor(Math.log(b) / Math.LN10)) } function v(b, d) { return 1E14 < b ? b : parseFloat(b.toPrecision(d || 14)) } function u(d, q, h) {
            var z = a.getStyle || u; if ("width" === q) return q = Math.min(d.offsetWidth, d.scrollWidth), h = d.getBoundingClientRect && d.getBoundingClientRect().width, h < q && h >= q - 1 && (q = Math.floor(h)), Math.max(0, q - (z(d, "padding-left", !0) || 0) - (z(d, "padding-right", !0) || 0)); if ("height" === q) return Math.max(0, Math.min(d.offsetHeight, d.scrollHeight) - (z(d, "padding-top", !0) || 0) - (z(d,
                "padding-bottom", !0) || 0)); b.getComputedStyle || f(27, !0); if (d = b.getComputedStyle(d, void 0)) { var l = d.getPropertyValue(q); n(h, "opacity" !== q) && (l = F(l)) } return l
        } function w(b, d, h) { for (var z in b) Object.hasOwnProperty.call(b, z) && d.call(h || b[z], b[z], z, b) } function A(b, d, h) {
            function z(d, H) { var z = b.removeEventListener || a.removeEventListenerPolyfill; z && z.call(b, d, H, !1) } function q(q) { var H; if (b.nodeName) { if (d) { var K = {}; K[d] = !0 } else K = q; w(K, function (b, d) { if (q[d]) for (H = q[d].length; H--;)z(d, q[d][H].fn) }) } } var l =
                "function" === typeof b && b.prototype || b; if (Object.hasOwnProperty.call(l, "hcEvents")) { var m = l.hcEvents; d ? (l = m[d] || [], h ? (m[d] = l.filter(function (b) { return h !== b.fn }), z(d, h)) : (q(m), m[d] = [])) : (q(m), delete l.hcEvents) }
        } function r(b, d, l, m) {
            l = l || {}; if (h.createEvent && (b.dispatchEvent || b.fireEvent && b !== a)) { var z = h.createEvent("Events"); z.initEvent(d, !0, !0); l = c(z, l); b.dispatchEvent ? b.dispatchEvent(l) : b.fireEvent(d, l) } else if (b.hcEvents) {
                l.target || c(l, {
                    preventDefault: function () { l.defaultPrevented = !0 }, target: b,
                    type: d
                }); z = []; for (var q = b, e = !1; q.hcEvents;)Object.hasOwnProperty.call(q, "hcEvents") && q.hcEvents[d] && (z.length && (e = !0), z.unshift.apply(z, q.hcEvents[d])), q = Object.getPrototypeOf(q); e && z.sort(function (b, d) { return b.order - d.order }); z.forEach(function (d) { !1 === d.fn.call(b, l) && l.preventDefault() })
            } m && !l.defaultPrevented && m.call(b, l)
        } var m = a.charts, h = a.doc, b = a.win; (f || (f = {})).messages = []; Math.easeInOutSine = function (b) { return -.5 * (Math.cos(Math.PI * b) - 1) }; var l = Array.prototype.find ? function (b, d) { return b.find(d) } :
            function (b, d) { var z, q = b.length; for (z = 0; z < q; z++)if (d(b[z], z)) return b[z] }; w({ map: "map", each: "forEach", grep: "filter", reduce: "reduce", some: "some" }, function (b, d) { a[d] = function (z) { var q; f(32, !1, void 0, (q = {}, q["Highcharts.".concat(d)] = "use Array.".concat(b), q)); return Array.prototype[b].apply(z, [].slice.call(arguments, 1)) } }); var d, G = function () { var b = Math.random().toString(36).substring(2, 9) + "-", q = 0; return function () { return "highcharts-" + (d ? "" : b) + q++ } }(); b.jQuery && (b.jQuery.fn.highcharts = function () {
                var b =
                    [].slice.call(arguments); if (this[0]) return b[0] ? (new (a[y(b[0]) ? b.shift() : "Chart"])(this[0], b[0], b[1]), this) : m[e(this[0], "data-highcharts-chart")]
            }); l = {
                addEvent: function (b, d, h, l) {
                    void 0 === l && (l = {}); var q = "function" === typeof b && b.prototype || b; Object.hasOwnProperty.call(q, "hcEvents") || (q.hcEvents = {}); q = q.hcEvents; a.Point && b instanceof a.Point && b.series && b.series.chart && (b.series.chart.runTrackerClick = !0); var z = b.addEventListener || a.addEventListenerPolyfill; z && z.call(b, d, h, a.supportsPassiveEvents ? {
                        passive: void 0 ===
                            l.passive ? -1 !== d.indexOf("touch") : l.passive, capture: !1
                    } : !1); q[d] || (q[d] = []); q[d].push({ fn: h, order: "number" === typeof l.order ? l.order : Infinity }); q[d].sort(function (b, d) { return b.order - d.order }); return function () { A(b, d, h) }
                }, arrayMax: function (b) { for (var d = b.length, z = b[0]; d--;)b[d] > z && (z = b[d]); return z }, arrayMin: function (b) { for (var d = b.length, z = b[0]; d--;)b[d] < z && (z = b[d]); return z }, attr: e, clamp: function (b, d, h) { return b > d ? b < h ? b : h : d }, cleanRecursively: B, clearTimeout: function (b) { g(b) && clearTimeout(b) }, correctFloat: v,
                createElement: function (b, d, l, m, e) { b = h.createElement(b); d && c(b, d); e && k(b, { padding: "0", border: "none", margin: "0" }); l && k(b, l); m && m.appendChild(b); return b }, css: k, defined: g, destroyObjectProperties: function (b, d) { w(b, function (q, z) { q && q !== d && q.destroy && q.destroy(); delete b[z] }) }, discardElement: function (b) { b && b.parentElement && b.parentElement.removeChild(b) }, erase: function (b, d) { for (var q = b.length; q--;)if (b[q] === d) { b.splice(q, 1); break } }, error: f, extend: c, extendClass: function (b, d) {
                    var q = function () { }; q.prototype =
                        new b; c(q.prototype, d); return q
                }, find: l, fireEvent: r, getMagnitude: t, getNestedProperty: function (d, q) { for (d = d.split("."); d.length && g(q);) { var h = d.shift(); if ("undefined" === typeof h || "__proto__" === h) return; q = q[h]; if (!g(q) || "function" === typeof q || "number" === typeof q.nodeType || q === b) return } return q }, getStyle: u, inArray: function (b, d, h) { f(32, !1, void 0, { "Highcharts.inArray": "use Array.indexOf" }); return d.indexOf(b, h) }, isArray: I, isClass: x, isDOMElement: C, isFunction: function (b) { return "function" === typeof b }, isNumber: p,
                isObject: J, isString: y, keys: function (b) { f(32, !1, void 0, { "Highcharts.keys": "use Object.keys" }); return Object.keys(b) }, merge: function () { var b, d = arguments, h = {}, l = function (b, d) { "object" !== typeof b && (b = {}); w(d, function (q, H) { "__proto__" !== H && "constructor" !== H && (!J(q, !0) || x(q) || C(q) ? b[H] = d[H] : b[H] = l(b[H] || {}, q)) }); return b }; !0 === d[0] && (h = d[1], d = Array.prototype.slice.call(d, 2)); var m = d.length; for (b = 0; b < m; b++)h = l(h, d[b]); return h }, normalizeTickInterval: function (b, d, h, l, m) {
                    var q = b; h = n(h, t(b)); var z = b / h; d || (d =
                        m ? [1, 1.2, 1.5, 2, 2.5, 3, 4, 5, 6, 8, 10] : [1, 2, 2.5, 5, 10], !1 === l && (1 === h ? d = d.filter(function (b) { return 0 === b % 1 }) : .1 >= h && (d = [1 / h]))); for (l = 0; l < d.length && !(q = d[l], m && q * h >= b || !m && z <= (d[l] + (d[l + 1] || d[l])) / 2); l++); return q = v(q * h, -Math.round(Math.log(.001) / Math.LN10))
                }, objectEach: w, offset: function (d) {
                    var q = h.documentElement; d = d.parentElement || d.parentNode ? d.getBoundingClientRect() : { top: 0, left: 0, width: 0, height: 0 }; return {
                        top: d.top + (b.pageYOffset || q.scrollTop) - (q.clientTop || 0), left: d.left + (b.pageXOffset || q.scrollLeft) -
                            (q.clientLeft || 0), width: d.width, height: d.height
                    }
                }, pad: function (b, d, h) { return Array((d || 2) + 1 - String(b).replace("-", "").length).join(h || "0") + b }, pick: n, pInt: F, relativeLength: function (b, d, h) { return /%$/.test(b) ? d * parseFloat(b) / 100 + (h || 0) : parseFloat(b) }, removeEvent: A, splat: function (b) { return I(b) ? b : [b] }, stableSort: function (b, d) { var h = b.length, q, l; for (l = 0; l < h; l++)b[l].safeI = l; b.sort(function (b, h) { q = d(b, h); return 0 === q ? b.safeI - h.safeI : q }); for (l = 0; l < h; l++)delete b[l].safeI }, syncTimeout: function (b, d, h) {
                    if (0 <
                        d) return setTimeout(b, d, h); b.call(0, h); return -1
                }, timeUnits: { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5, month: 24192E5, year: 314496E5 }, uniqueKey: G, useSerialIds: function (b) { return d = n(b, d) }, wrap: function (b, d, h) { var l = b[d]; b[d] = function () { var b = Array.prototype.slice.call(arguments), d = arguments, q = this; q.proceed = function () { l.apply(q, arguments.length ? arguments : d) }; b.unshift(l); b = h.apply(this, b); q.proceed = null; return b } }
            }; ""; return l
    }); L(f, "Core/Chart/ChartDefaults.js", [], function () {
        return {
            alignThresholds: !1,
            panning: { enabled: !1, type: "x" }, styledMode: !1, borderRadius: 0, colorCount: 10, allowMutatingData: !0, defaultSeriesType: "line", ignoreHiddenSeries: !0, spacing: [10, 10, 15, 10], resetZoomButton: { theme: { zIndex: 6 }, position: { align: "right", x: -10, y: 10 } }, zoomBySingleTouch: !1, width: null, height: null, borderColor: "#335cad", backgroundColor: "#ffffff", plotBorderColor: "#cccccc"
        }
    }); L(f, "Core/Color/Color.js", [f["Core/Globals.js"], f["Core/Utilities.js"]], function (a, f) {
        var D = f.isNumber, F = f.merge, y = f.pInt; f = function () {
            function f(D) {
                this.rgba =
                [NaN, NaN, NaN, NaN]; this.input = D; var C = a.Color; if (C && C !== f) return new C(D); if (!(this instanceof f)) return new f(D); this.init(D)
            } f.parse = function (a) { return a ? new f(a) : f.None }; f.prototype.init = function (a) {
                var C; if ("object" === typeof a && "undefined" !== typeof a.stops) this.stops = a.stops.map(function (e) { return new f(e[1]) }); else if ("string" === typeof a) {
                    this.input = a = f.names[a.toLowerCase()] || a; if ("#" === a.charAt(0)) {
                        var x = a.length; var p = parseInt(a.substr(1), 16); 7 === x ? C = [(p & 16711680) >> 16, (p & 65280) >> 8, p & 255, 1] :
                            4 === x && (C = [(p & 3840) >> 4 | (p & 3840) >> 8, (p & 240) >> 4 | p & 240, (p & 15) << 4 | p & 15, 1])
                    } if (!C) for (p = f.parsers.length; p-- && !C;) { var g = f.parsers[p]; (x = g.regex.exec(a)) && (C = g.parse(x)) }
                } C && (this.rgba = C)
            }; f.prototype.get = function (a) {
                var C = this.input, x = this.rgba; if ("object" === typeof C && "undefined" !== typeof this.stops) { var p = F(C); p.stops = [].slice.call(p.stops); this.stops.forEach(function (g, e) { p.stops[e] = [p.stops[e][0], g.get(a)] }); return p } return x && D(x[0]) ? "rgb" === a || !a && 1 === x[3] ? "rgb(" + x[0] + "," + x[1] + "," + x[2] + ")" : "a" ===
                    a ? "".concat(x[3]) : "rgba(" + x.join(",") + ")" : C
            }; f.prototype.brighten = function (a) { var C = this.rgba; if (this.stops) this.stops.forEach(function (p) { p.brighten(a) }); else if (D(a) && 0 !== a) for (var x = 0; 3 > x; x++)C[x] += y(255 * a), 0 > C[x] && (C[x] = 0), 255 < C[x] && (C[x] = 255); return this }; f.prototype.setOpacity = function (a) { this.rgba[3] = a; return this }; f.prototype.tweenTo = function (a, C) {
                var x = this.rgba, p = a.rgba; if (!D(x[0]) || !D(p[0])) return a.input || "none"; a = 1 !== p[3] || 1 !== x[3]; return (a ? "rgba(" : "rgb(") + Math.round(p[0] + (x[0] - p[0]) *
                    (1 - C)) + "," + Math.round(p[1] + (x[1] - p[1]) * (1 - C)) + "," + Math.round(p[2] + (x[2] - p[2]) * (1 - C)) + (a ? "," + (p[3] + (x[3] - p[3]) * (1 - C)) : "") + ")"
            }; f.names = { white: "#ffffff", black: "#000000" }; f.parsers = [{ regex: /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]?(?:\.[0-9]+)?)\s*\)/, parse: function (a) { return [y(a[1]), y(a[2]), y(a[3]), parseFloat(a[4], 10)] } }, { regex: /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/, parse: function (a) { return [y(a[1]), y(a[2]), y(a[3]), 1] } }]; f.None = new f(""); return f
        }();
        ""; return f
    }); L(f, "Core/Color/Palettes.js", [], function () { return { colors: "#7cb5ec #434348 #90ed7d #f7a35c #8085e9 #f15c80 #e4d354 #2b908f #f45b5b #91e8e1".split(" ") } }); L(f, "Core/Time.js", [f["Core/Globals.js"], f["Core/Utilities.js"]], function (a, f) {
        var D = a.win, F = f.defined, y = f.error, I = f.extend, J = f.isObject, C = f.merge, x = f.objectEach, p = f.pad, g = f.pick, e = f.splat, c = f.timeUnits, n = a.isSafari && D.Intl && D.Intl.DateTimeFormat.prototype.formatRange, k = a.isSafari && D.Intl && !D.Intl.DateTimeFormat.prototype.formatRange;
        f = function () {
            function t(c) { this.options = {}; this.variableTimezone = this.useUTC = !1; this.Date = D.Date; this.getTimezoneOffset = this.timezoneOffsetFunction(); this.update(c) } t.prototype.get = function (c, e) { if (this.variableTimezone || this.timezoneOffset) { var v = e.getTime(), n = v - this.getTimezoneOffset(e); e.setTime(n); c = e["getUTC" + c](); e.setTime(v); return c } return this.useUTC ? e["getUTC" + c]() : e["get" + c]() }; t.prototype.set = function (c, e, k) {
                if (this.variableTimezone || this.timezoneOffset) {
                    if ("Milliseconds" === c || "Seconds" ===
                        c || "Minutes" === c && 0 === this.getTimezoneOffset(e) % 36E5) return e["setUTC" + c](k); var v = this.getTimezoneOffset(e); v = e.getTime() - v; e.setTime(v); e["setUTC" + c](k); c = this.getTimezoneOffset(e); v = e.getTime() + c; return e.setTime(v)
                } return this.useUTC || n && "FullYear" === c ? e["setUTC" + c](k) : e["set" + c](k)
            }; t.prototype.update = function (c) {
                var e = g(c && c.useUTC, !0); this.options = c = C(!0, this.options || {}, c); this.Date = c.Date || D.Date || Date; this.timezoneOffset = (this.useUTC = e) && c.timezoneOffset || void 0; this.getTimezoneOffset = this.timezoneOffsetFunction();
                this.variableTimezone = e && !(!c.getTimezoneOffset && !c.timezone)
            }; t.prototype.makeTime = function (c, e, n, t, r, m) { if (this.useUTC) { var h = this.Date.UTC.apply(0, arguments); var b = this.getTimezoneOffset(h); h += b; var l = this.getTimezoneOffset(h); b !== l ? h += l - b : b - 36E5 !== this.getTimezoneOffset(h - 36E5) || k || (h -= 36E5) } else h = (new this.Date(c, e, g(n, 1), g(t, 0), g(r, 0), g(m, 0))).getTime(); return h }; t.prototype.timezoneOffsetFunction = function () {
                var c = this, e = this.options, n = e.getTimezoneOffset, k = e.moment || D.moment; if (!this.useUTC) return function (c) {
                    return 6E4 *
                        (new Date(c.toString())).getTimezoneOffset()
                }; if (e.timezone) { if (k) return function (c) { return 6E4 * -k.tz(c, e.timezone).utcOffset() }; y(25) } return this.useUTC && n ? function (c) { return 6E4 * n(c.valueOf()) } : function () { return 6E4 * (c.timezoneOffset || 0) }
            }; t.prototype.dateFormat = function (c, e, n) {
                if (!F(e) || isNaN(e)) return a.defaultOptions.lang && a.defaultOptions.lang.invalidDate || ""; c = g(c, "%Y-%m-%d %H:%M:%S"); var k = this, r = new this.Date(e), m = this.get("Hours", r), h = this.get("Day", r), b = this.get("Date", r), l = this.get("Month",
                    r), d = this.get("FullYear", r), G = a.defaultOptions.lang, z = G && G.weekdays, q = G && G.shortWeekdays; r = I({ a: q ? q[h] : z[h].substr(0, 3), A: z[h], d: p(b), e: p(b, 2, " "), w: h, b: G.shortMonths[l], B: G.months[l], m: p(l + 1), o: l + 1, y: d.toString().substr(2, 2), Y: d, H: p(m), k: m, I: p(m % 12 || 12), l: m % 12 || 12, M: p(this.get("Minutes", r)), p: 12 > m ? "AM" : "PM", P: 12 > m ? "am" : "pm", S: p(r.getSeconds()), L: p(Math.floor(e % 1E3), 3) }, a.dateFormats); x(r, function (b, d) { for (; -1 !== c.indexOf("%" + d);)c = c.replace("%" + d, "function" === typeof b ? b.call(k, e) : b) }); return n ? c.substr(0,
                        1).toUpperCase() + c.substr(1) : c
            }; t.prototype.resolveDTLFormat = function (c) { return J(c, !0) ? c : (c = e(c), { main: c[0], from: c[1], to: c[2] }) }; t.prototype.getTimeTicks = function (e, n, k, t) {
                var r = this, m = [], h = {}, b = new r.Date(n), l = e.unitRange, d = e.count || 1, G; t = g(t, 1); if (F(n)) {
                    r.set("Milliseconds", b, l >= c.second ? 0 : d * Math.floor(r.get("Milliseconds", b) / d)); l >= c.second && r.set("Seconds", b, l >= c.minute ? 0 : d * Math.floor(r.get("Seconds", b) / d)); l >= c.minute && r.set("Minutes", b, l >= c.hour ? 0 : d * Math.floor(r.get("Minutes", b) / d)); l >= c.hour &&
                        r.set("Hours", b, l >= c.day ? 0 : d * Math.floor(r.get("Hours", b) / d)); l >= c.day && r.set("Date", b, l >= c.month ? 1 : Math.max(1, d * Math.floor(r.get("Date", b) / d))); if (l >= c.month) { r.set("Month", b, l >= c.year ? 0 : d * Math.floor(r.get("Month", b) / d)); var z = r.get("FullYear", b) } l >= c.year && r.set("FullYear", b, z - z % d); l === c.week && (z = r.get("Day", b), r.set("Date", b, r.get("Date", b) - z + t + (z < t ? -7 : 0))); z = r.get("FullYear", b); t = r.get("Month", b); var q = r.get("Date", b), v = r.get("Hours", b); n = b.getTime(); !r.variableTimezone && r.useUTC || !F(k) || (G = k -
                            n > 4 * c.month || r.getTimezoneOffset(n) !== r.getTimezoneOffset(k)); n = b.getTime(); for (b = 1; n < k;)m.push(n), n = l === c.year ? r.makeTime(z + b * d, 0) : l === c.month ? r.makeTime(z, t + b * d) : !G || l !== c.day && l !== c.week ? G && l === c.hour && 1 < d ? r.makeTime(z, t, q, v + b * d) : n + l * d : r.makeTime(z, t, q + b * d * (l === c.day ? 1 : 7)), b++; m.push(n); l <= c.hour && 1E4 > m.length && m.forEach(function (b) { 0 === b % 18E5 && "000000000" === r.dateFormat("%H%M%S%L", b) && (h[b] = "day") })
                } m.info = I(e, { higherRanks: h, totalRange: l * d }); return m
            }; t.prototype.getDateFormat = function (e, n, k,
                t) { var r = this.dateFormat("%m-%d %H:%M:%S.%L", n), m = { millisecond: 15, second: 12, minute: 9, hour: 6, day: 3 }, h = "millisecond"; for (b in c) { if (e === c.week && +this.dateFormat("%w", n) === k && "00:00:00.000" === r.substr(6)) { var b = "week"; break } if (c[b] > e) { b = h; break } if (m[b] && r.substr(m[b]) !== "01-01 00:00:00.000".substr(m[b])) break; "week" !== b && (h = b) } if (b) var l = this.resolveDTLFormat(t[b]).main; return l }; return t
        }(); ""; return f
    }); L(f, "Core/DefaultOptions.js", [f["Core/Chart/ChartDefaults.js"], f["Core/Color/Color.js"], f["Core/Globals.js"],
    f["Core/Color/Palettes.js"], f["Core/Time.js"], f["Core/Utilities.js"]], function (a, f, B, F, y, I) {
        f = f.parse; var D = I.merge, C = {
            colors: F.colors, symbols: ["circle", "diamond", "square", "triangle", "triangle-down"], lang: {
                loading: "Loading...", months: "January February March April May June July August September October November December".split(" "), shortMonths: "Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec".split(" "), weekdays: "Sunday Monday Tuesday Wednesday Thursday Friday Saturday".split(" "), decimalPoint: ".",
                numericSymbols: "kMGTPE".split(""), resetZoom: "Reset zoom", resetZoomTitle: "Reset zoom level 1:1", thousandsSep: " "
            }, global: {}, time: { Date: void 0, getTimezoneOffset: void 0, timezone: void 0, timezoneOffset: 0, useUTC: !0 }, chart: a, title: { text: "Chart title", align: "center", margin: 15, widthAdjust: -44 }, subtitle: { text: "", align: "center", widthAdjust: -44 }, caption: { margin: 15, text: "", align: "left", verticalAlign: "bottom" }, plotOptions: {}, labels: { style: { position: "absolute", color: "#333333" } }, legend: {
                enabled: !0, align: "center",
                alignColumns: !0, className: "highcharts-no-tooltip", layout: "horizontal", labelFormatter: function () { return this.name }, borderColor: "#999999", borderRadius: 0, navigation: { activeColor: "#003399", inactiveColor: "#cccccc" }, itemStyle: { color: "#333333", cursor: "pointer", fontSize: "12px", fontWeight: "bold", textOverflow: "ellipsis" }, itemHoverStyle: { color: "#000000" }, itemHiddenStyle: { color: "#cccccc" }, shadow: !1, itemCheckboxStyle: { position: "absolute", width: "13px", height: "13px" }, squareSymbol: !0, symbolPadding: 5, verticalAlign: "bottom",
                x: 0, y: 0, title: { style: { fontWeight: "bold" } }
            }, loading: { labelStyle: { fontWeight: "bold", position: "relative", top: "45%" }, style: { position: "absolute", backgroundColor: "#ffffff", opacity: .5, textAlign: "center" } }, tooltip: {
                enabled: !0, animation: B.svg, borderRadius: 3, dateTimeLabelFormats: { millisecond: "%A, %b %e, %H:%M:%S.%L", second: "%A, %b %e, %H:%M:%S", minute: "%A, %b %e, %H:%M", hour: "%A, %b %e, %H:%M", day: "%A, %b %e, %Y", week: "Week from %A, %b %e, %Y", month: "%B %Y", year: "%Y" }, footerFormat: "", headerShape: "callout",
                hideDelay: 500, padding: 8, shape: "callout", shared: !1, snap: B.isTouchDevice ? 25 : 10, headerFormat: '<span style="font-size: 10px">{point.key}</span><br/>', pointFormat: '<span style="color:{point.color}">\u25cf</span> {series.name}: <b>{point.y}</b><br/>', backgroundColor: f("#f7f7f7").setOpacity(.85).get(), borderWidth: 1, shadow: !0, stickOnContact: !1, style: { color: "#333333", cursor: "default", fontSize: "12px", whiteSpace: "nowrap" }, useHTML: !1
            }, credits: {
                enabled: !0, href: "https://www.highcharts.com?credits", position: {
                    align: "right",
                    x: -10, verticalAlign: "bottom", y: -5
                }, style: { cursor: "pointer", color: "#999999", fontSize: "9px" }, text: "Highcharts.com"
            }
        }; C.chart.styledMode = !1; ""; var x = new y(D(C.global, C.time)); a = { defaultOptions: C, defaultTime: x, getOptions: function () { return C }, setOptions: function (p) { D(!0, C, p); if (p.time || p.global) B.time ? B.time.update(D(C.global, C.time, p.global, p.time)) : B.time = x; return C } }; ""; return a
    }); L(f, "Core/Animation/Fx.js", [f["Core/Color/Color.js"], f["Core/Globals.js"], f["Core/Utilities.js"]], function (a, f, B) {
        var D =
            a.parse, y = f.win, I = B.isNumber, J = B.objectEach; return function () {
                function a(a, p, g) { this.pos = NaN; this.options = p; this.elem = a; this.prop = g } a.prototype.dSetter = function () { var a = this.paths, p = a && a[0]; a = a && a[1]; var g = this.now || 0, e = []; if (1 !== g && p && a) if (p.length === a.length && 1 > g) for (var c = 0; c < a.length; c++) { for (var n = p[c], k = a[c], t = [], v = 0; v < k.length; v++) { var u = n[v], w = k[v]; I(u) && I(w) && ("A" !== k[0] || 4 !== v && 5 !== v) ? t[v] = u + g * (w - u) : t[v] = w } e.push(t) } else e = a; else e = this.toD || []; this.elem.attr("d", e, void 0, !0) }; a.prototype.update =
                    function () { var a = this.elem, p = this.prop, g = this.now, e = this.options.step; if (this[p + "Setter"]) this[p + "Setter"](); else a.attr ? a.element && a.attr(p, g, null, !0) : a.style[p] = g + this.unit; e && e.call(a, g, this) }; a.prototype.run = function (x, p, g) {
                        var e = this, c = e.options, n = function (c) { return n.stopped ? !1 : e.step(c) }, k = y.requestAnimationFrame || function (c) { setTimeout(c, 13) }, t = function () { for (var c = 0; c < a.timers.length; c++)a.timers[c]() || a.timers.splice(c--, 1); a.timers.length && k(t) }; x !== p || this.elem["forceAnimate:" + this.prop] ?
                            (this.startTime = +new Date, this.start = x, this.end = p, this.unit = g, this.now = this.start, this.pos = 0, n.elem = this.elem, n.prop = this.prop, n() && 1 === a.timers.push(n) && k(t)) : (delete c.curAnim[this.prop], c.complete && 0 === Object.keys(c.curAnim).length && c.complete.call(this.elem))
                    }; a.prototype.step = function (a) {
                        var p = +new Date, g = this.options, e = this.elem, c = g.complete, n = g.duration, k = g.curAnim; if (e.attr && !e.element) a = !1; else if (a || p >= n + this.startTime) {
                            this.now = this.end; this.pos = 1; this.update(); var t = k[this.prop] = !0; J(k,
                                function (c) { !0 !== c && (t = !1) }); t && c && c.call(e); a = !1
                        } else this.pos = g.easing((p - this.startTime) / n), this.now = this.start + (this.end - this.start) * this.pos, this.update(), a = !0; return a
                    }; a.prototype.initPath = function (a, p, g) {
                        function e(c, m) { for (; c.length < A;) { var h = c[0], b = m[A - c.length]; b && "M" === h[0] && (c[0] = "C" === b[0] ? ["C", h[1], h[2], h[1], h[2], h[1], h[2]] : ["L", h[1], h[2]]); c.unshift(h); t && (h = c.pop(), c.push(c[c.length - 1], h)) } } function c(c, m) {
                            for (; c.length < A;)if (m = c[Math.floor(c.length / v) - 1].slice(), "C" === m[0] && (m[1] =
                                m[5], m[2] = m[6]), t) { var h = c[Math.floor(c.length / v)].slice(); c.splice(c.length / 2, 0, m, h) } else c.push(m)
                        } var n = a.startX, k = a.endX; g = g.slice(); var t = a.isArea, v = t ? 2 : 1; p = p && p.slice(); if (!p) return [g, g]; if (n && k && k.length) { for (a = 0; a < n.length; a++)if (n[a] === k[0]) { var u = a; break } else if (n[0] === k[k.length - n.length + a]) { u = a; var w = !0; break } else if (n[n.length - 1] === k[k.length - n.length + a]) { u = n.length - a; break } "undefined" === typeof u && (p = []) } if (p.length && I(u)) { var A = g.length + u * v; w ? (e(p, g), c(g, p)) : (e(g, p), c(p, g)) } return [p,
                            g]
                    }; a.prototype.fillSetter = function () { a.prototype.strokeSetter.apply(this, arguments) }; a.prototype.strokeSetter = function () { this.elem.attr(this.prop, D(this.start).tweenTo(D(this.end), this.pos), void 0, !0) }; a.timers = []; return a
            }()
    }); L(f, "Core/Animation/AnimationUtilities.js", [f["Core/Animation/Fx.js"], f["Core/Utilities.js"]], function (a, f) {
        function D(c) { return x(c) ? p({ duration: 500, defer: 0 }, c) : { duration: c ? 500 : 0, defer: 0 } } function F(c, e) {
            for (var n = a.timers.length; n--;)a.timers[n].elem !== c || e && e !== a.timers[n].prop ||
                (a.timers[n].stopped = !0)
        } var y = f.defined, I = f.getStyle, J = f.isArray, C = f.isNumber, x = f.isObject, p = f.merge, g = f.objectEach, e = f.pick; return {
            animate: function (c, e, k) {
                var n, v = "", u, w; if (!x(k)) { var A = arguments; k = { duration: A[2], easing: A[3], complete: A[4] } } C(k.duration) || (k.duration = 400); k.easing = "function" === typeof k.easing ? k.easing : Math[k.easing] || Math.easeInOutSine; k.curAnim = p(e); g(e, function (t, m) {
                    F(c, m); w = new a(c, k, m); u = void 0; "d" === m && J(e.d) ? (w.paths = w.initPath(c, c.pathArray, e.d), w.toD = e.d, n = 0, u = 1) : c.attr ?
                        n = c.attr(m) : (n = parseFloat(I(c, m)) || 0, "opacity" !== m && (v = "px")); u || (u = t); "string" === typeof u && u.match("px") && (u = u.replace(/px/g, "")); w.run(n, u, v)
                })
            }, animObject: D, getDeferredAnimation: function (c, e, k) { var n = D(e), g = 0, a = 0; (k ? [k] : c.series).forEach(function (c) { c = D(c.options.animation); g = e && y(e.defer) ? n.defer : Math.max(g, c.duration + c.defer); a = Math.min(n.duration, c.duration) }); c.renderer.forExport && (g = 0); return { defer: Math.max(0, g - a), duration: Math.min(g, a) } }, setAnimation: function (c, n) {
                n.renderer.globalAnimation =
                e(c, n.options.chart.animation, !0)
            }, stop: F
        }
    }); L(f, "Core/Renderer/HTML/AST.js", [f["Core/Globals.js"], f["Core/Utilities.js"]], function (a, f) {
        var D = a.SVG_NS, F = f.attr, y = f.createElement, I = f.css, J = f.error, C = f.isFunction, x = f.isString, p = f.objectEach, g = f.splat, e = (f = a.win.trustedTypes) && C(f.createPolicy) && f.createPolicy("highcharts", { createHTML: function (c) { return c } }), c = e ? e.createHTML("") : ""; try { var n = !!(new DOMParser).parseFromString(c, "text/html") } catch (k) { n = !1 } C = function () {
            function k(c) {
                this.nodes = "string" ===
                    typeof c ? this.parseMarkup(c) : c
            } k.filterUserAttributes = function (c) { p(c, function (e, n) { var g = !0; -1 === k.allowedAttributes.indexOf(n) && (g = !1); -1 !== ["background", "dynsrc", "href", "lowsrc", "src"].indexOf(n) && (g = x(e) && k.allowedReferences.some(function (c) { return 0 === e.indexOf(c) })); g || (J(33, !1, void 0, { "Invalid attribute in config": "".concat(n) }), delete c[n]) }); return c }; k.parseStyle = function (c) {
                return c.split(";").reduce(function (c, e) {
                    e = e.split(":").map(function (c) { return c.trim() }); var n = e.shift(); n && e.length &&
                        (c[n.replace(/-([a-z])/g, function (c) { return c[1].toUpperCase() })] = e.join(":")); return c
                }, {})
            }; k.setElementHTML = function (c, e) { c.innerHTML = k.emptyHTML; e && (new k(e)).addToDOM(c) }; k.prototype.addToDOM = function (c) {
                function e(c, n) {
                    var t; g(c).forEach(function (c) {
                        var m = c.tagName, h = c.textContent ? a.doc.createTextNode(c.textContent) : void 0, b = k.bypassHTMLFiltering; if (m) if ("#text" === m) var l = h; else if (-1 !== k.allowedTags.indexOf(m) || b) {
                            m = a.doc.createElementNS("svg" === m ? D : n.namespaceURI || D, m); var d = c.attributes ||
                                {}; p(c, function (b, c) { "tagName" !== c && "attributes" !== c && "children" !== c && "style" !== c && "textContent" !== c && (d[c] = b) }); F(m, b ? d : k.filterUserAttributes(d)); c.style && I(m, c.style); h && m.appendChild(h); e(c.children || [], m); l = m
                        } else J(33, !1, void 0, { "Invalid tagName in config": m }); l && n.appendChild(l); t = l
                    }); return t
                } return e(this.nodes, c)
            }; k.prototype.parseMarkup = function (c) {
                var g = []; c = c.trim().replace(/ style="/g, ' data-style="'); if (n) c = (new DOMParser).parseFromString(e ? e.createHTML(c) : c, "text/html"); else {
                    var t =
                        y("div"); t.innerHTML = c; c = { body: t }
                } var a = function (c, e) { var m = c.nodeName.toLowerCase(), h = { tagName: m }; "#text" === m && (h.textContent = c.textContent || ""); if (m = c.attributes) { var b = {};[].forEach.call(m, function (d) { "data-style" === d.name ? h.style = k.parseStyle(d.value) : b[d.name] = d.value }); h.attributes = b } if (c.childNodes.length) { var l = [];[].forEach.call(c.childNodes, function (b) { a(b, l) }); l.length && (h.children = l) } e.push(h) };[].forEach.call(c.body.childNodes, function (c) { return a(c, g) }); return g
            }; k.allowedAttributes =
                "aria-controls aria-describedby aria-expanded aria-haspopup aria-hidden aria-label aria-labelledby aria-live aria-pressed aria-readonly aria-roledescription aria-selected class clip-path color colspan cx cy d dx dy disabled fill height href id in markerHeight markerWidth offset opacity orient padding paddingLeft paddingRight patternUnits r refX refY role scope slope src startOffset stdDeviation stroke stroke-linecap stroke-width style tableValues result rowspan summary target tabindex text-align textAnchor textLength title type valign width x x1 x2 y y1 y2 zIndex".split(" ");
            k.allowedReferences = "https:// http:// mailto: / ../ ./ #".split(" "); k.allowedTags = "a abbr b br button caption circle clipPath code dd defs div dl dt em feComponentTransfer feFuncA feFuncB feFuncG feFuncR feGaussianBlur feOffset feMerge feMergeNode filter h1 h2 h3 h4 h5 h6 hr i img li linearGradient marker ol p path pattern pre rect small span stop strong style sub sup svg table text thead tbody tspan td th tr u ul #text".split(" "); k.emptyHTML = c; k.bypassHTMLFiltering = !1; return k
        }(); ""; return C
    });
    L(f, "Core/FormatUtilities.js", [f["Core/DefaultOptions.js"], f["Core/Utilities.js"]], function (a, f) {
        function D(a, g, e, c) {
            a = +a || 0; g = +g; var n = F.lang, k = (a.toString().split(".")[1] || "").split("e")[0].length, t = a.toString().split("e"), v = g; if (-1 === g) g = Math.min(k, 20); else if (!J(g)) g = 2; else if (g && t[1] && 0 > t[1]) { var u = g + +t[1]; 0 <= u ? (t[0] = (+t[0]).toExponential(u).split("e")[0], g = u) : (t[0] = t[0].split(".")[0] || 0, a = 20 > g ? (t[0] * Math.pow(10, t[1])).toFixed(g) : 0, t[1] = 0) } u = (Math.abs(t[1] ? t[0] : a) + Math.pow(10, -Math.max(g, k) -
                1)).toFixed(g); k = String(x(u)); var w = 3 < k.length ? k.length % 3 : 0; e = C(e, n.decimalPoint); c = C(c, n.thousandsSep); a = (0 > a ? "-" : "") + (w ? k.substr(0, w) + c : ""); a = 0 > +t[1] && !v ? "0" : a + k.substr(w).replace(/(\d{3})(?=\d)/g, "$1" + c); g && (a += e + u.slice(-g)); t[1] && 0 !== +a && (a += "e" + t[1]); return a
        } var F = a.defaultOptions, y = a.defaultTime, I = f.getNestedProperty, J = f.isNumber, C = f.pick, x = f.pInt; return {
            dateFormat: function (a, g, e) { return y.dateFormat(a, g, e) }, format: function (a, g, e) {
                var c = "{", n = !1, k = /f$/, t = /\.([0-9])/, v = F.lang, u = e && e.time ||
                    y; e = e && e.numberFormatter || D; for (var w = []; a;) { var A = a.indexOf(c); if (-1 === A) break; var r = a.slice(0, A); if (n) { r = r.split(":"); c = I(r.shift() || "", g); if (r.length && "number" === typeof c) if (r = r.join(":"), k.test(r)) { var m = parseInt((r.match(t) || ["", "-1"])[1], 10); null !== c && (c = e(c, m, v.decimalPoint, -1 < r.indexOf(",") ? v.thousandsSep : "")) } else c = u.dateFormat(r, c); w.push(c) } else w.push(r); a = a.slice(A + 1); c = (n = !n) ? "}" : "{" } w.push(a); return w.join("")
            }, numberFormat: D
        }
    }); L(f, "Core/Renderer/RendererUtilities.js", [f["Core/Utilities.js"]],
        function (a) {
            var f = a.clamp, B = a.pick, F = a.stableSort, y; (function (a) {
                function D(a, x, p) {
                    var g = a, e = g.reducedLen || x, c = function (c, e) { return (e.rank || 0) - (c.rank || 0) }, n = function (c, e) { return c.target - e.target }, k, t = !0, v = [], u = 0; for (k = a.length; k--;)u += a[k].size; if (u > e) { F(a, c); for (u = k = 0; u <= e;)u += a[k].size, k++; v = a.splice(k - 1, a.length) } F(a, n); for (a = a.map(function (c) { return { size: c.size, targets: [c.target], align: B(c.align, .5) } }); t;) {
                        for (k = a.length; k--;)e = a[k], c = (Math.min.apply(0, e.targets) + Math.max.apply(0, e.targets)) /
                            2, e.pos = f(c - e.size * e.align, 0, x - e.size); k = a.length; for (t = !1; k--;)0 < k && a[k - 1].pos + a[k - 1].size > a[k].pos && (a[k - 1].size += a[k].size, a[k - 1].targets = a[k - 1].targets.concat(a[k].targets), a[k - 1].align = .5, a[k - 1].pos + a[k - 1].size > x && (a[k - 1].pos = x - a[k - 1].size), a.splice(k, 1), t = !0)
                    } g.push.apply(g, v); k = 0; a.some(function (c) {
                        var e = 0; return (c.targets || []).some(function () {
                            g[k].pos = c.pos + e; if ("undefined" !== typeof p && Math.abs(g[k].pos - g[k].target) > p) return g.slice(0, k + 1).forEach(function (c) { return delete c.pos }), g.reducedLen =
                                (g.reducedLen || x) - .1 * x, g.reducedLen > .1 * x && D(g, x, p), !0; e += g[k].size; k++; return !1
                        })
                    }); F(g, n); return g
                } a.distribute = D
            })(y || (y = {})); return y
        }); L(f, "Core/Renderer/SVG/SVGElement.js", [f["Core/Animation/AnimationUtilities.js"], f["Core/Renderer/HTML/AST.js"], f["Core/Color/Color.js"], f["Core/Globals.js"], f["Core/Utilities.js"]], function (a, f, B, F, y) {
            var D = a.animate, J = a.animObject, C = a.stop, x = F.deg2rad, p = F.doc, g = F.noop, e = F.svg, c = F.SVG_NS, n = F.win, k = y.addEvent, t = y.attr, v = y.createElement, u = y.css, w = y.defined, A = y.erase,
            r = y.extend, m = y.fireEvent, h = y.isArray, b = y.isFunction, l = y.isNumber, d = y.isString, G = y.merge, z = y.objectEach, q = y.pick, O = y.pInt, P = y.syncTimeout, S = y.uniqueKey; a = function () {
                function a() { this.element = void 0; this.onEvents = {}; this.opacity = 1; this.renderer = void 0; this.SVG_NS = c; this.symbolCustomAttribs = "x y width height r start end innerR anchorX anchorY rounded".split(" ") } a.prototype._defaultGetter = function (b) {
                    b = q(this[b + "Value"], this[b], this.element ? this.element.getAttribute(b) : null, 0); /^[\-0-9\.]+$/.test(b) &&
                        (b = parseFloat(b)); return b
                }; a.prototype._defaultSetter = function (b, d, c) { c.setAttribute(d, b) }; a.prototype.add = function (b) { var d = this.renderer, c = this.element; b && (this.parentGroup = b); this.parentInverted = b && b.inverted; "undefined" !== typeof this.textStr && "text" === this.element.nodeName && d.buildText(this); this.added = !0; if (!b || b.handleZ || this.zIndex) var h = this.zIndexSetter(); h || (b ? b.element : d.box).appendChild(c); if (this.onAdd) this.onAdd(); return this }; a.prototype.addClass = function (b, d) {
                    var c = d ? "" : this.attr("class") ||
                        ""; b = (b || "").split(/ /g).reduce(function (b, d) { -1 === c.indexOf(d) && b.push(d); return b }, c ? [c] : []).join(" "); b !== c && this.attr("class", b); return this
                }; a.prototype.afterSetters = function () { this.doTransform && (this.updateTransform(), this.doTransform = !1) }; a.prototype.align = function (b, c, H) {
                    var h = {}, l = this.renderer, e = l.alignedObjects, m, a, E; if (b) { if (this.alignOptions = b, this.alignByTranslate = c, !H || d(H)) this.alignTo = m = H || "renderer", A(e, this), e.push(this), H = void 0 } else b = this.alignOptions, c = this.alignByTranslate,
                        m = this.alignTo; H = q(H, l[m], "scrollablePlotBox" === m ? l.plotBox : void 0, l); m = b.align; var z = b.verticalAlign; l = (H.x || 0) + (b.x || 0); e = (H.y || 0) + (b.y || 0); "right" === m ? a = 1 : "center" === m && (a = 2); a && (l += (H.width - (b.width || 0)) / a); h[c ? "translateX" : "x"] = Math.round(l); "bottom" === z ? E = 1 : "middle" === z && (E = 2); E && (e += (H.height - (b.height || 0)) / E); h[c ? "translateY" : "y"] = Math.round(e); this[this.placed ? "animate" : "attr"](h); this.placed = !0; this.alignAttr = h; return this
                }; a.prototype.alignSetter = function (b) {
                    var d = {
                        left: "start", center: "middle",
                        right: "end"
                    }; d[b] && (this.alignValue = b, this.element.setAttribute("text-anchor", d[b]))
                }; a.prototype.animate = function (b, d, c) { var H = this, h = J(q(d, this.renderer.globalAnimation, !0)); d = h.defer; q(p.hidden, p.msHidden, p.webkitHidden, !1) && (h.duration = 0); 0 !== h.duration ? (c && (h.complete = c), P(function () { H.element && D(H, b, h) }, d)) : (this.attr(b, void 0, c || h.complete), z(b, function (b, d) { h.step && h.step.call(this, b, { prop: d, pos: 1, elem: this }) }, this)); return this }; a.prototype.applyTextOutline = function (b) {
                    var d = this.element;
                    -1 !== b.indexOf("contrast") && (b = b.replace(/contrast/g, this.renderer.getContrast(d.style.fill))); var H = b.split(" "); b = H[H.length - 1]; if ((H = H[0]) && "none" !== H && F.svg) {
                        this.fakeTS = !0; this.ySetter = this.xSetter; H = H.replace(/(^[\d\.]+)(.*?)$/g, function (b, d, c) { return 2 * Number(d) + c }); this.removeTextOutline(); var h = p.createElementNS(c, "tspan"); t(h, { "class": "highcharts-text-outline", fill: b, stroke: b, "stroke-width": H, "stroke-linejoin": "round" });[].forEach.call(d.childNodes, function (b) {
                            var d = b.cloneNode(!0); d.removeAttribute &&
                                ["fill", "stroke", "stroke-width", "stroke"].forEach(function (b) { return d.removeAttribute(b) }); h.appendChild(d)
                        }); var l = p.createElementNS(c, "tspan"); l.textContent = "\u200b";["x", "y"].forEach(function (b) { var c = d.getAttribute(b); c && l.setAttribute(b, c) }); h.appendChild(l); d.insertBefore(h, d.firstChild)
                    }
                }; a.prototype.attr = function (b, d, c, h) {
                    var H = this.element, l = this.symbolCustomAttribs, q, e = this, E, K; if ("string" === typeof b && "undefined" !== typeof d) { var m = b; b = {}; b[m] = d } "string" === typeof b ? e = (this[b + "Getter"] ||
                        this._defaultGetter).call(this, b, H) : (z(b, function (d, c) { E = !1; h || C(this, c); this.symbolName && -1 !== l.indexOf(c) && (q || (this.symbolAttr(b), q = !0), E = !0); !this.rotation || "x" !== c && "y" !== c || (this.doTransform = !0); E || (K = this[c + "Setter"] || this._defaultSetter, K.call(this, d, c, H), !this.styledMode && this.shadows && /^(width|height|visibility|x|y|d|transform|cx|cy|r)$/.test(c) && this.updateShadows(c, d, K)) }, this), this.afterSetters()); c && c.call(this); return e
                }; a.prototype.clip = function (b) {
                    return this.attr("clip-path", b ? "url(" +
                        this.renderer.url + "#" + b.id + ")" : "none")
                }; a.prototype.crisp = function (b, d) { d = d || b.strokeWidth || 0; var c = Math.round(d) % 2 / 2; b.x = Math.floor(b.x || this.x || 0) + c; b.y = Math.floor(b.y || this.y || 0) + c; b.width = Math.floor((b.width || this.width || 0) - 2 * c); b.height = Math.floor((b.height || this.height || 0) - 2 * c); w(b.strokeWidth) && (b.strokeWidth = d); return b }; a.prototype.complexColor = function (b, d, c) {
                    var H = this.renderer, l, q, e, a, E, n, k, g, t, r, v = [], A; m(this.renderer, "complexColor", { args: arguments }, function () {
                        b.radialGradient ? q = "radialGradient" :
                        b.linearGradient && (q = "linearGradient"); if (q) {
                            e = b[q]; E = H.gradients; n = b.stops; t = c.radialReference; h(e) && (b[q] = e = { x1: e[0], y1: e[1], x2: e[2], y2: e[3], gradientUnits: "userSpaceOnUse" }); "radialGradient" === q && t && !w(e.gradientUnits) && (a = e, e = G(e, H.getRadialAttr(t, a), { gradientUnits: "userSpaceOnUse" })); z(e, function (b, d) { "id" !== d && v.push(d, b) }); z(n, function (b) { v.push(b) }); v = v.join(","); if (E[v]) r = E[v].attr("id"); else {
                                e.id = r = S(); var K = E[v] = H.createElement(q).attr(e).add(H.defs); K.radAttr = a; K.stops = []; n.forEach(function (b) {
                                    0 ===
                                    b[1].indexOf("rgba") ? (l = B.parse(b[1]), k = l.get("rgb"), g = l.get("a")) : (k = b[1], g = 1); b = H.createElement("stop").attr({ offset: b[0], "stop-color": k, "stop-opacity": g }).add(K); K.stops.push(b)
                                })
                            } A = "url(" + H.url + "#" + r + ")"; c.setAttribute(d, A); c.gradient = v; b.toString = function () { return A }
                        }
                    })
                }; a.prototype.css = function (b) {
                    var d = this.styles, c = {}, h = this.element, l = !d; b.color && (b.fill = b.color); d && z(b, function (b, H) { d && d[H] !== b && (c[H] = b, l = !0) }); if (l) {
                        d && (b = r(d, c)); if (null === b.width || "auto" === b.width) delete this.textWidth;
                        else if ("text" === h.nodeName.toLowerCase() && b.width) var q = this.textWidth = O(b.width); this.styles = b; q && !e && this.renderer.forExport && delete b.width; var m = G(b); h.namespaceURI === this.SVG_NS && ["textOutline", "textOverflow", "width"].forEach(function (b) { return m && delete m[b] }); u(h, m); this.added && ("text" === this.element.nodeName && this.renderer.buildText(this), b.textOutline && this.applyTextOutline(b.textOutline))
                    } return this
                }; a.prototype.dashstyleSetter = function (b) {
                    var d = this["stroke-width"]; "inherit" === d && (d =
                        1); if (b = b && b.toLowerCase()) { var c = b.replace("shortdashdotdot", "3,1,1,1,1,1,").replace("shortdashdot", "3,1,1,1").replace("shortdot", "1,1,").replace("shortdash", "3,1,").replace("longdash", "8,3,").replace(/dot/g, "1,3,").replace("dash", "4,3,").replace(/,$/, "").split(","); for (b = c.length; b--;)c[b] = "" + O(c[b]) * q(d, NaN); b = c.join(",").replace(/NaN/g, "none"); this.element.setAttribute("stroke-dasharray", b) }
                }; a.prototype.destroy = function () {
                    var b = this, d = b.element || {}, c = b.renderer, h = d.ownerSVGElement, l = c.isSVG &&
                        "SPAN" === d.nodeName && b.parentGroup || void 0; d.onclick = d.onmouseout = d.onmouseover = d.onmousemove = d.point = null; C(b); if (b.clipPath && h) { var e = b.clipPath;[].forEach.call(h.querySelectorAll("[clip-path],[CLIP-PATH]"), function (b) { -1 < b.getAttribute("clip-path").indexOf(e.element.id) && b.removeAttribute("clip-path") }); b.clipPath = e.destroy() } if (b.stops) { for (h = 0; h < b.stops.length; h++)b.stops[h].destroy(); b.stops.length = 0; b.stops = void 0 } b.safeRemoveChild(d); for (c.styledMode || b.destroyShadows(); l && l.div && 0 === l.div.childNodes.length;)d =
                            l.parentGroup, b.safeRemoveChild(l.div), delete l.div, l = d; b.alignTo && A(c.alignedObjects, b); z(b, function (d, c) { b[c] && b[c].parentGroup === b && b[c].destroy && b[c].destroy(); delete b[c] })
                }; a.prototype.destroyShadows = function () { (this.shadows || []).forEach(function (b) { this.safeRemoveChild(b) }, this); this.shadows = void 0 }; a.prototype.destroyTextPath = function (b, d) {
                    var c = b.getElementsByTagName("text")[0]; if (c) {
                        if (c.removeAttribute("dx"), c.removeAttribute("dy"), d.element.setAttribute("id", ""), this.textPathWrapper &&
                            c.getElementsByTagName("textPath").length) { for (b = this.textPathWrapper.element.childNodes; b.length;)c.appendChild(b[0]); c.removeChild(this.textPathWrapper.element) }
                    } else if (b.getAttribute("dx") || b.getAttribute("dy")) b.removeAttribute("dx"), b.removeAttribute("dy"); this.textPathWrapper && (this.textPathWrapper = this.textPathWrapper.destroy())
                }; a.prototype.dSetter = function (b, d, c) {
                    h(b) && ("string" === typeof b[0] && (b = this.renderer.pathToSegments(b)), this.pathArray = b, b = b.reduce(function (b, d, c) {
                        return d && d.join ?
                            (c ? b + " " : "") + d.join(" ") : (d || "").toString()
                    }, "")); /(NaN| {2}|^$)/.test(b) && (b = "M 0 0"); this[d] !== b && (c.setAttribute(d, b), this[d] = b)
                }; a.prototype.fadeOut = function (b) { var d = this; d.animate({ opacity: 0 }, { duration: q(b, 150), complete: function () { d.hide() } }) }; a.prototype.fillSetter = function (b, d, c) { "string" === typeof b ? c.setAttribute(d, b) : b && this.complexColor(b, d, c) }; a.prototype.getBBox = function (d, c) {
                    var h = this.alignValue, l = this.element, e = this.renderer, m = this.styles, z = this.textStr, n = e.cache, E = e.cacheKeys, G = l.namespaceURI ===
                        this.SVG_NS; c = q(c, this.rotation, 0); var k = e.styledMode ? l && a.prototype.getStyle.call(l, "font-size") : m && m.fontSize, g; if (w(z)) { var t = z.toString(); -1 === t.indexOf("<") && (t = t.replace(/[0-9]/g, "0")); t += ["", c, k, this.textWidth, h, m && m.textOverflow, m && m.fontWeight].join() } t && !d && (g = n[t]); if (!g) {
                            if (G || e.forExport) {
                                try {
                                    var v = this.fakeTS && function (b) { var d = l.querySelector(".highcharts-text-outline"); d && u(d, { display: b }) }; b(v) && v("none"); g = l.getBBox ? r({}, l.getBBox()) : { width: l.offsetWidth, height: l.offsetHeight }; b(v) &&
                                        v("")
                                } catch (ha) { "" } if (!g || 0 > g.width) g = { x: 0, y: 0, width: 0, height: 0 }
                            } else g = this.htmlGetBBox(); if (e.isSVG && (e = g.width, d = g.height, G && (g.height = d = { "11px,17": 14, "13px,20": 16 }["" + (k || "") + ",".concat(Math.round(d))] || d), c)) {
                                G = Number(l.getAttribute("y") || 0) - g.y; h = { right: 1, center: .5 }[h || 0] || 0; m = c * x; k = (c - 90) * x; var A = e * Math.cos(m); c = e * Math.sin(m); v = Math.cos(k); m = Math.sin(k); e = g.x + h * (e - A) + G * v; k = e + A; v = k - d * v; A = v - A; G = g.y + G - h * c + G * m; h = G + c; d = h - d * m; c = d - c; g.x = Math.min(e, k, v, A); g.y = Math.min(G, h, d, c); g.width = Math.max(e, k,
                                    v, A) - g.x; g.height = Math.max(G, h, d, c) - g.y
                            } if (t && ("" === z || 0 < g.height)) { for (; 250 < E.length;)delete n[E.shift()]; n[t] || E.push(t); n[t] = g }
                        } return g
                }; a.prototype.getStyle = function (b) { return n.getComputedStyle(this.element || this, "").getPropertyValue(b) }; a.prototype.hasClass = function (b) { return -1 !== ("" + this.attr("class")).split(" ").indexOf(b) }; a.prototype.hide = function () { return this.attr({ visibility: "hidden" }) }; a.prototype.htmlGetBBox = function () { return { height: 0, width: 0, x: 0, y: 0 } }; a.prototype.init = function (b,
                    d) { this.element = "span" === d ? v(d) : p.createElementNS(this.SVG_NS, d); this.renderer = b; m(this, "afterInit") }; a.prototype.invert = function (b) { this.inverted = b; this.updateTransform(); return this }; a.prototype.on = function (b, d) { var c = this.onEvents; if (c[b]) c[b](); c[b] = k(this.element, b, d); return this }; a.prototype.opacitySetter = function (b, d, c) { this.opacity = b = Number(Number(b).toFixed(3)); c.setAttribute(d, b) }; a.prototype.removeClass = function (b) {
                        return this.attr("class", ("" + this.attr("class")).replace(d(b) ? new RegExp("(^| )".concat(b,
                            "( |$)")) : b, " ").replace(/ +/g, " ").trim())
                    }; a.prototype.removeTextOutline = function () { var b = this.element.querySelector("tspan.highcharts-text-outline"); b && this.safeRemoveChild(b) }; a.prototype.safeRemoveChild = function (b) { var d = b.parentNode; d && d.removeChild(b) }; a.prototype.setRadialReference = function (b) { var d = this.element.gradient && this.renderer.gradients[this.element.gradient]; this.element.radialReference = b; d && d.radAttr && d.animate(this.renderer.getRadialAttr(b, d.radAttr)); return this }; a.prototype.setTextPath =
                        function (b, d) {
                            var c = this.element, h = this.text ? this.text.element : c, e = { textAnchor: "text-anchor" }, q = !1, m = this.textPathWrapper, a = !m; d = G(!0, { enabled: !0, attributes: { dy: -5, startOffset: "50%", textAnchor: "middle" } }, d); var E = f.filterUserAttributes(d.attributes); if (b && d && d.enabled) {
                                m && null === m.element.parentNode ? (a = !0, m = m.destroy()) : m && this.removeTextOutline.call(m.parentGroup); this.options && this.options.padding && (E.dx = -this.options.padding); m || (this.textPathWrapper = m = this.renderer.createElement("textPath"), q =
                                    !0); var k = m.element; (d = b.element.getAttribute("id")) || b.element.setAttribute("id", d = S()); if (a) for (h.setAttribute("y", 0), l(E.dx) && h.setAttribute("x", -E.dx), b = [].slice.call(h.childNodes), a = 0; a < b.length; a++) { var t = b[a]; t.nodeType !== n.Node.TEXT_NODE && "tspan" !== t.nodeName || k.appendChild(t) } q && m && m.add({ element: h }); k.setAttributeNS("http://www.w3.org/1999/xlink", "href", this.renderer.url + "#" + d); w(E.dy) && (k.parentNode.setAttribute("dy", E.dy), delete E.dy); w(E.dx) && (k.parentNode.setAttribute("dx", E.dx), delete E.dx);
                                z(E, function (b, d) { k.setAttribute(e[d] || d, b) }); c.removeAttribute("transform"); this.removeTextOutline.call(m); this.text && !this.renderer.styledMode && this.attr({ fill: "none", "stroke-width": 0 }); this.applyTextOutline = this.updateTransform = g
                            } else m && (delete this.updateTransform, delete this.applyTextOutline, this.destroyTextPath(c, b), this.updateTransform(), this.options && this.options.rotation && this.applyTextOutline(this.options.style.textOutline)); return this
                        }; a.prototype.shadow = function (b, d, c) {
                            var h = [], l = this.element,
                            e = this.oldShadowOptions, H = { color: "#000000", offsetX: this.parentInverted ? -1 : 1, offsetY: this.parentInverted ? -1 : 1, opacity: .15, width: 3 }, q = !1, E; !0 === b ? E = H : "object" === typeof b && (E = r(H, b)); E && (E && e && z(E, function (b, d) { b !== e[d] && (q = !0) }), q && this.destroyShadows(), this.oldShadowOptions = E); if (!E) this.destroyShadows(); else if (!this.shadows) {
                                var m = E.opacity / E.width; var a = this.parentInverted ? "translate(".concat(E.offsetY, ", ").concat(E.offsetX, ")") : "translate(".concat(E.offsetX, ", ").concat(E.offsetY, ")"); for (H = 1; H <=
                                    E.width; H++) { var n = l.cloneNode(!1); var k = 2 * E.width + 1 - 2 * H; t(n, { stroke: b.color || "#000000", "stroke-opacity": m * H, "stroke-width": k, transform: a, fill: "none" }); n.setAttribute("class", (n.getAttribute("class") || "") + " highcharts-shadow"); c && (t(n, "height", Math.max(t(n, "height") - k, 0)), n.cutHeight = k); d ? d.element.appendChild(n) : l.parentNode && l.parentNode.insertBefore(n, l); h.push(n) } this.shadows = h
                            } return this
                        }; a.prototype.show = function (b) { void 0 === b && (b = !0); return this.attr({ visibility: b ? "inherit" : "visible" }) }; a.prototype.strokeSetter =
                            function (b, d, c) { this[d] = b; this.stroke && this["stroke-width"] ? (a.prototype.fillSetter.call(this, this.stroke, "stroke", c), c.setAttribute("stroke-width", this["stroke-width"]), this.hasStroke = !0) : "stroke-width" === d && 0 === b && this.hasStroke ? (c.removeAttribute("stroke"), this.hasStroke = !1) : this.renderer.styledMode && this["stroke-width"] && (c.setAttribute("stroke-width", this["stroke-width"]), this.hasStroke = !0) }; a.prototype.strokeWidth = function () {
                                if (!this.renderer.styledMode) return this["stroke-width"] || 0; var b =
                                    this.getStyle("stroke-width"), d = 0; if (b.indexOf("px") === b.length - 2) d = O(b); else if ("" !== b) { var h = p.createElementNS(c, "rect"); t(h, { width: b, "stroke-width": 0 }); this.element.parentNode.appendChild(h); d = h.getBBox().width; h.parentNode.removeChild(h) } return d
                            }; a.prototype.symbolAttr = function (b) { var d = this; "x y r start end width height innerR anchorX anchorY clockwise".split(" ").forEach(function (c) { d[c] = q(b[c], d[c]) }); d.attr({ d: d.renderer.symbols[d.symbolName](d.x, d.y, d.width, d.height, d) }) }; a.prototype.textSetter =
                                function (b) { b !== this.textStr && (delete this.textPxLength, this.textStr = b, this.added && this.renderer.buildText(this)) }; a.prototype.titleSetter = function (b) { var d = this.element, c = d.getElementsByTagName("title")[0] || p.createElementNS(this.SVG_NS, "title"); d.insertBefore ? d.insertBefore(c, d.firstChild) : d.appendChild(c); c.textContent = String(q(b, "")).replace(/<[^>]*>/g, "").replace(/&lt;/g, "<").replace(/&gt;/g, ">") }; a.prototype.toFront = function () { var b = this.element; b.parentNode.appendChild(b); return this }; a.prototype.translate =
                                    function (b, d) { return this.attr({ translateX: b, translateY: d }) }; a.prototype.updateShadows = function (b, d, c) { var h = this.shadows; if (h) for (var l = h.length; l--;)c.call(h[l], "height" === b ? Math.max(d - (h[l].cutHeight || 0), 0) : "d" === b ? this.d : d, b, h[l]) }; a.prototype.updateTransform = function () {
                                        var b = this.scaleX, d = this.scaleY, c = this.inverted, h = this.rotation, l = this.matrix, e = this.element, m = this.translateX || 0, a = this.translateY || 0; c && (m += this.width, a += this.height); m = ["translate(" + m + "," + a + ")"]; w(l) && m.push("matrix(" + l.join(",") +
                                            ")"); c ? m.push("rotate(90) scale(-1,1)") : h && m.push("rotate(" + h + " " + q(this.rotationOriginX, e.getAttribute("x"), 0) + " " + q(this.rotationOriginY, e.getAttribute("y") || 0) + ")"); (w(b) || w(d)) && m.push("scale(" + q(b, 1) + " " + q(d, 1) + ")"); m.length && e.setAttribute("transform", m.join(" "))
                                    }; a.prototype.visibilitySetter = function (b, d, c) { "inherit" === b ? c.removeAttribute(d) : this[d] !== b && c.setAttribute(d, b); this[d] = b }; a.prototype.xGetter = function (b) { "circle" === this.element.nodeName && ("x" === b ? b = "cx" : "y" === b && (b = "cy")); return this._defaultGetter(b) };
                a.prototype.zIndexSetter = function (b, d) {
                    var c = this.renderer, h = this.parentGroup, l = (h || c).element || c.box, e = this.element; c = l === c.box; var q = !1; var m = this.added; var E; w(b) ? (e.setAttribute("data-z-index", b), b = +b, this[d] === b && (m = !1)) : w(this[d]) && e.removeAttribute("data-z-index"); this[d] = b; if (m) {
                        (b = this.zIndex) && h && (h.handleZ = !0); d = l.childNodes; for (E = d.length - 1; 0 <= E && !q; E--) {
                            h = d[E]; m = h.getAttribute("data-z-index"); var a = !w(m); if (h !== e) if (0 > b && a && !c && !E) l.insertBefore(e, d[E]), q = !0; else if (O(m) <= b || a && (!w(b) ||
                                0 <= b)) l.insertBefore(e, d[E + 1] || null), q = !0
                        } q || (l.insertBefore(e, d[c ? 3 : 0] || null), q = !0)
                    } return q
                }; return a
            }(); a.prototype["stroke-widthSetter"] = a.prototype.strokeSetter; a.prototype.yGetter = a.prototype.xGetter; a.prototype.matrixSetter = a.prototype.rotationOriginXSetter = a.prototype.rotationOriginYSetter = a.prototype.rotationSetter = a.prototype.scaleXSetter = a.prototype.scaleYSetter = a.prototype.translateXSetter = a.prototype.translateYSetter = a.prototype.verticalAlignSetter = function (b, d) {
                this[d] = b; this.doTransform =
                    !0
            }; ""; return a
        }); L(f, "Core/Renderer/RendererRegistry.js", [f["Core/Globals.js"]], function (a) { var f; (function (f) { f.rendererTypes = {}; var D; f.getRendererType = function (a) { void 0 === a && (a = D); return f.rendererTypes[a] || f.rendererTypes[D] }; f.registerRendererType = function (y, B, J) { f.rendererTypes[y] = B; if (!D || J) D = y, a.Renderer = B } })(f || (f = {})); return f }); L(f, "Core/Renderer/SVG/SVGLabel.js", [f["Core/Renderer/SVG/SVGElement.js"], f["Core/Utilities.js"]], function (a, f) {
            var D = this && this.__extends || function () {
                var a = function (g,
                    e) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (c, e) { c.__proto__ = e } || function (c, e) { for (var a in e) e.hasOwnProperty(a) && (c[a] = e[a]) }; return a(g, e) }; return function (g, e) { function c() { this.constructor = g } a(g, e); g.prototype = null === e ? Object.create(e) : (c.prototype = e.prototype, new c) }
            }(), F = f.defined, y = f.extend, I = f.isNumber, J = f.merge, C = f.pick, x = f.removeEvent; return function (f) {
                function g(e, c, a, k, t, v, u, w, A, r) {
                    var m = f.call(this) || this; m.paddingLeftSetter = m.paddingSetter; m.paddingRightSetter =
                        m.paddingSetter; m.init(e, "g"); m.textStr = c; m.x = a; m.y = k; m.anchorX = v; m.anchorY = u; m.baseline = A; m.className = r; m.addClass("button" === r ? "highcharts-no-tooltip" : "highcharts-label"); r && m.addClass("highcharts-" + r); m.text = e.text(void 0, 0, 0, w).attr({ zIndex: 1 }); var h; "string" === typeof t && ((h = /^url\((.*?)\)$/.test(t)) || m.renderer.symbols[t]) && (m.symbolKey = t); m.bBox = g.emptyBBox; m.padding = 3; m.baselineOffset = 0; m.needsBox = e.styledMode || h; m.deferredAttr = {}; m.alignFactor = 0; return m
                } D(g, f); g.prototype.alignSetter = function (e) {
                    e =
                    { left: 0, center: .5, right: 1 }[e]; e !== this.alignFactor && (this.alignFactor = e, this.bBox && I(this.xSetting) && this.attr({ x: this.xSetting }))
                }; g.prototype.anchorXSetter = function (e, c) { this.anchorX = e; this.boxAttr(c, Math.round(e) - this.getCrispAdjust() - this.xSetting) }; g.prototype.anchorYSetter = function (e, c) { this.anchorY = e; this.boxAttr(c, e - this.ySetting) }; g.prototype.boxAttr = function (e, c) { this.box ? this.box.attr(e, c) : this.deferredAttr[e] = c }; g.prototype.css = function (e) {
                    if (e) {
                        var c = {}; e = J(e); g.textProps.forEach(function (a) {
                            "undefined" !==
                            typeof e[a] && (c[a] = e[a], delete e[a])
                        }); this.text.css(c); var n = "width" in c; "fontSize" in c || "fontWeight" in c ? this.updateTextPadding() : n && this.updateBoxSize()
                    } return a.prototype.css.call(this, e)
                }; g.prototype.destroy = function () { x(this.element, "mouseenter"); x(this.element, "mouseleave"); this.text && this.text.destroy(); this.box && (this.box = this.box.destroy()); a.prototype.destroy.call(this) }; g.prototype.fillSetter = function (e, c) { e && (this.needsBox = !0); this.fill = e; this.boxAttr(c, e) }; g.prototype.getBBox = function () {
                    this.textStr &&
                    0 === this.bBox.width && 0 === this.bBox.height && this.updateBoxSize(); var e = this.padding, c = C(this.paddingLeft, e); return { width: this.width, height: this.height, x: this.bBox.x - c, y: this.bBox.y - e }
                }; g.prototype.getCrispAdjust = function () { return this.renderer.styledMode && this.box ? this.box.strokeWidth() % 2 / 2 : (this["stroke-width"] ? parseInt(this["stroke-width"], 10) : 0) % 2 / 2 }; g.prototype.heightSetter = function (e) { this.heightSetting = e }; g.prototype.onAdd = function () {
                    var e = this.textStr; this.text.add(this); this.attr({
                        text: F(e) ?
                            e : "", x: this.x, y: this.y
                    }); this.box && F(this.anchorX) && this.attr({ anchorX: this.anchorX, anchorY: this.anchorY })
                }; g.prototype.paddingSetter = function (e, c) { I(e) ? e !== this[c] && (this[c] = e, this.updateTextPadding()) : this[c] = void 0 }; g.prototype.rSetter = function (e, c) { this.boxAttr(c, e) }; g.prototype.shadow = function (e) { e && !this.renderer.styledMode && (this.updateBoxSize(), this.box && this.box.shadow(e)); return this }; g.prototype.strokeSetter = function (e, c) { this.stroke = e; this.boxAttr(c, e) }; g.prototype["stroke-widthSetter"] =
                    function (e, c) { e && (this.needsBox = !0); this["stroke-width"] = e; this.boxAttr(c, e) }; g.prototype["text-alignSetter"] = function (e) { this.textAlign = e }; g.prototype.textSetter = function (e) { "undefined" !== typeof e && this.text.attr({ text: e }); this.updateTextPadding() }; g.prototype.updateBoxSize = function () {
                        var e = this.text.element.style, c = {}, a = this.padding, k = this.bBox = I(this.widthSetting) && I(this.heightSetting) && !this.textAlign || !F(this.text.textStr) ? g.emptyBBox : this.text.getBBox(); this.width = this.getPaddedWidth(); this.height =
                            (this.heightSetting || k.height || 0) + 2 * a; e = this.renderer.fontMetrics(e && e.fontSize, this.text); this.baselineOffset = a + Math.min((this.text.firstLineMetrics || e).b, k.height || Infinity); this.heightSetting && (this.baselineOffset += (this.heightSetting - e.h) / 2); this.needsBox && (this.box || (a = this.box = this.symbolKey ? this.renderer.symbol(this.symbolKey) : this.renderer.rect(), a.addClass(("button" === this.className ? "" : "highcharts-label-box") + (this.className ? " highcharts-" + this.className + "-box" : "")), a.add(this)), a = this.getCrispAdjust(),
                                c.x = a, c.y = (this.baseline ? -this.baselineOffset : 0) + a, c.width = Math.round(this.width), c.height = Math.round(this.height), this.box.attr(y(c, this.deferredAttr)), this.deferredAttr = {})
                    }; g.prototype.updateTextPadding = function () {
                        var e = this.text; this.updateBoxSize(); var c = this.baseline ? 0 : this.baselineOffset, a = C(this.paddingLeft, this.padding); F(this.widthSetting) && this.bBox && ("center" === this.textAlign || "right" === this.textAlign) && (a += { center: .5, right: 1 }[this.textAlign] * (this.widthSetting - this.bBox.width)); if (a !==
                            e.x || c !== e.y) e.attr("x", a), e.hasBoxWidthChanged && (this.bBox = e.getBBox(!0)), "undefined" !== typeof c && e.attr("y", c); e.x = a; e.y = c
                    }; g.prototype.widthSetter = function (e) { this.widthSetting = I(e) ? e : void 0 }; g.prototype.getPaddedWidth = function () { var e = this.padding, c = C(this.paddingLeft, e); e = C(this.paddingRight, e); return (this.widthSetting || this.bBox.width || 0) + c + e }; g.prototype.xSetter = function (e) {
                        this.x = e; this.alignFactor && (e -= this.alignFactor * this.getPaddedWidth(), this["forceAnimate:x"] = !0); this.xSetting = Math.round(e);
                        this.attr("translateX", this.xSetting)
                    }; g.prototype.ySetter = function (e) { this.ySetting = this.y = Math.round(e); this.attr("translateY", this.ySetting) }; g.emptyBBox = { width: 0, height: 0, x: 0, y: 0 }; g.textProps = "color direction fontFamily fontSize fontStyle fontWeight lineHeight textAlign textDecoration textOutline textOverflow width".split(" "); return g
            }(a)
        }); L(f, "Core/Renderer/SVG/Symbols.js", [f["Core/Utilities.js"]], function (a) {
            function f(a, f, p, g, e) {
                var c = []; if (e) {
                    var n = e.start || 0, k = J(e.r, p); p = J(e.r, g || p); var t =
                        (e.end || 0) - .001; g = e.innerR; var v = J(e.open, .001 > Math.abs((e.end || 0) - n - 2 * Math.PI)), u = Math.cos(n), w = Math.sin(n), A = Math.cos(t), r = Math.sin(t); n = J(e.longArc, .001 > t - n - Math.PI ? 0 : 1); c.push(["M", a + k * u, f + p * w], ["A", k, p, 0, n, J(e.clockwise, 1), a + k * A, f + p * r]); y(g) && c.push(v ? ["M", a + g * A, f + g * r] : ["L", a + g * A, f + g * r], ["A", g, g, 0, n, y(e.clockwise) ? 1 - e.clockwise : 0, a + g * u, f + g * w]); v || c.push(["Z"])
                } return c
            } function B(a, f, p, g, e) { return e && e.r ? F(a, f, p, g, e) : [["M", a, f], ["L", a + p, f], ["L", a + p, f + g], ["L", a, f + g], ["Z"]] } function F(a, f, p, g,
                e) { e = e && e.r || 0; return [["M", a + e, f], ["L", a + p - e, f], ["C", a + p, f, a + p, f, a + p, f + e], ["L", a + p, f + g - e], ["C", a + p, f + g, a + p, f + g, a + p - e, f + g], ["L", a + e, f + g], ["C", a, f + g, a, f + g, a, f + g - e], ["L", a, f + e], ["C", a, f, a, f, a + e, f]] } var y = a.defined, I = a.isNumber, J = a.pick; return {
                    arc: f, callout: function (a, f, p, g, e) {
                        var c = Math.min(e && e.r || 0, p, g), n = c + 6, k = e && e.anchorX; e = e && e.anchorY || 0; var t = F(a, f, p, g, { r: c }); if (!I(k)) return t; a + k >= p ? e > f + n && e < f + g - n ? t.splice(3, 1, ["L", a + p, e - 6], ["L", a + p + 6, e], ["L", a + p, e + 6], ["L", a + p, f + g - c]) : t.splice(3, 1, ["L", a + p, g /
                            2], ["L", k, e], ["L", a + p, g / 2], ["L", a + p, f + g - c]) : 0 >= a + k ? e > f + n && e < f + g - n ? t.splice(7, 1, ["L", a, e + 6], ["L", a - 6, e], ["L", a, e - 6], ["L", a, f + c]) : t.splice(7, 1, ["L", a, g / 2], ["L", k, e], ["L", a, g / 2], ["L", a, f + c]) : e && e > g && k > a + n && k < a + p - n ? t.splice(5, 1, ["L", k + 6, f + g], ["L", k, f + g + 6], ["L", k - 6, f + g], ["L", a + c, f + g]) : e && 0 > e && k > a + n && k < a + p - n && t.splice(1, 1, ["L", k - 6, f], ["L", k, f - 6], ["L", k + 6, f], ["L", p - c, f]); return t
                    }, circle: function (a, x, p, g) { return f(a + p / 2, x + g / 2, p / 2, g / 2, { start: .5 * Math.PI, end: 2.5 * Math.PI, open: !1 }) }, diamond: function (a, f, p, g) {
                        return [["M",
                            a + p / 2, f], ["L", a + p, f + g / 2], ["L", a + p / 2, f + g], ["L", a, f + g / 2], ["Z"]]
                    }, rect: B, roundedRect: F, square: B, triangle: function (a, f, p, g) { return [["M", a + p / 2, f], ["L", a + p, f + g], ["L", a, f + g], ["Z"]] }, "triangle-down": function (a, f, p, g) { return [["M", a, f], ["L", a + p, f], ["L", a + p / 2, f + g], ["Z"]] }
                }
        }); L(f, "Core/Renderer/SVG/TextBuilder.js", [f["Core/Renderer/HTML/AST.js"], f["Core/Globals.js"], f["Core/Utilities.js"]], function (a, f, B) {
            var D = f.doc, y = f.SVG_NS, I = f.win, J = B.attr, C = B.extend, x = B.isString, p = B.objectEach, g = B.pick; return function () {
                function e(c) {
                    var a =
                        c.styles; this.renderer = c.renderer; this.svgElement = c; this.width = c.textWidth; this.textLineHeight = a && a.lineHeight; this.textOutline = a && a.textOutline; this.ellipsis = !(!a || "ellipsis" !== a.textOverflow); this.noWrap = !(!a || "nowrap" !== a.whiteSpace); this.fontSize = a && a.fontSize
                } e.prototype.buildSVG = function () {
                    var c = this.svgElement, e = c.element, k = c.renderer, t = g(c.textStr, "").toString(), v = -1 !== t.indexOf("<"), f = e.childNodes; k = this.width && !c.added && k.box; var w = /<br.*?>/g, A = [t, this.ellipsis, this.noWrap, this.textLineHeight,
                        this.textOutline, this.fontSize, this.width].join(); if (A !== c.textCache) {
                            c.textCache = A; delete c.actualWidth; for (A = f.length; A--;)e.removeChild(f[A]); v || this.ellipsis || this.width || -1 !== t.indexOf(" ") && (!this.noWrap || w.test(t)) ? "" !== t && (k && k.appendChild(e), t = new a(t), this.modifyTree(t.nodes), t.addToDOM(c.element), this.modifyDOM(), this.ellipsis && -1 !== (e.textContent || "").indexOf("\u2026") && c.attr("title", this.unescapeEntities(c.textStr || "", ["&lt;", "&gt;"])), k && k.removeChild(e)) : e.appendChild(D.createTextNode(this.unescapeEntities(t)));
                            x(this.textOutline) && c.applyTextOutline && c.applyTextOutline(this.textOutline)
                        }
                }; e.prototype.modifyDOM = function () {
                    var c = this, a = this.svgElement, e = J(a.element, "x"); a.firstLineMetrics = void 0; for (var g; g = a.element.firstChild;)if (/^[\s\u200B]*$/.test(g.textContent || " ")) a.element.removeChild(g); else break;[].forEach.call(a.element.querySelectorAll("tspan.highcharts-br"), function (g, k) {
                        g.nextSibling && g.previousSibling && (0 === k && 1 === g.previousSibling.nodeType && (a.firstLineMetrics = a.renderer.fontMetrics(void 0,
                            g.previousSibling)), J(g, { dy: c.getLineHeight(g.nextSibling), x: e }))
                    }); var v = this.width || 0; if (v) {
                        var f = function (g, k) {
                            var m = g.textContent || "", h = m.replace(/([^\^])-/g, "$1- ").split(" "), b = !c.noWrap && (1 < h.length || 1 < a.element.childNodes.length), l = c.getLineHeight(k), d = 0, G = a.actualWidth; if (c.ellipsis) m && c.truncate(g, m, void 0, 0, Math.max(0, v - parseInt(c.fontSize || 12, 10)), function (b, d) { return b.substring(0, d) + "\u2026" }); else if (b) {
                                m = []; for (b = []; k.firstChild && k.firstChild !== g;)b.push(k.firstChild), k.removeChild(k.firstChild);
                                for (; h.length;)h.length && !c.noWrap && 0 < d && (m.push(g.textContent || ""), g.textContent = h.join(" ").replace(/- /g, "-")), c.truncate(g, void 0, h, 0 === d ? G || 0 : 0, v, function (b, d) { return h.slice(0, d).join(" ").replace(/- /g, "-") }), G = a.actualWidth, d++; b.forEach(function (b) { k.insertBefore(b, g) }); m.forEach(function (b) { k.insertBefore(D.createTextNode(b), g); b = D.createElementNS(y, "tspan"); b.textContent = "\u200b"; J(b, { dy: l, x: e }); k.insertBefore(b, g) })
                            }
                        }, w = function (c) {
                            [].slice.call(c.childNodes).forEach(function (e) {
                                e.nodeType ===
                                I.Node.TEXT_NODE ? f(e, c) : (-1 !== e.className.baseVal.indexOf("highcharts-br") && (a.actualWidth = 0), w(e))
                            })
                        }; w(a.element)
                    }
                }; e.prototype.getLineHeight = function (c) { var a; c = c.nodeType === I.Node.TEXT_NODE ? c.parentElement : c; this.renderer.styledMode || (a = c && /(px|em)$/.test(c.style.fontSize) ? c.style.fontSize : this.fontSize || this.renderer.style.fontSize || 12); return this.textLineHeight ? parseInt(this.textLineHeight.toString(), 10) : this.renderer.fontMetrics(a, c || this.svgElement.element).h }; e.prototype.modifyTree = function (c) {
                    var a =
                        this, e = function (g, k) {
                            var n = g.attributes; n = void 0 === n ? {} : n; var t = g.children, v = g.style; v = void 0 === v ? {} : v; var f = g.tagName, m = a.renderer.styledMode; if ("b" === f || "strong" === f) m ? n["class"] = "highcharts-strong" : v.fontWeight = "bold"; else if ("i" === f || "em" === f) m ? n["class"] = "highcharts-emphasized" : v.fontStyle = "italic"; v && v.color && (v.fill = v.color); "br" === f ? (n["class"] = "highcharts-br", g.textContent = "\u200b", (k = c[k + 1]) && k.textContent && (k.textContent = k.textContent.replace(/^ +/gm, ""))) : "a" === f && t && t.some(function (c) {
                                return "#text" ===
                                    c.tagName
                            }) && (g.children = [{ children: t, tagName: "tspan" }]); "#text" !== f && "a" !== f && (g.tagName = "tspan"); C(g, { attributes: n, style: v }); t && t.filter(function (c) { return "#text" !== c.tagName }).forEach(e)
                        }; c.forEach(e)
                }; e.prototype.truncate = function (c, a, e, g, v, f) {
                    var k = this.svgElement, n = k.renderer, t = k.rotation, m = [], h = e ? 1 : 0, b = (a || e || "").length, l = b, d, G = function (b, d) {
                        d = d || b; var h = c.parentNode; if (h && "undefined" === typeof m[d]) if (h.getSubStringLength) try { m[d] = g + h.getSubStringLength(0, e ? d + 1 : d) } catch (S) { "" } else n.getSpanWidth &&
                            (c.textContent = f(a || e, b), m[d] = g + n.getSpanWidth(k, c)); return m[d]
                    }; k.rotation = 0; var z = G(c.textContent.length); if (g + z > v) { for (; h <= b;)l = Math.ceil((h + b) / 2), e && (d = f(e, l)), z = G(l, d && d.length - 1), h === b ? h = b + 1 : z > v ? b = l - 1 : h = l; 0 === b ? c.textContent = "" : a && b === a.length - 1 || (c.textContent = d || f(a || e, l)) } e && e.splice(0, l); k.actualWidth = z; k.rotation = t
                }; e.prototype.unescapeEntities = function (c, a) { p(this.renderer.escapes, function (e, g) { a && -1 !== a.indexOf(e) || (c = c.toString().replace(new RegExp(e, "g"), g)) }); return c }; return e
            }()
        });
    L(f, "Core/Renderer/SVG/SVGRenderer.js", [f["Core/Renderer/HTML/AST.js"], f["Core/Color/Color.js"], f["Core/Globals.js"], f["Core/Renderer/RendererRegistry.js"], f["Core/Renderer/SVG/SVGElement.js"], f["Core/Renderer/SVG/SVGLabel.js"], f["Core/Renderer/SVG/Symbols.js"], f["Core/Renderer/SVG/TextBuilder.js"], f["Core/Utilities.js"]], function (a, f, B, F, y, I, J, C, x) {
        var p = B.charts, g = B.deg2rad, e = B.doc, c = B.isFirefox, n = B.isMS, k = B.isWebKit, t = B.noop, v = B.SVG_NS, u = B.symbolSizes, w = B.win, A = x.addEvent, r = x.attr, m = x.createElement,
        h = x.css, b = x.defined, l = x.destroyObjectProperties, d = x.extend, G = x.isArray, z = x.isNumber, q = x.isObject, O = x.isString, P = x.merge, S = x.pick, N = x.pInt, D = x.uniqueKey, W; B = function () {
            function H(b, d, c, a, h, e, l) { this.width = this.url = this.style = this.isSVG = this.imgCount = this.height = this.gradients = this.globalAnimation = this.defs = this.chartIndex = this.cacheKeys = this.cache = this.boxWrapper = this.box = this.alignedObjects = void 0; this.init(b, d, c, a, h, e, l) } H.prototype.init = function (b, d, a, l, q, m, H) {
                var E = this.createElement("svg").attr({
                    version: "1.1",
                    "class": "highcharts-root"
                }), K = E.element; H || E.css(this.getStyle(l)); b.appendChild(K); r(b, "dir", "ltr"); -1 === b.innerHTML.indexOf("xmlns") && r(K, "xmlns", this.SVG_NS); this.isSVG = !0; this.box = K; this.boxWrapper = E; this.alignedObjects = []; this.url = this.getReferenceURL(); this.createElement("desc").add().element.appendChild(e.createTextNode("Created with Highcharts 10.1.0")); this.defs = this.createElement("defs").add(); this.allowHTML = m; this.forExport = q; this.styledMode = H; this.gradients = {}; this.cache = {}; this.cacheKeys =
                    []; this.imgCount = 0; this.setSize(d, a, !1); var g; c && b.getBoundingClientRect && (d = function () { h(b, { left: 0, top: 0 }); g = b.getBoundingClientRect(); h(b, { left: Math.ceil(g.left) - g.left + "px", top: Math.ceil(g.top) - g.top + "px" }) }, d(), this.unSubPixelFix = A(w, "resize", d))
            }; H.prototype.definition = function (b) { return (new a([b])).addToDOM(this.defs.element) }; H.prototype.getReferenceURL = function () {
                if ((c || k) && e.getElementsByTagName("base").length) {
                    if (!b(W)) {
                        var d = D(); d = (new a([{
                            tagName: "svg", attributes: { width: 8, height: 8 }, children: [{
                                tagName: "defs",
                                children: [{ tagName: "clipPath", attributes: { id: d }, children: [{ tagName: "rect", attributes: { width: 4, height: 4 } }] }]
                            }, { tagName: "rect", attributes: { id: "hitme", width: 8, height: 8, "clip-path": "url(#".concat(d, ")"), fill: "rgba(0,0,0,0.001)" } }]
                        }])).addToDOM(e.body); h(d, { position: "fixed", top: 0, left: 0, zIndex: 9E5 }); var l = e.elementFromPoint(6, 6); W = "hitme" === (l && l.id); e.body.removeChild(d)
                    } if (W) return w.location.href.split("#")[0].replace(/<[^>]*>/g, "").replace(/([\('\)])/g, "\\$1").replace(/ /g, "%20")
                } return ""
            }; H.prototype.getStyle =
                function (b) { return this.style = d({ fontFamily: '"Lucida Grande", "Lucida Sans Unicode", Arial, Helvetica, sans-serif', fontSize: "12px" }, b) }; H.prototype.setStyle = function (b) { this.boxWrapper.css(this.getStyle(b)) }; H.prototype.isHidden = function () { return !this.boxWrapper.getBBox().width }; H.prototype.destroy = function () {
                    var b = this.defs; this.box = null; this.boxWrapper = this.boxWrapper.destroy(); l(this.gradients || {}); this.gradients = null; b && (this.defs = b.destroy()); this.unSubPixelFix && this.unSubPixelFix(); return this.alignedObjects =
                        null
                }; H.prototype.createElement = function (b) { var d = new this.Element; d.init(this, b); return d }; H.prototype.getRadialAttr = function (b, d) { return { cx: b[0] - b[2] / 2 + (d.cx || 0) * b[2], cy: b[1] - b[2] / 2 + (d.cy || 0) * b[2], r: (d.r || 0) * b[2] } }; H.prototype.buildText = function (b) { (new C(b)).buildSVG() }; H.prototype.getContrast = function (b) { b = f.parse(b).rgba; b[0] *= 1; b[1] *= 1.2; b[2] *= .5; return 459 < b[0] + b[1] + b[2] ? "#000000" : "#FFFFFF" }; H.prototype.button = function (b, c, h, e, l, m, H, g, z, k) {
                    var E = this.label(b, c, h, z, void 0, void 0, k, void 0, "button"),
                    K = this.styledMode; b = l && l.states || {}; l && delete l.states; var G = 0, t = l ? P(l) : {}, f = P({ color: "#333333", cursor: "pointer", fontWeight: "normal" }, t.style); delete t.style; t = a.filterUserAttributes(t); E.attr(P({ padding: 8, r: 2 }, t)); if (!K) {
                        t = P({ fill: "#f7f7f7", stroke: "#cccccc", "stroke-width": 1 }, t); m = P(t, { fill: "#e6e6e6" }, a.filterUserAttributes(m || b.hover || {})); var v = m.style; delete m.style; H = P(t, { fill: "#e6ebf5", style: { color: "#000000", fontWeight: "bold" } }, a.filterUserAttributes(H || b.select || {})); var M = H.style; delete H.style;
                        g = P(t, { style: { color: "#cccccc" } }, a.filterUserAttributes(g || b.disabled || {})); var r = g.style; delete g.style
                    } A(E.element, n ? "mouseover" : "mouseenter", function () { 3 !== G && E.setState(1) }); A(E.element, n ? "mouseout" : "mouseleave", function () { 3 !== G && E.setState(G) }); E.setState = function (b) { 1 !== b && (E.state = G = b); E.removeClass(/highcharts-button-(normal|hover|pressed|disabled)/).addClass("highcharts-button-" + ["normal", "hover", "pressed", "disabled"][b || 0]); K || (E.attr([t, m, H, g][b || 0]), b = [f, v, M, r][b || 0], q(b) && E.css(b)) }; K ||
                        E.attr(t).css(d({ cursor: "default" }, f)); return E.on("touchstart", function (b) { return b.stopPropagation() }).on("click", function (b) { 3 !== G && e.call(E, b) })
                }; H.prototype.crispLine = function (d, c, a) { void 0 === a && (a = "round"); var h = d[0], e = d[1]; b(h[1]) && h[1] === e[1] && (h[1] = e[1] = Math[a](h[1]) - c % 2 / 2); b(h[2]) && h[2] === e[2] && (h[2] = e[2] = Math[a](h[2]) + c % 2 / 2); return d }; H.prototype.path = function (b) { var c = this.styledMode ? {} : { fill: "none" }; G(b) ? c.d = b : q(b) && d(c, b); return this.createElement("path").attr(c) }; H.prototype.circle =
                    function (b, d, c) { b = q(b) ? b : "undefined" === typeof b ? {} : { x: b, y: d, r: c }; d = this.createElement("circle"); d.xSetter = d.ySetter = function (b, d, c) { c.setAttribute("c" + d, b) }; return d.attr(b) }; H.prototype.arc = function (b, d, c, a, h, e) { q(b) ? (a = b, d = a.y, c = a.r, b = a.x) : a = { innerR: a, start: h, end: e }; b = this.symbol("arc", b, d, c, c, a); b.r = c; return b }; H.prototype.rect = function (b, d, c, a, h, e) {
                        h = q(b) ? b.r : h; var l = this.createElement("rect"); b = q(b) ? b : "undefined" === typeof b ? {} : { x: b, y: d, width: Math.max(c, 0), height: Math.max(a, 0) }; this.styledMode ||
                            ("undefined" !== typeof e && (b["stroke-width"] = e, b = l.crisp(b)), b.fill = "none"); h && (b.r = h); l.rSetter = function (b, d, c) { l.r = b; r(c, { rx: b, ry: b }) }; l.rGetter = function () { return l.r || 0 }; return l.attr(b)
                    }; H.prototype.setSize = function (b, d, c) { this.width = b; this.height = d; this.boxWrapper.animate({ width: b, height: d }, { step: function () { this.attr({ viewBox: "0 0 " + this.attr("width") + " " + this.attr("height") }) }, duration: S(c, !0) ? void 0 : 0 }); this.alignElements() }; H.prototype.g = function (b) {
                        var d = this.createElement("g"); return b ? d.attr({
                            "class": "highcharts-" +
                                b
                        }) : d
                    }; H.prototype.image = function (b, d, c, a, h, e) {
                        var l = { preserveAspectRatio: "none" }, m = function (b, d) { b.setAttributeNS ? b.setAttributeNS("http://www.w3.org/1999/xlink", "href", d) : b.setAttribute("hc-svg-href", d) }; z(d) && (l.x = d); z(c) && (l.y = c); z(a) && (l.width = a); z(h) && (l.height = h); var q = this.createElement("image").attr(l); d = function (d) { m(q.element, b); e.call(q, d) }; e ? (m(q.element, "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw=="), c = new w.Image, A(c, "load", d), c.src = b, c.complete && d({})) :
                            m(q.element, b); return q
                    }; H.prototype.symbol = function (c, a, l, q, H, E) {
                        var g = this, z = /^url\((.*?)\)$/, G = z.test(c), k = !G && (this.symbols[c] ? c : "circle"), n = k && this.symbols[k], t; if (n) { "number" === typeof a && (t = n.call(this.symbols, Math.round(a || 0), Math.round(l || 0), q || 0, H || 0, E)); var f = this.path(t); g.styledMode || f.attr("fill", "none"); d(f, { symbolName: k || void 0, x: a, y: l, width: q, height: H }); E && d(f, E) } else if (G) {
                            var v = c.match(z)[1]; var K = f = this.image(v); K.imgwidth = S(u[v] && u[v].width, E && E.width); K.imgheight = S(u[v] && u[v].height,
                                E && E.height); var r = function (b) { return b.attr({ width: b.width, height: b.height }) };["width", "height"].forEach(function (d) { K[d + "Setter"] = function (d, c) { var a = this["img" + c]; this[c] = d; b(a) && (E && "within" === E.backgroundSize && this.width && this.height && (a = Math.round(a * Math.min(this.width / this.imgwidth, this.height / this.imgheight))), this.element && this.element.setAttribute(c, a), this.alignByTranslate || (d = ((this[c] || 0) - a) / 2, this.attr("width" === c ? { translateX: d } : { translateY: d }))) } }); b(a) && K.attr({ x: a, y: l }); K.isImg = !0;
                            b(K.imgwidth) && b(K.imgheight) ? r(K) : (K.attr({ width: 0, height: 0 }), m("img", { onload: function () { var b = p[g.chartIndex]; 0 === this.width && (h(this, { position: "absolute", top: "-999em" }), e.body.appendChild(this)); u[v] = { width: this.width, height: this.height }; K.imgwidth = this.width; K.imgheight = this.height; K.element && r(K); this.parentNode && this.parentNode.removeChild(this); g.imgCount--; if (!g.imgCount && b && !b.hasLoaded) b.onload() }, src: v }), this.imgCount++)
                        } return f
                    }; H.prototype.clipRect = function (b, d, c, a) {
                        var h = D() + "-", e =
                            this.createElement("clipPath").attr({ id: h }).add(this.defs); b = this.rect(b, d, c, a, 0).add(e); b.id = h; b.clipPath = e; b.count = 0; return b
                    }; H.prototype.text = function (d, c, a, h) {
                        var e = {}; if (h && (this.allowHTML || !this.forExport)) return this.html(d, c, a); e.x = Math.round(c || 0); a && (e.y = Math.round(a)); b(d) && (e.text = d); d = this.createElement("text").attr(e); if (!h || this.forExport && !this.allowHTML) d.xSetter = function (b, d, c) {
                            for (var a = c.getElementsByTagName("tspan"), h = c.getAttribute(d), e = 0, l; e < a.length; e++)l = a[e], l.getAttribute(d) ===
                                h && l.setAttribute(d, b); c.setAttribute(d, b)
                        }; return d
                    }; H.prototype.fontMetrics = function (b, d) { b = !this.styledMode && /px/.test(b) || !w.getComputedStyle ? b || d && d.style && d.style.fontSize || this.style && this.style.fontSize : d && y.prototype.getStyle.call(d, "font-size"); b = /px/.test(b) ? N(b) : 12; d = 24 > b ? b + 3 : Math.round(1.2 * b); return { h: d, b: Math.round(.8 * d), f: b } }; H.prototype.rotCorr = function (b, d, c) { var a = b; d && c && (a = Math.max(a * Math.cos(d * g), 4)); return { x: -b / 3 * Math.sin(d * g), y: a } }; H.prototype.pathToSegments = function (b) {
                        for (var d =
                            [], c = [], a = { A: 8, C: 7, H: 2, L: 3, M: 3, Q: 5, S: 5, T: 3, V: 2 }, h = 0; h < b.length; h++)O(c[0]) && z(b[h]) && c.length === a[c[0].toUpperCase()] && b.splice(h, 0, c[0].replace("M", "L").replace("m", "l")), "string" === typeof b[h] && (c.length && d.push(c.slice(0)), c.length = 0), c.push(b[h]); d.push(c.slice(0)); return d
                    }; H.prototype.label = function (b, d, c, a, h, e, l, q, m) { return new I(this, b, d, c, a, h, e, l, q, m) }; H.prototype.alignElements = function () { this.alignedObjects.forEach(function (b) { return b.align() }) }; return H
        }(); d(B.prototype, {
            Element: y, SVG_NS: v,
            escapes: { "&": "&amp;", "<": "&lt;", ">": "&gt;", "'": "&#39;", '"': "&quot;" }, symbols: J, draw: t
        }); F.registerRendererType("svg", B, !0); ""; return B
    }); L(f, "Core/Renderer/HTML/HTMLElement.js", [f["Core/Globals.js"], f["Core/Renderer/SVG/SVGElement.js"], f["Core/Utilities.js"]], function (a, f, B) {
        var D = this && this.__extends || function () {
            var c = function (a, e) { c = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (c, a) { c.__proto__ = a } || function (c, a) { for (var e in a) a.hasOwnProperty(e) && (c[e] = a[e]) }; return c(a, e) }; return function (a,
                e) { function g() { this.constructor = a } c(a, e); a.prototype = null === e ? Object.create(e) : (g.prototype = e.prototype, new g) }
        }(), y = a.isFirefox, I = a.isMS, J = a.isWebKit, C = a.win, x = B.css, p = B.defined, g = B.extend, e = B.pick, c = B.pInt; return function (a) {
            function k() { return null !== a && a.apply(this, arguments) || this } D(k, a); k.compose = function (c) {
                if (-1 === k.composedClasses.indexOf(c)) {
                    k.composedClasses.push(c); var a = k.prototype, e = c.prototype; e.getSpanCorrection = a.getSpanCorrection; e.htmlCss = a.htmlCss; e.htmlGetBBox = a.htmlGetBBox;
                    e.htmlUpdateTransform = a.htmlUpdateTransform; e.setSpanRotation = a.setSpanRotation
                } return c
            }; k.prototype.getSpanCorrection = function (c, a, e) { this.xCorr = -c * e; this.yCorr = -a }; k.prototype.htmlCss = function (c) { var a = "SPAN" === this.element.tagName && c && "width" in c, k = e(a && c.width, void 0); if (a) { delete c.width; this.textWidth = k; var n = !0 } c && "ellipsis" === c.textOverflow && (c.whiteSpace = "nowrap", c.overflow = "hidden"); this.styles = g(this.styles, c); x(this.element, c); n && this.htmlUpdateTransform(); return this }; k.prototype.htmlGetBBox =
                function () { var c = this.element; return { x: c.offsetLeft, y: c.offsetTop, width: c.offsetWidth, height: c.offsetHeight } }; k.prototype.htmlUpdateTransform = function () {
                    if (this.added) {
                        var a = this.renderer, e = this.element, g = this.translateX || 0, k = this.translateY || 0, n = this.x || 0, f = this.y || 0, m = this.textAlign || "left", h = { left: 0, center: .5, right: 1 }[m], b = this.styles; b = b && b.whiteSpace; x(e, { marginLeft: g, marginTop: k }); !a.styledMode && this.shadows && this.shadows.forEach(function (b) { x(b, { marginLeft: g + 1, marginTop: k + 1 }) }); this.inverted &&
                            [].forEach.call(e.childNodes, function (b) { a.invertChild(b, e) }); if ("SPAN" === e.tagName) {
                                var l = this.rotation, d = this.textWidth && c(this.textWidth), G = [l, m, e.innerHTML, this.textWidth, this.textAlign].join(), z = void 0; z = !1; if (d !== this.oldTextWidth) {
                                    if (this.textPxLength) var q = this.textPxLength; else x(e, { width: "", whiteSpace: b || "nowrap" }), q = e.offsetWidth; (d > this.oldTextWidth || q > d) && (/[ \-]/.test(e.textContent || e.innerText) || "ellipsis" === e.style.textOverflow) && (x(e, {
                                        width: q > d || l ? d + "px" : "auto", display: "block", whiteSpace: b ||
                                            "normal"
                                    }), this.oldTextWidth = d, z = !0)
                                } this.hasBoxWidthChanged = z; G !== this.cTT && (z = a.fontMetrics(e.style.fontSize, e).b, !p(l) || l === (this.oldRotation || 0) && m === this.oldAlign || this.setSpanRotation(l, h, z), this.getSpanCorrection(!p(l) && this.textPxLength || e.offsetWidth, z, h, l, m)); x(e, { left: n + (this.xCorr || 0) + "px", top: f + (this.yCorr || 0) + "px" }); this.cTT = G; this.oldRotation = l; this.oldAlign = m
                            }
                    } else this.alignOnAdd = !0
                }; k.prototype.setSpanRotation = function (c, a, e) {
                    var g = {}, k = I && !/Edge/.test(C.navigator.userAgent) ? "-ms-transform" :
                        J ? "-webkit-transform" : y ? "MozTransform" : C.opera ? "-o-transform" : void 0; k && (g[k] = g.transform = "rotate(" + c + "deg)", g[k + (y ? "Origin" : "-origin")] = g.transformOrigin = 100 * a + "% " + e + "px", x(this.element, g))
                }; k.composedClasses = []; return k
        }(f)
    }); L(f, "Core/Renderer/HTML/HTMLRenderer.js", [f["Core/Renderer/HTML/AST.js"], f["Core/Renderer/SVG/SVGElement.js"], f["Core/Renderer/SVG/SVGRenderer.js"], f["Core/Utilities.js"]], function (a, f, B, F) {
        var D = this && this.__extends || function () {
            var a = function (g, e) {
                a = Object.setPrototypeOf ||
                { __proto__: [] } instanceof Array && function (c, a) { c.__proto__ = a } || function (c, a) { for (var e in a) a.hasOwnProperty(e) && (c[e] = a[e]) }; return a(g, e)
            }; return function (g, e) { function c() { this.constructor = g } a(g, e); g.prototype = null === e ? Object.create(e) : (c.prototype = e.prototype, new c) }
        }(), I = F.attr, J = F.createElement, C = F.extend, x = F.pick; return function (p) {
            function g() { return null !== p && p.apply(this, arguments) || this } D(g, p); g.compose = function (a) {
                -1 === g.composedClasses.indexOf(a) && (g.composedClasses.push(a), a.prototype.html =
                    g.prototype.html); return a
            }; g.prototype.html = function (e, c, g) {
                var k = this.createElement("span"), n = k.element, v = k.renderer, u = v.isSVG, w = function (c, a) { ["opacity", "visibility"].forEach(function (e) { c[e + "Setter"] = function (h, b, l) { var d = c.div ? c.div.style : a; f.prototype[e + "Setter"].call(this, h, b, l); d && (d[b] = h) } }); c.addedSetters = !0 }; k.textSetter = function (c) { c !== this.textStr && (delete this.bBox, delete this.oldTextWidth, a.setElementHTML(this.element, x(c, "")), this.textStr = c, k.doTransform = !0) }; u && w(k, k.element.style);
                k.xSetter = k.ySetter = k.alignSetter = k.rotationSetter = function (c, a) { "align" === a ? k.alignValue = k.textAlign = c : k[a] = c; k.doTransform = !0 }; k.afterSetters = function () { this.doTransform && (this.htmlUpdateTransform(), this.doTransform = !1) }; k.attr({ text: e, x: Math.round(c), y: Math.round(g) }).css({ position: "absolute" }); v.styledMode || k.css({ fontFamily: this.style.fontFamily, fontSize: this.style.fontSize }); n.style.whiteSpace = "nowrap"; k.css = k.htmlCss; u && (k.add = function (c) {
                    var a = v.box.parentNode, e = []; if (this.parentGroup = c) {
                        var h =
                            c.div; if (!h) {
                                for (; c;)e.push(c), c = c.parentGroup; e.reverse().forEach(function (b) {
                                    function c(d, c) { b[c] = d; "translateX" === c ? g.left = d + "px" : g.top = d + "px"; b.doTransform = !0 } var d = I(b.element, "class"), m = b.styles || {}; h = b.div = b.div || J("div", d ? { className: d } : void 0, { position: "absolute", left: (b.translateX || 0) + "px", top: (b.translateY || 0) + "px", display: b.display, opacity: b.opacity, cursor: m.cursor, pointerEvents: m.pointerEvents, visibility: b.visibility }, h || a); var g = h.style; C(b, {
                                        classSetter: function (b) {
                                            return function (d) {
                                                this.element.setAttribute("class",
                                                    d); b.className = d
                                            }
                                        }(h), on: function () { e[0].div && k.on.apply({ element: e[0].div, onEvents: b.onEvents }, arguments); return b }, translateXSetter: c, translateYSetter: c
                                    }); b.addedSetters || w(b)
                                })
                            }
                    } else h = a; h.appendChild(n); k.added = !0; k.alignOnAdd && k.htmlUpdateTransform(); return k
                }); return k
            }; g.composedClasses = []; return g
        }(B)
    }); L(f, "Core/Axis/AxisDefaults.js", [], function () {
        var a; (function (a) {
            a.defaultXAxisOptions = {
                alignTicks: !0, allowDecimals: void 0, panningEnabled: !0, zIndex: 2, zoomEnabled: !0, dateTimeLabelFormats: {
                    millisecond: {
                        main: "%H:%M:%S.%L",
                        range: !1
                    }, second: { main: "%H:%M:%S", range: !1 }, minute: { main: "%H:%M", range: !1 }, hour: { main: "%H:%M", range: !1 }, day: { main: "%e. %b" }, week: { main: "%e. %b" }, month: { main: "%b '%y" }, year: { main: "%Y" }
                }, endOnTick: !1, gridLineDashStyle: "Solid", gridZIndex: 1, labels: { autoRotation: void 0, autoRotationLimit: 80, distance: void 0, enabled: !0, indentation: 10, overflow: "justify", padding: 5, reserveSpace: void 0, rotation: void 0, staggerLines: 0, step: 0, useHTML: !1, x: 0, zIndex: 7, style: { color: "#666666", cursor: "default", fontSize: "11px" } }, maxPadding: .01,
                minorGridLineDashStyle: "Solid", minorTickLength: 2, minorTickPosition: "outside", minPadding: .01, offset: void 0, opposite: !1, reversed: void 0, reversedStacks: !1, showEmpty: !0, showFirstLabel: !0, showLastLabel: !0, startOfWeek: 1, startOnTick: !1, tickLength: 10, tickPixelInterval: 100, tickmarkPlacement: "between", tickPosition: "outside", title: { align: "middle", rotation: 0, useHTML: !1, x: 0, y: 0, style: { color: "#666666" } }, type: "linear", uniqueNames: !0, visible: !0, minorGridLineColor: "#f2f2f2", minorGridLineWidth: 1, minorTickColor: "#999999",
                lineColor: "#ccd6eb", lineWidth: 1, gridLineColor: "#e6e6e6", gridLineWidth: void 0, tickColor: "#ccd6eb"
            }; a.defaultYAxisOptions = {
                reversedStacks: !0, endOnTick: !0, maxPadding: .05, minPadding: .05, tickPixelInterval: 72, showLastLabel: !0, labels: { x: -8 }, startOnTick: !0, title: { rotation: 270, text: "Values" }, stackLabels: {
                    animation: {}, allowOverlap: !1, enabled: !1, crop: !0, overflow: "justify", formatter: function () { var a = this.axis.chart.numberFormatter; return a(this.total, -1) }, style: {
                        color: "#000000", fontSize: "11px", fontWeight: "bold",
                        textOutline: "1px contrast"
                    }
                }, gridLineWidth: 1, lineWidth: 0
            }; a.defaultLeftAxisOptions = { labels: { x: -15 }, title: { rotation: 270 } }; a.defaultRightAxisOptions = { labels: { x: 15 }, title: { rotation: 90 } }; a.defaultBottomAxisOptions = { labels: { autoRotation: [-45], x: 0 }, margin: 15, title: { rotation: 0 } }; a.defaultTopAxisOptions = { labels: { autoRotation: [-45], x: 0 }, margin: 15, title: { rotation: 0 } }
        })(a || (a = {})); return a
    }); L(f, "Core/Foundation.js", [f["Core/Utilities.js"]], function (a) {
        var f = a.addEvent, B = a.isFunction, F = a.objectEach, y = a.removeEvent,
        I; (function (a) { a.registerEventOptions = function (a, x) { a.eventOptions = a.eventOptions || {}; F(x.events, function (p, g) { a.eventOptions[g] !== p && (a.eventOptions[g] && (y(a, g, a.eventOptions[g]), delete a.eventOptions[g]), B(p) && (a.eventOptions[g] = p, f(a, g, p))) }) } })(I || (I = {})); return I
    }); L(f, "Core/Axis/Tick.js", [f["Core/FormatUtilities.js"], f["Core/Globals.js"], f["Core/Utilities.js"]], function (a, f, B) {
        var D = f.deg2rad, y = B.clamp, I = B.correctFloat, J = B.defined, C = B.destroyObjectProperties, x = B.extend, p = B.fireEvent, g = B.isNumber,
        e = B.merge, c = B.objectEach, n = B.pick; f = function () {
            function k(c, a, e, g, k) { this.isNewLabel = this.isNew = !0; this.axis = c; this.pos = a; this.type = e || ""; this.parameters = k || {}; this.tickmarkOffset = this.parameters.tickmarkOffset; this.options = this.parameters.options; p(this, "init"); e || g || this.addLabel() } k.prototype.addLabel = function () {
                var c = this, e = c.axis, k = e.options, f = e.chart, A = e.categories, r = e.logarithmic, m = e.names, h = c.pos, b = n(c.options && c.options.labels, k.labels), l = e.tickPositions, d = h === l[0], G = h === l[l.length - 1], z =
                    (!b.step || 1 === b.step) && 1 === e.tickInterval; l = l.info; var q = c.label, O; A = this.parameters.category || (A ? n(A[h], m[h], h) : h); r && g(A) && (A = I(r.lin2log(A))); if (e.dateTime) if (l) { var P = f.time.resolveDTLFormat(k.dateTimeLabelFormats[!k.grid && l.higherRanks[h] || l.unitName]); var S = P.main } else g(A) && (S = e.dateTime.getXDateFormat(A, k.dateTimeLabelFormats || {})); c.isFirst = d; c.isLast = G; var N = { axis: e, chart: f, dateTimeLabelFormat: S, isFirst: d, isLast: G, pos: h, tick: c, tickPositionInfo: l, value: A }; p(this, "labelFormat", N); var D = function (d) {
                        return b.formatter ?
                            b.formatter.call(d, d) : b.format ? (d.text = e.defaultLabelFormatter.call(d), a.format(b.format, d, f)) : e.defaultLabelFormatter.call(d, d)
                    }; k = D.call(N, N); var W = P && P.list; c.shortenLabel = W ? function () { for (O = 0; O < W.length; O++)if (x(N, { dateTimeLabelFormat: W[O] }), q.attr({ text: D.call(N, N) }), q.getBBox().width < e.getSlotWidth(c) - 2 * b.padding) return; q.attr({ text: "" }) } : void 0; z && e._addedPlotLB && c.moveLabel(k, b); J(q) || c.movedLabel ? q && q.textStr !== k && !z && (!q.textWidth || b.style.width || q.styles.width || q.css({ width: null }), q.attr({ text: k }),
                        q.textPxLength = q.getBBox().width) : (c.label = q = c.createLabel({ x: 0, y: 0 }, k, b), c.rotation = 0)
            }; k.prototype.createLabel = function (c, a, g) { var k = this.axis, f = k.chart; if (c = J(a) && g.enabled ? f.renderer.text(a, c.x, c.y, g.useHTML).add(k.labelGroup) : null) f.styledMode || c.css(e(g.style)), c.textPxLength = c.getBBox().width; return c }; k.prototype.destroy = function () { C(this, this.axis) }; k.prototype.getPosition = function (c, a, e, g) {
                var k = this.axis, f = k.chart, m = g && f.oldChartHeight || f.chartHeight; c = {
                    x: c ? I(k.translate(a + e, null, null,
                        g) + k.transB) : k.left + k.offset + (k.opposite ? (g && f.oldChartWidth || f.chartWidth) - k.right - k.left : 0), y: c ? m - k.bottom + k.offset - (k.opposite ? k.height : 0) : I(m - k.translate(a + e, null, null, g) - k.transB)
                }; c.y = y(c.y, -1E5, 1E5); p(this, "afterGetPosition", { pos: c }); return c
            }; k.prototype.getLabelPosition = function (c, a, e, g, k, f, m, h) {
                var b = this.axis, l = b.transA, d = b.isLinked && b.linkedParent ? b.linkedParent.reversed : b.reversed, G = b.staggerLines, z = b.tickRotCorr || { x: 0, y: 0 }, q = g || b.reserveSpaceDefault ? 0 : -b.labelOffset * ("center" === b.labelAlign ?
                    .5 : 1), n = {}, r = k.y; J(r) || (r = 0 === b.side ? e.rotation ? -8 : -e.getBBox().height : 2 === b.side ? z.y + 8 : Math.cos(e.rotation * D) * (z.y - e.getBBox(!1, 0).height / 2)); c = c + k.x + q + z.x - (f && g ? f * l * (d ? -1 : 1) : 0); a = a + r - (f && !g ? f * l * (d ? 1 : -1) : 0); G && (e = m / (h || 1) % G, b.opposite && (e = G - e - 1), a += b.labelOffset / G * e); n.x = c; n.y = Math.round(a); p(this, "afterGetLabelPosition", { pos: n, tickmarkOffset: f, index: m }); return n
            }; k.prototype.getLabelSize = function () { return this.label ? this.label.getBBox()[this.axis.horiz ? "height" : "width"] : 0 }; k.prototype.getMarkPath =
                function (c, a, e, g, k, f) { return f.crispLine([["M", c, a], ["L", c + (k ? 0 : -e), a + (k ? e : 0)]], g) }; k.prototype.handleOverflow = function (c) {
                    var a = this.axis, e = a.options.labels, g = c.x, k = a.chart.chartWidth, f = a.chart.spacing, m = n(a.labelLeft, Math.min(a.pos, f[3])); f = n(a.labelRight, Math.max(a.isRadial ? 0 : a.pos + a.len, k - f[1])); var h = this.label, b = this.rotation, l = { left: 0, center: .5, right: 1 }[a.labelAlign || h.attr("align")], d = h.getBBox().width, G = a.getSlotWidth(this), z = {}, q = G, t = 1, p; if (b || "justify" !== e.overflow) 0 > b && g - l * d < m ? p = Math.round(g /
                        Math.cos(b * D) - m) : 0 < b && g + l * d > f && (p = Math.round((k - g) / Math.cos(b * D))); else if (k = g + (1 - l) * d, g - l * d < m ? q = c.x + q * (1 - l) - m : k > f && (q = f - c.x + q * l, t = -1), q = Math.min(G, q), q < G && "center" === a.labelAlign && (c.x += t * (G - q - l * (G - Math.min(d, q)))), d > q || a.autoRotation && (h.styles || {}).width) p = q; p && (this.shortenLabel ? this.shortenLabel() : (z.width = Math.floor(p) + "px", (e.style || {}).textOverflow || (z.textOverflow = "ellipsis"), h.css(z)))
                }; k.prototype.moveLabel = function (a, e) {
                    var g = this, k = g.label, f = g.axis, n = f.reversed, m = !1; k && k.textStr === a ?
                        (g.movedLabel = k, m = !0, delete g.label) : c(f.ticks, function (b) { m || b.isNew || b === g || !b.label || b.label.textStr !== a || (g.movedLabel = b.label, m = !0, b.labelPos = g.movedLabel.xy, delete b.label) }); if (!m && (g.labelPos || k)) { var h = g.labelPos || k.xy; k = f.horiz ? n ? 0 : f.width + f.left : h.x; f = f.horiz ? h.y : n ? f.width + f.left : 0; g.movedLabel = g.createLabel({ x: k, y: f }, a, e); g.movedLabel && g.movedLabel.attr({ opacity: 0 }) }
                }; k.prototype.render = function (c, a, e) {
                    var g = this.axis, k = g.horiz, f = this.pos, m = n(this.tickmarkOffset, g.tickmarkOffset); f = this.getPosition(k,
                        f, m, a); m = f.x; var h = f.y; g = k && m === g.pos + g.len || !k && h === g.pos ? -1 : 1; k = n(e, this.label && this.label.newOpacity, 1); e = n(e, 1); this.isActive = !0; this.renderGridLine(a, e, g); this.renderMark(f, e, g); this.renderLabel(f, a, k, c); this.isNew = !1; p(this, "afterRender")
                }; k.prototype.renderGridLine = function (c, a, e) {
                    var g = this.axis, k = g.options, f = {}, m = this.pos, h = this.type, b = n(this.tickmarkOffset, g.tickmarkOffset), l = g.chart.renderer, d = this.gridLine, G = k.gridLineWidth, z = k.gridLineColor, q = k.gridLineDashStyle; "minor" === this.type &&
                        (G = k.minorGridLineWidth, z = k.minorGridLineColor, q = k.minorGridLineDashStyle); d || (g.chart.styledMode || (f.stroke = z, f["stroke-width"] = G || 0, f.dashstyle = q), h || (f.zIndex = 1), c && (a = 0), this.gridLine = d = l.path().attr(f).addClass("highcharts-" + (h ? h + "-" : "") + "grid-line").add(g.gridGroup)); if (d && (e = g.getPlotLinePath({ value: m + b, lineWidth: d.strokeWidth() * e, force: "pass", old: c }))) d[c || this.isNew ? "attr" : "animate"]({ d: e, opacity: a })
                }; k.prototype.renderMark = function (c, a, e) {
                    var g = this.axis, k = g.options, f = g.chart.renderer, m =
                        this.type, h = g.tickSize(m ? m + "Tick" : "tick"), b = c.x; c = c.y; var l = n(k["minor" !== m ? "tickWidth" : "minorTickWidth"], !m && g.isXAxis ? 1 : 0); k = k["minor" !== m ? "tickColor" : "minorTickColor"]; var d = this.mark, G = !d; h && (g.opposite && (h[0] = -h[0]), d || (this.mark = d = f.path().addClass("highcharts-" + (m ? m + "-" : "") + "tick").add(g.axisGroup), g.chart.styledMode || d.attr({ stroke: k, "stroke-width": l })), d[G ? "attr" : "animate"]({ d: this.getMarkPath(b, c, h[0], d.strokeWidth() * e, g.horiz, f), opacity: a }))
                }; k.prototype.renderLabel = function (c, a, e, k) {
                    var f =
                        this.axis, r = f.horiz, m = f.options, h = this.label, b = m.labels, l = b.step; f = n(this.tickmarkOffset, f.tickmarkOffset); var d = c.x; c = c.y; var G = !0; h && g(d) && (h.xy = c = this.getLabelPosition(d, c, h, r, b, f, k, l), this.isFirst && !this.isLast && !m.showFirstLabel || this.isLast && !this.isFirst && !m.showLastLabel ? G = !1 : !r || b.step || b.rotation || a || 0 === e || this.handleOverflow(c), l && k % l && (G = !1), G && g(c.y) ? (c.opacity = e, h[this.isNewLabel ? "attr" : "animate"](c).show(!0), this.isNewLabel = !1) : (h.hide(), this.isNewLabel = !0))
                }; k.prototype.replaceMovedLabel =
                    function () { var c = this.label, a = this.axis, e = a.reversed; if (c && !this.isNew) { var g = a.horiz ? e ? a.left : a.width + a.left : c.xy.x; e = a.horiz ? c.xy.y : e ? a.width + a.top : a.top; c.animate({ x: g, y: e, opacity: 0 }, void 0, c.destroy); delete this.label } a.isDirty = !0; this.label = this.movedLabel; delete this.movedLabel }; return k
        }(); ""; return f
    }); L(f, "Core/Axis/Axis.js", [f["Core/Animation/AnimationUtilities.js"], f["Core/Axis/AxisDefaults.js"], f["Core/Color/Color.js"], f["Core/DefaultOptions.js"], f["Core/Foundation.js"], f["Core/Globals.js"],
    f["Core/Axis/Tick.js"], f["Core/Utilities.js"]], function (a, f, B, F, y, I, J, C) {
        var x = a.animObject, p = F.defaultOptions, g = y.registerEventOptions, e = I.deg2rad, c = C.arrayMax, n = C.arrayMin, k = C.clamp, t = C.correctFloat, v = C.defined, u = C.destroyObjectProperties, w = C.erase, A = C.error, r = C.extend, m = C.fireEvent, h = C.isArray, b = C.isNumber, l = C.isString, d = C.merge, G = C.normalizeTickInterval, z = C.objectEach, q = C.pick, O = C.relativeLength, P = C.removeEvent, S = C.splat, N = C.syncTimeout, D = function (b, d) {
            return G(d, void 0, void 0, q(b.options.allowDecimals,
                .5 > d || void 0 !== b.tickAmount), !!b.tickAmount)
        }; a = function () {
            function a(b, d) {
                this.zoomEnabled = this.width = this.visible = this.userOptions = this.translationSlope = this.transB = this.transA = this.top = this.ticks = this.tickRotCorr = this.tickPositions = this.tickmarkOffset = this.tickInterval = this.tickAmount = this.side = this.series = this.right = this.positiveValuesOnly = this.pos = this.pointRangePadding = this.pointRange = this.plotLinesAndBandsGroups = this.plotLinesAndBands = this.paddedTicks = this.overlap = this.options = this.offset =
                    this.names = this.minPixelPadding = this.minorTicks = this.minorTickInterval = this.min = this.maxLabelLength = this.max = this.len = this.left = this.labelFormatter = this.labelEdge = this.isLinked = this.height = this.hasVisibleSeries = this.hasNames = this.eventOptions = this.coll = this.closestPointRange = this.chart = this.bottom = this.alternateBands = void 0; this.init(b, d)
            } a.prototype.init = function (d, c) {
                var a = c.isX; this.chart = d; this.horiz = d.inverted && !this.isZAxis ? !a : a; this.isXAxis = a; this.coll = this.coll || (a ? "xAxis" : "yAxis"); m(this,
                    "init", { userOptions: c }); this.opposite = q(c.opposite, this.opposite); this.side = q(c.side, this.side, this.horiz ? this.opposite ? 0 : 2 : this.opposite ? 1 : 3); this.setOptions(c); var e = this.options, h = e.labels, l = e.type; this.userOptions = c; this.minPixelPadding = 0; this.reversed = q(e.reversed, this.reversed); this.visible = e.visible; this.zoomEnabled = e.zoomEnabled; this.hasNames = "category" === l || !0 === e.categories; this.categories = e.categories || (this.hasNames ? [] : void 0); this.names || (this.names = [], this.names.keys = {}); this.plotLinesAndBandsGroups =
                        {}; this.positiveValuesOnly = !!this.logarithmic; this.isLinked = v(e.linkedTo); this.ticks = {}; this.labelEdge = []; this.minorTicks = {}; this.plotLinesAndBands = []; this.alternateBands = {}; this.len = 0; this.minRange = this.userMinRange = e.minRange || e.maxZoom; this.range = e.range; this.offset = e.offset || 0; this.min = this.max = null; c = q(e.crosshair, S(d.options.tooltip.crosshairs)[a ? 0 : 1]); this.crosshair = !0 === c ? {} : c; -1 === d.axes.indexOf(this) && (a ? d.axes.splice(d.xAxis.length, 0, this) : d.axes.push(this), d[this.coll].push(this)); this.series =
                            this.series || []; d.inverted && !this.isZAxis && a && "undefined" === typeof this.reversed && (this.reversed = !0); this.labelRotation = b(h.rotation) ? h.rotation : void 0; g(this, e); m(this, "afterInit")
            }; a.prototype.setOptions = function (b) { this.options = d(f.defaultXAxisOptions, "yAxis" === this.coll && f.defaultYAxisOptions, [f.defaultTopAxisOptions, f.defaultRightAxisOptions, f.defaultBottomAxisOptions, f.defaultLeftAxisOptions][this.side], d(p[this.coll], b)); m(this, "afterSetOptions", { userOptions: b }) }; a.prototype.defaultLabelFormatter =
                function (d) {
                    var c = this.axis; d = this.chart.numberFormatter; var a = b(this.value) ? this.value : NaN, e = c.chart.time, h = this.dateTimeLabelFormat, l = p.lang, g = l.numericSymbols; l = l.numericSymbolMagnitude || 1E3; var q = c.logarithmic ? Math.abs(a) : c.tickInterval, m = g && g.length; if (c.categories) var H = "".concat(this.value); else if (h) H = e.dateFormat(h, a); else if (m && 1E3 <= q) for (; m-- && "undefined" === typeof H;)c = Math.pow(l, m + 1), q >= c && 0 === 10 * a % c && null !== g[m] && 0 !== a && (H = d(a / c, -1) + g[m]); "undefined" === typeof H && (H = 1E4 <= Math.abs(a) ? d(a,
                        -1) : d(a, -1, void 0, "")); return H
                }; a.prototype.getSeriesExtremes = function () {
                    var d = this, c = d.chart, a; m(this, "getSeriesExtremes", null, function () {
                        d.hasVisibleSeries = !1; d.dataMin = d.dataMax = d.threshold = null; d.softThreshold = !d.isXAxis; d.stacking && d.stacking.buildStacks(); d.series.forEach(function (e) {
                            if (e.visible || !c.options.chart.ignoreHiddenSeries) {
                                var h = e.options, l = h.threshold; d.hasVisibleSeries = !0; d.positiveValuesOnly && 0 >= l && (l = null); if (d.isXAxis) {
                                    if (h = e.xData, h.length) {
                                        h = d.logarithmic ? h.filter(d.validatePositiveValue) :
                                            h; a = e.getXExtremes(h); var g = a.min; var m = a.max; b(g) || g instanceof Date || (h = h.filter(b), a = e.getXExtremes(h), g = a.min, m = a.max); h.length && (d.dataMin = Math.min(q(d.dataMin, g), g), d.dataMax = Math.max(q(d.dataMax, m), m))
                                    }
                                } else if (e = e.applyExtremes(), b(e.dataMin) && (g = e.dataMin, d.dataMin = Math.min(q(d.dataMin, g), g)), b(e.dataMax) && (m = e.dataMax, d.dataMax = Math.max(q(d.dataMax, m), m)), v(l) && (d.threshold = l), !h.softThreshold || d.positiveValuesOnly) d.softThreshold = !1
                            }
                        })
                    }); m(this, "afterGetSeriesExtremes")
                }; a.prototype.translate =
                    function (d, c, a, e, h, l) { var g = this.linkedParent || this, m = e && g.old ? g.old.min : g.min, q = g.minPixelPadding; h = (g.isOrdinal || g.brokenAxis && g.brokenAxis.hasBreaks || g.logarithmic && h) && g.lin2val; var k = 1, H = 0; e = e && g.old ? g.old.transA : g.transA; e || (e = g.transA); a && (k *= -1, H = g.len); g.reversed && (k *= -1, H -= k * (g.sector || g.len)); c ? (l = (d * k + H - q) / e + m, h && (l = g.lin2val(l))) : (h && (d = g.val2lin(d)), d = k * (d - m) * e, l = b(m) ? (g.isRadial ? d : t(d)) + H + k * q + (b(l) ? e * l : 0) : void 0); return l }; a.prototype.toPixels = function (b, d) {
                        return this.translate(b,
                            !1, !this.horiz, null, !0) + (d ? 0 : this.pos)
                    }; a.prototype.toValue = function (b, d) { return this.translate(b - (d ? 0 : this.pos), !0, !this.horiz, null, !0) }; a.prototype.getPlotLinePath = function (d) {
                        function c(b, d, c) { if ("pass" !== t && b < d || b > c) t ? b = k(b, d, c) : P = !0; return b } var a = this, e = a.chart, h = a.left, l = a.top, g = d.old, H = d.value, f = d.lineWidth, z = g && e.oldChartHeight || e.chartHeight, G = g && e.oldChartWidth || e.chartWidth, n = a.transB, r = d.translatedValue, t = d.force, v, p, w, O, P; d = {
                            value: H, lineWidth: f, old: g, force: t, acrossPanes: d.acrossPanes,
                            translatedValue: r
                        }; m(this, "getPlotLinePath", d, function (d) { r = q(r, a.translate(H, null, null, g)); r = k(r, -1E5, 1E5); v = w = Math.round(r + n); p = O = Math.round(z - r - n); b(r) ? a.horiz ? (p = l, O = z - a.bottom, v = w = c(v, h, h + a.width)) : (v = h, w = G - a.right, p = O = c(p, l, l + a.height)) : (P = !0, t = !1); d.path = P && !t ? null : e.renderer.crispLine([["M", v, p], ["L", w, O]], f || 1) }); return d.path
                    }; a.prototype.getLinearTickPositions = function (b, d, c) {
                        var a = t(Math.floor(d / b) * b); c = t(Math.ceil(c / b) * b); var e = [], h; t(a + b) === a && (h = 20); if (this.single) return [d]; for (d = a; d <=
                            c;) { e.push(d); d = t(d + b, h); if (d === l) break; var l = d } return e
                    }; a.prototype.getMinorTickInterval = function () { var b = this.options; return !0 === b.minorTicks ? q(b.minorTickInterval, "auto") : !1 === b.minorTicks ? null : b.minorTickInterval }; a.prototype.getMinorTickPositions = function () {
                        var b = this.options, d = this.tickPositions, c = this.minorTickInterval, a = this.pointRangePadding || 0, e = this.min - a; a = this.max + a; var h = a - e, l = []; if (h && h / c < this.len / 3) {
                            var g = this.logarithmic; if (g) this.paddedTicks.forEach(function (b, d, a) {
                                d && l.push.apply(l,
                                    g.getLogTickPositions(c, a[d - 1], a[d], !0))
                            }); else if (this.dateTime && "auto" === this.getMinorTickInterval()) l = l.concat(this.getTimeTicks(this.dateTime.normalizeTimeTickInterval(c), e, a, b.startOfWeek)); else for (b = e + (d[0] - e) % c; b <= a && b !== l[0]; b += c)l.push(b)
                        } 0 !== l.length && this.trimTicks(l); return l
                    }; a.prototype.adjustForMinRange = function () {
                        var b = this.options, d = this.logarithmic, a = this.min, e = this.max, h = 0, l, g, m, k; this.isXAxis && "undefined" === typeof this.minRange && !d && (v(b.min) || v(b.max) || v(b.floor) || v(b.ceiling) ?
                            this.minRange = null : (this.series.forEach(function (b) { m = b.xData; k = b.xIncrement ? 1 : m.length - 1; if (1 < m.length) for (l = k; 0 < l; l--)if (g = m[l] - m[l - 1], !h || g < h) h = g }), this.minRange = Math.min(5 * h, this.dataMax - this.dataMin))); if (e - a < this.minRange) {
                                var f = this.dataMax - this.dataMin >= this.minRange; var z = this.minRange; var G = (z - e + a) / 2; G = [a - G, q(b.min, a - G)]; f && (G[2] = this.logarithmic ? this.logarithmic.log2lin(this.dataMin) : this.dataMin); a = c(G); e = [a + z, q(b.max, a + z)]; f && (e[2] = d ? d.log2lin(this.dataMax) : this.dataMax); e = n(e); e - a < z &&
                                    (G[0] = e - z, G[1] = q(b.min, e - z), a = c(G))
                            } this.min = a; this.max = e
                    }; a.prototype.getClosest = function () { var b; this.categories ? b = 1 : this.series.forEach(function (d) { var c = d.closestPointRange, a = d.visible || !d.chart.options.chart.ignoreHiddenSeries; !d.noSharedTooltip && v(c) && a && (b = v(b) ? Math.min(b, c) : c) }); return b }; a.prototype.nameToX = function (b) {
                        var d = h(this.options.categories), c = d ? this.categories : this.names, a = b.options.x; b.series.requireSorting = !1; v(a) || (a = this.options.uniqueNames && c ? d ? c.indexOf(b.name) : q(c.keys[b.name],
                            -1) : b.series.autoIncrement()); if (-1 === a) { if (!d && c) var e = c.length } else e = a; "undefined" !== typeof e && (this.names[e] = b.name, this.names.keys[b.name] = e); return e
                    }; a.prototype.updateNames = function () {
                        var b = this, d = this.names; 0 < d.length && (Object.keys(d.keys).forEach(function (b) { delete d.keys[b] }), d.length = 0, this.minRange = this.userMinRange, (this.series || []).forEach(function (d) {
                            d.xIncrement = null; if (!d.points || d.isDirtyData) b.max = Math.max(b.max, d.xData.length - 1), d.processData(), d.generatePoints(); d.data.forEach(function (c,
                                a) { if (c && c.options && "undefined" !== typeof c.name) { var e = b.nameToX(c); "undefined" !== typeof e && e !== c.x && (c.x = e, d.xData[a] = e) } })
                        }))
                    }; a.prototype.setAxisTranslation = function () {
                        var b = this, d = b.max - b.min, c = b.linkedParent, a = !!b.categories, e = b.isXAxis, h = b.axisPointRange || 0, g = 0, k = 0, f = b.transA; if (e || a || h) {
                            var z = b.getClosest(); c ? (g = c.minPointOffset, k = c.pointRangePadding) : b.series.forEach(function (d) {
                                var c = a ? 1 : e ? q(d.options.pointRange, z, 0) : b.axisPointRange || 0, m = d.options.pointPlacement; h = Math.max(h, c); if (!b.single ||
                                    a) d = d.is("xrange") ? !e : e, g = Math.max(g, d && l(m) ? 0 : c / 2), k = Math.max(k, d && "on" === m ? 0 : c)
                            }); c = b.ordinal && b.ordinal.slope && z ? b.ordinal.slope / z : 1; b.minPointOffset = g *= c; b.pointRangePadding = k *= c; b.pointRange = Math.min(h, b.single && a ? 1 : d); e && (b.closestPointRange = z)
                        } b.translationSlope = b.transA = f = b.staticScale || b.len / (d + k || 1); b.transB = b.horiz ? b.left : b.bottom; b.minPixelPadding = f * g; m(this, "afterSetAxisTranslation")
                    }; a.prototype.minFromRange = function () { return this.max - this.range }; a.prototype.setTickInterval = function (d) {
                        var c =
                            this.chart, a = this.logarithmic, e = this.options, h = this.isXAxis, l = this.isLinked, g = e.tickPixelInterval, k = this.categories, f = this.softThreshold, z = e.maxPadding, G = e.minPadding, n = b(e.tickInterval) && 0 <= e.tickInterval ? e.tickInterval : void 0, H = b(this.threshold) ? this.threshold : null; this.dateTime || k || l || this.getTickAmount(); var r = q(this.userMin, e.min); var p = q(this.userMax, e.max); if (l) {
                                this.linkedParent = c[this.coll][e.linkedTo]; var w = this.linkedParent.getExtremes(); this.min = q(w.min, w.dataMin); this.max = q(w.max, w.dataMax);
                                e.type !== this.linkedParent.options.type && A(11, 1, c)
                            } else { if (f && v(H)) if (this.dataMin >= H) w = H, G = 0; else if (this.dataMax <= H) { var O = H; z = 0 } this.min = q(r, w, this.dataMin); this.max = q(p, O, this.dataMax) } a && (this.positiveValuesOnly && !d && 0 >= Math.min(this.min, q(this.dataMin, this.min)) && A(10, 1, c), this.min = t(a.log2lin(this.min), 16), this.max = t(a.log2lin(this.max), 16)); this.range && v(this.max) && (this.userMin = this.min = r = Math.max(this.dataMin, this.minFromRange()), this.userMax = p = this.max, this.range = null); m(this, "foundExtremes");
                        this.beforePadding && this.beforePadding(); this.adjustForMinRange(); !(k || this.axisPointRange || this.stacking && this.stacking.usePercentage || l) && v(this.min) && v(this.max) && (c = this.max - this.min) && (!v(r) && G && (this.min -= c * G), !v(p) && z && (this.max += c * z)); b(this.userMin) || (b(e.softMin) && e.softMin < this.min && (this.min = r = e.softMin), b(e.floor) && (this.min = Math.max(this.min, e.floor))); b(this.userMax) || (b(e.softMax) && e.softMax > this.max && (this.max = p = e.softMax), b(e.ceiling) && (this.max = Math.min(this.max, e.ceiling))); f &&
                            v(this.dataMin) && (H = H || 0, !v(r) && this.min < H && this.dataMin >= H ? this.min = this.options.minRange ? Math.min(H, this.max - this.minRange) : H : !v(p) && this.max > H && this.dataMax <= H && (this.max = this.options.minRange ? Math.max(H, this.min + this.minRange) : H)); b(this.min) && b(this.max) && !this.chart.polar && this.min > this.max && (v(this.options.min) ? this.max = this.min : v(this.options.max) && (this.min = this.max)); this.tickInterval = this.min === this.max || "undefined" === typeof this.min || "undefined" === typeof this.max ? 1 : l && this.linkedParent &&
                                !n && g === this.linkedParent.options.tickPixelInterval ? n = this.linkedParent.tickInterval : q(n, this.tickAmount ? (this.max - this.min) / Math.max(this.tickAmount - 1, 1) : void 0, k ? 1 : (this.max - this.min) * g / Math.max(this.len, g)); if (h && !d) { var P = this.min !== (this.old && this.old.min) || this.max !== (this.old && this.old.max); this.series.forEach(function (b) { b.forceCrop = b.forceCropping && b.forceCropping(); b.processData(P) }); m(this, "postProcessData", { hasExtemesChanged: P }) } this.setAxisTranslation(); m(this, "initialAxisTranslation");
                        this.pointRange && !n && (this.tickInterval = Math.max(this.pointRange, this.tickInterval)); d = q(e.minTickInterval, this.dateTime && !this.series.some(function (b) { return b.noSharedTooltip }) ? this.closestPointRange : 0); !n && this.tickInterval < d && (this.tickInterval = d); this.dateTime || this.logarithmic || n || (this.tickInterval = D(this, this.tickInterval)); this.tickAmount || (this.tickInterval = this.unsquish()); this.setTickPositions()
                    }; a.prototype.setTickPositions = function () {
                        var b = this.options, d = b.tickPositions, c = this.getMinorTickInterval(),
                        a = this.hasVerticalPanning(), e = "colorAxis" === this.coll, h = (e || !a) && b.startOnTick; a = (e || !a) && b.endOnTick; e = b.tickPositioner; this.tickmarkOffset = this.categories && "between" === b.tickmarkPlacement && 1 === this.tickInterval ? .5 : 0; this.minorTickInterval = "auto" === c && this.tickInterval ? this.tickInterval / 5 : c; this.single = this.min === this.max && v(this.min) && !this.tickAmount && (parseInt(this.min, 10) === this.min || !1 !== b.allowDecimals); this.tickPositions = c = d && d.slice(); if (!c) {
                            if (this.ordinal && this.ordinal.positions || !((this.max -
                                this.min) / this.tickInterval > Math.max(2 * this.len, 200))) if (this.dateTime) c = this.getTimeTicks(this.dateTime.normalizeTimeTickInterval(this.tickInterval, b.units), this.min, this.max, b.startOfWeek, this.ordinal && this.ordinal.positions, this.closestPointRange, !0); else if (this.logarithmic) c = this.logarithmic.getLogTickPositions(this.tickInterval, this.min, this.max); else for (var l = b = this.tickInterval; l <= 2 * b;)if (c = this.getLinearTickPositions(this.tickInterval, this.min, this.max), this.tickAmount && c.length > this.tickAmount) this.tickInterval =
                                    D(this, l *= 1.1); else break; else c = [this.min, this.max], A(19, !1, this.chart); c.length > this.len && (c = [c[0], c.pop()], c[0] === c[1] && (c.length = 1)); this.tickPositions = c; e && (e = e.apply(this, [this.min, this.max])) && (this.tickPositions = c = e)
                        } this.paddedTicks = c.slice(0); this.trimTicks(c, h, a); this.isLinked || (this.single && 2 > c.length && !this.categories && !this.series.some(function (b) { return b.is("heatmap") && "between" === b.options.pointPlacement }) && (this.min -= .5, this.max += .5), d || e || this.adjustTickAmount()); m(this, "afterSetTickPositions")
                    };
            a.prototype.trimTicks = function (b, d, c) { var a = b[0], e = b[b.length - 1], h = !this.isOrdinal && this.minPointOffset || 0; m(this, "trimTicks"); if (!this.isLinked) { if (d && -Infinity !== a) this.min = a; else for (; this.min - h > b[0];)b.shift(); if (c) this.max = e; else for (; this.max + h < b[b.length - 1];)b.pop(); 0 === b.length && v(a) && !this.options.tickPositions && b.push((e + a) / 2) } }; a.prototype.alignToOthers = function () {
                var d = this, c = [this], a = d.options, e = "yAxis" === this.coll && this.chart.options.chart.alignThresholds, h = [], l; d.thresholdAlignment =
                    void 0; if ((!1 !== this.chart.options.chart.alignTicks && a.alignTicks || e) && !1 !== a.startOnTick && !1 !== a.endOnTick && !d.logarithmic) { var g = function (b) { var d = b.options; return [b.horiz ? d.left : d.top, d.width, d.height, d.pane].join() }, m = g(this); this.chart[this.coll].forEach(function (b) { var a = b.series; a.length && a.some(function (b) { return b.visible }) && b !== d && g(b) === m && (l = !0, c.push(b)) }) } if (l && e) {
                        c.forEach(function (c) { c = c.getThresholdAlignment(d); b(c) && h.push(c) }); var q = 1 < h.length ? h.reduce(function (b, d) { return b + d },
                            0) / h.length : void 0; c.forEach(function (b) { b.thresholdAlignment = q })
                    } return l
            }; a.prototype.getThresholdAlignment = function (d) { (!b(this.dataMin) || this !== d && this.series.some(function (b) { return b.isDirty || b.isDirtyData })) && this.getSeriesExtremes(); if (b(this.threshold)) return d = k((this.threshold - (this.dataMin || 0)) / ((this.dataMax || 0) - (this.dataMin || 0)), 0, 1), this.options.reversed && (d = 1 - d), d }; a.prototype.getTickAmount = function () {
                var b = this.options, d = b.tickPixelInterval, c = b.tickAmount; !v(b.tickInterval) && !c &&
                    this.len < d && !this.isRadial && !this.logarithmic && b.startOnTick && b.endOnTick && (c = 2); !c && this.alignToOthers() && (c = Math.ceil(this.len / d) + 1); 4 > c && (this.finalTickAmt = c, c = 5); this.tickAmount = c
            }; a.prototype.adjustTickAmount = function () {
                var d = this, c = d.finalTickAmt, a = d.max, e = d.min, h = d.options, l = d.tickPositions, g = d.tickAmount, m = d.thresholdAlignment, k = l && l.length, f = q(d.threshold, d.softThreshold ? 0 : null); var z = d.tickInterval; if (b(m)) { var G = .5 > m ? Math.ceil(m * (g - 1)) : Math.floor(m * (g - 1)); h.reversed && (G = g - 1 - G) } if (d.hasData() &&
                    b(e) && b(a)) {
                        m = function () { d.transA *= (k - 1) / (g - 1); d.min = h.startOnTick ? l[0] : Math.min(e, l[0]); d.max = h.endOnTick ? l[l.length - 1] : Math.max(a, l[l.length - 1]) }; if (b(G) && b(d.threshold)) { for (; l[G] !== f || l.length !== g || l[0] > e || l[l.length - 1] < a;) { l.length = 0; for (l.push(d.threshold); l.length < g;)void 0 === l[G] || l[G] > d.threshold ? l.unshift(t(l[0] - z)) : l.push(t(l[l.length - 1] + z)); if (z > 8 * d.tickInterval) break; z *= 2 } m() } else if (k < g) { for (; l.length < g;)l.length % 2 || e === f ? l.push(t(l[l.length - 1] + z)) : l.unshift(t(l[0] - z)); m() } if (v(c)) {
                            for (z =
                                f = l.length; z--;)(3 === c && 1 === z % 2 || 2 >= c && 0 < z && z < f - 1) && l.splice(z, 1); d.finalTickAmt = void 0
                        }
                }
            }; a.prototype.setScale = function () {
                var b = !1, d = !1; this.series.forEach(function (c) { b = b || c.isDirtyData || c.isDirty; d = d || c.xAxis && c.xAxis.isDirty || !1 }); this.setAxisSize(); var c = this.len !== (this.old && this.old.len); c || b || d || this.isLinked || this.forceRedraw || this.userMin !== (this.old && this.old.userMin) || this.userMax !== (this.old && this.old.userMax) || this.alignToOthers() ? (this.stacking && this.stacking.resetStacks(), this.forceRedraw =
                    !1, this.getSeriesExtremes(), this.setTickInterval(), this.isDirty || (this.isDirty = c || this.min !== (this.old && this.old.min) || this.max !== (this.old && this.old.max))) : this.stacking && this.stacking.cleanStacks(); b && this.panningState && (this.panningState.isDirty = !0); m(this, "afterSetScale")
            }; a.prototype.setExtremes = function (b, d, c, a, e) { var h = this, l = h.chart; c = q(c, !0); h.series.forEach(function (b) { delete b.kdTree }); e = r(e, { min: b, max: d }); m(h, "setExtremes", e, function () { h.userMin = b; h.userMax = d; h.eventArgs = e; c && l.redraw(a) }) };
            a.prototype.zoom = function (b, d) { var c = this, a = this.dataMin, e = this.dataMax, h = this.options, l = Math.min(a, q(h.min, a)), g = Math.max(e, q(h.max, e)); b = { newMin: b, newMax: d }; m(this, "zoom", b, function (b) { var d = b.newMin, h = b.newMax; if (d !== c.min || h !== c.max) c.allowZoomOutside || (v(a) && (d < l && (d = l), d > g && (d = g)), v(e) && (h < l && (h = l), h > g && (h = g))), c.displayBtn = "undefined" !== typeof d || "undefined" !== typeof h, c.setExtremes(d, h, !1, void 0, { trigger: "zoom" }); b.zoomed = !0 }); return b.zoomed }; a.prototype.setAxisSize = function () {
                var b = this.chart,
                d = this.options, c = d.offsets || [0, 0, 0, 0], a = this.horiz, e = this.width = Math.round(O(q(d.width, b.plotWidth - c[3] + c[1]), b.plotWidth)), h = this.height = Math.round(O(q(d.height, b.plotHeight - c[0] + c[2]), b.plotHeight)), l = this.top = Math.round(O(q(d.top, b.plotTop + c[0]), b.plotHeight, b.plotTop)); d = this.left = Math.round(O(q(d.left, b.plotLeft + c[3]), b.plotWidth, b.plotLeft)); this.bottom = b.chartHeight - h - l; this.right = b.chartWidth - e - d; this.len = Math.max(a ? e : h, 0); this.pos = a ? d : l
            }; a.prototype.getExtremes = function () {
                var b = this.logarithmic;
                return { min: b ? t(b.lin2log(this.min)) : this.min, max: b ? t(b.lin2log(this.max)) : this.max, dataMin: this.dataMin, dataMax: this.dataMax, userMin: this.userMin, userMax: this.userMax }
            }; a.prototype.getThreshold = function (b) { var d = this.logarithmic, c = d ? d.lin2log(this.min) : this.min; d = d ? d.lin2log(this.max) : this.max; null === b || -Infinity === b ? b = c : Infinity === b ? b = d : c > b ? b = c : d < b && (b = d); return this.translate(b, 0, 1, 0, 1) }; a.prototype.autoLabelAlign = function (b) {
                var d = (q(b, 0) - 90 * this.side + 720) % 360; b = { align: "center" }; m(this, "autoLabelAlign",
                    b, function (b) { 15 < d && 165 > d ? b.align = "right" : 195 < d && 345 > d && (b.align = "left") }); return b.align
            }; a.prototype.tickSize = function (b) { var d = this.options, c = q(d["tick" === b ? "tickWidth" : "minorTickWidth"], "tick" === b && this.isXAxis && !this.categories ? 1 : 0), a = d["tick" === b ? "tickLength" : "minorTickLength"]; if (c && a) { "inside" === d[b + "Position"] && (a = -a); var e = [a, c] } b = { tickSize: e }; m(this, "afterTickSize", b); return b.tickSize }; a.prototype.labelMetrics = function () {
                var b = this.tickPositions && this.tickPositions[0] || 0; return this.chart.renderer.fontMetrics(this.options.labels.style.fontSize,
                    this.ticks[b] && this.ticks[b].label)
            }; a.prototype.unsquish = function () {
                var d = this.options.labels, c = this.horiz, a = this.tickInterval, h = this.len / (((this.categories ? 1 : 0) + this.max - this.min) / a), l = d.rotation, g = this.labelMetrics(), m = Math.max(this.max - this.min, 0), k = function (b) { var d = b / (h || 1); d = 1 < d ? Math.ceil(d) : 1; d * a > m && Infinity !== b && Infinity !== h && m && (d = Math.ceil(m / a)); return t(d * a) }, f = a, z, G, n = Number.MAX_VALUE; if (c) {
                    if (!d.staggerLines && !d.step) if (b(l)) var r = [l]; else h < d.autoRotationLimit && (r = d.autoRotation); r &&
                        r.forEach(function (b) { if (b === l || b && -90 <= b && 90 >= b) { G = k(Math.abs(g.h / Math.sin(e * b))); var d = G + Math.abs(b / 360); d < n && (n = d, z = b, f = G) } })
                } else d.step || (f = k(g.h)); this.autoRotation = r; this.labelRotation = q(z, b(l) ? l : 0); return f
            }; a.prototype.getSlotWidth = function (d) {
                var c = this.chart, a = this.horiz, e = this.options.labels, h = Math.max(this.tickPositions.length - (this.categories ? 0 : 1), 1), l = c.margin[3]; if (d && b(d.slotWidth)) return d.slotWidth; if (a && 2 > e.step) return e.rotation ? 0 : (this.staggerLines || 1) * this.len / h; if (!a) {
                    d = e.style.width;
                    if (void 0 !== d) return parseInt(String(d), 10); if (l) return l - c.spacing[3]
                } return .33 * c.chartWidth
            }; a.prototype.renderUnsquish = function () {
                var b = this.chart, d = b.renderer, c = this.tickPositions, a = this.ticks, e = this.options.labels, h = e.style, g = this.horiz, m = this.getSlotWidth(), q = Math.max(1, Math.round(m - 2 * e.padding)), k = {}, f = this.labelMetrics(), z = h.textOverflow, G = 0; l(e.rotation) || (k.rotation = e.rotation || 0); c.forEach(function (b) { b = a[b]; b.movedLabel && b.replaceMovedLabel(); b && b.label && b.label.textPxLength > G && (G = b.label.textPxLength) });
                this.maxLabelLength = G; if (this.autoRotation) G > q && G > f.h ? k.rotation = this.labelRotation : this.labelRotation = 0; else if (m) { var n = q; if (!z) { var r = "clip"; for (q = c.length; !g && q--;) { var p = c[q]; if (p = a[p].label) p.styles && "ellipsis" === p.styles.textOverflow ? p.css({ textOverflow: "clip" }) : p.textPxLength > m && p.css({ width: m + "px" }), p.getBBox().height > this.len / c.length - (f.h - f.f) && (p.specificTextOverflow = "ellipsis") } } } k.rotation && (n = G > .5 * b.chartHeight ? .33 * b.chartHeight : G, z || (r = "ellipsis")); if (this.labelAlign = e.align || this.autoLabelAlign(this.labelRotation)) k.align =
                    this.labelAlign; c.forEach(function (b) { var d = (b = a[b]) && b.label, c = h.width, e = {}; d && (d.attr(k), b.shortenLabel ? b.shortenLabel() : n && !c && "nowrap" !== h.whiteSpace && (n < d.textPxLength || "SPAN" === d.element.tagName) ? (e.width = n + "px", z || (e.textOverflow = d.specificTextOverflow || r), d.css(e)) : d.styles && d.styles.width && !e.width && !c && d.css({ width: null }), delete d.specificTextOverflow, b.rotation = k.rotation) }, this); this.tickRotCorr = d.rotCorr(f.b, this.labelRotation || 0, 0 !== this.side)
            }; a.prototype.hasData = function () {
                return this.series.some(function (b) { return b.hasData() }) ||
                    this.options.showEmpty && v(this.min) && v(this.max)
            }; a.prototype.addTitle = function (b) {
                var c = this.chart.renderer, a = this.horiz, e = this.opposite, h = this.options.title, l = this.chart.styledMode, g; this.axisTitle || ((g = h.textAlign) || (g = (a ? { low: "left", middle: "center", high: "right" } : { low: e ? "right" : "left", middle: "center", high: e ? "left" : "right" })[h.align]), this.axisTitle = c.text(h.text || "", 0, 0, h.useHTML).attr({ zIndex: 7, rotation: h.rotation, align: g }).addClass("highcharts-axis-title"), l || this.axisTitle.css(d(h.style)), this.axisTitle.add(this.axisGroup),
                    this.axisTitle.isNew = !0); l || h.style.width || this.isRadial || this.axisTitle.css({ width: this.len + "px" }); this.axisTitle[b ? "show" : "hide"](b)
            }; a.prototype.generateTick = function (b) { var d = this.ticks; d[b] ? d[b].addLabel() : d[b] = new J(this, b) }; a.prototype.getOffset = function () {
                var b = this, d = this, c = d.chart, a = d.horiz, e = d.options, h = d.side, l = d.ticks, g = d.tickPositions, k = d.coll, f = d.axisParent, G = c.renderer, n = c.inverted && !d.isZAxis ? [1, 0, 3, 2][h] : h, r = d.hasData(), p = e.title, t = e.labels, w = c.axisOffset; c = c.clipOffset; var O = [-1,
                    1, 1, -1][h], P = e.className, A, u = 0, fa = 0, ca = 0; d.showAxis = A = r || e.showEmpty; d.staggerLines = d.horiz && t.staggerLines || void 0; if (!d.axisGroup) { var N = function (d, c, a) { return G.g(d).attr({ zIndex: a }).addClass("highcharts-".concat(k.toLowerCase()).concat(c, " ") + (b.isRadial ? "highcharts-radial-axis".concat(c, " ") : "") + (P || "")).add(f) }; d.gridGroup = N("grid", "-grid", e.gridZIndex); d.axisGroup = N("axis", "", e.zIndex); d.labelGroup = N("axis-labels", "-labels", t.zIndex) } r || d.isLinked ? (g.forEach(function (b) { d.generateTick(b) }),
                        d.renderUnsquish(), d.reserveSpaceDefault = 0 === h || 2 === h || { 1: "left", 3: "right" }[h] === d.labelAlign, q(t.reserveSpace, "center" === d.labelAlign ? !0 : null, d.reserveSpaceDefault) && g.forEach(function (b) { ca = Math.max(l[b].getLabelSize(), ca) }), d.staggerLines && (ca *= d.staggerLines), d.labelOffset = ca * (d.opposite ? -1 : 1)) : z(l, function (b, d) { b.destroy(); delete l[d] }); if (p && p.text && !1 !== p.enabled && (d.addTitle(A), A && !1 !== p.reserveSpace)) {
                            d.titleOffset = u = d.axisTitle.getBBox()[a ? "height" : "width"]; var x = p.offset; fa = v(x) ? 0 : q(p.margin,
                                a ? 5 : 10)
                        } d.renderLine(); d.offset = O * q(e.offset, w[h] ? w[h] + (e.margin || 0) : 0); d.tickRotCorr = d.tickRotCorr || { x: 0, y: 0 }; p = 0 === h ? -d.labelMetrics().h : 2 === h ? d.tickRotCorr.y : 0; r = Math.abs(ca) + fa; ca && (r = r - p + O * (a ? q(t.y, d.tickRotCorr.y + 8 * O) : t.x)); d.axisTitleMargin = q(x, r); d.getMaxLabelDimensions && (d.maxLabelDimensions = d.getMaxLabelDimensions(l, g)); "colorAxis" !== k && (a = this.tickSize("tick"), w[h] = Math.max(w[h], (d.axisTitleMargin || 0) + u + O * d.offset, r, g && g.length && a ? a[0] + O * d.offset : 0), e = !d.axisLine || e.offset ? 0 : 2 * Math.floor(d.axisLine.strokeWidth() /
                            2), c[n] = Math.max(c[n], e)); m(this, "afterGetOffset")
            }; a.prototype.getLinePath = function (b) { var d = this.chart, c = this.opposite, a = this.offset, e = this.horiz, h = this.left + (c ? this.width : 0) + a; a = d.chartHeight - this.bottom - (c ? this.height : 0) + a; c && (b *= -1); return d.renderer.crispLine([["M", e ? this.left : h, e ? a : this.top], ["L", e ? d.chartWidth - this.right : h, e ? a : d.chartHeight - this.bottom]], b) }; a.prototype.renderLine = function () {
                this.axisLine || (this.axisLine = this.chart.renderer.path().addClass("highcharts-axis-line").add(this.axisGroup),
                    this.chart.styledMode || this.axisLine.attr({ stroke: this.options.lineColor, "stroke-width": this.options.lineWidth, zIndex: 7 }))
            }; a.prototype.getTitlePosition = function () {
                var b = this.horiz, d = this.left, c = this.top, a = this.len, e = this.options.title, h = b ? d : c, l = this.opposite, g = this.offset, q = e.x, k = e.y, f = this.axisTitle, z = this.chart.renderer.fontMetrics(e.style.fontSize, f); f = f ? Math.max(f.getBBox(!1, 0).height - z.h - 1, 0) : 0; a = { low: h + (b ? 0 : a), middle: h + a / 2, high: h + (b ? a : 0) }[e.align]; d = (b ? c + this.height : d) + (b ? 1 : -1) * (l ? -1 : 1) * (this.axisTitleMargin ||
                    0) + [-f, f, z.f, -f][this.side]; b = { x: b ? a + q : d + (l ? this.width : 0) + g + q, y: b ? d + k - (l ? this.height : 0) + g : a + k }; m(this, "afterGetTitlePosition", { titlePosition: b }); return b
            }; a.prototype.renderMinorTick = function (b, d) { var c = this.minorTicks; c[b] || (c[b] = new J(this, b, "minor")); d && c[b].isNew && c[b].render(null, !0); c[b].render(null, !1, 1) }; a.prototype.renderTick = function (b, d, c) {
                var a = this.ticks; if (!this.isLinked || b >= this.min && b <= this.max || this.grid && this.grid.isColumn) a[b] || (a[b] = new J(this, b)), c && a[b].isNew && a[b].render(d,
                    !0, -1), a[b].render(d)
            }; a.prototype.render = function () {
                var d = this, c = d.chart, a = d.logarithmic, e = d.options, h = d.isLinked, l = d.tickPositions, g = d.axisTitle, q = d.ticks, k = d.minorTicks, f = d.alternateBands, G = e.stackLabels, n = e.alternateGridColor, r = d.tickmarkOffset, p = d.axisLine, t = d.showAxis, v = x(c.renderer.globalAnimation), w, O; d.labelEdge.length = 0; d.overlap = !1;[q, k, f].forEach(function (b) { z(b, function (b) { b.isActive = !1 }) }); if (d.hasData() || h) {
                    var P = d.chart.hasRendered && d.old && b(d.old.min); d.minorTickInterval && !d.categories &&
                        d.getMinorTickPositions().forEach(function (b) { d.renderMinorTick(b, P) }); l.length && (l.forEach(function (b, c) { d.renderTick(b, c, P) }), r && (0 === d.min || d.single) && (q[-1] || (q[-1] = new J(d, -1, null, !0)), q[-1].render(-1))); n && l.forEach(function (b, e) {
                            O = "undefined" !== typeof l[e + 1] ? l[e + 1] + r : d.max - r; 0 === e % 2 && b < d.max && O <= d.max + (c.polar ? -r : r) && (f[b] || (f[b] = new I.PlotLineOrBand(d)), w = b + r, f[b].options = { from: a ? a.lin2log(w) : w, to: a ? a.lin2log(O) : O, color: n, className: "highcharts-alternate-grid" }, f[b].render(), f[b].isActive =
                                !0)
                        }); d._addedPlotLB || (d._addedPlotLB = !0, (e.plotLines || []).concat(e.plotBands || []).forEach(function (b) { d.addPlotBandOrLine(b) }))
                } [q, k, f].forEach(function (b) { var d = [], a = v.duration; z(b, function (b, c) { b.isActive || (b.render(c, !1, 0), b.isActive = !1, d.push(c)) }); N(function () { for (var c = d.length; c--;)b[d[c]] && !b[d[c]].isActive && (b[d[c]].destroy(), delete b[d[c]]) }, b !== f && c.hasRendered && a ? a : 0) }); p && (p[p.isPlaced ? "animate" : "attr"]({ d: this.getLinePath(p.strokeWidth()) }), p.isPlaced = !0, p[t ? "show" : "hide"](t)); g && t &&
                    (e = d.getTitlePosition(), g[g.isNew ? "attr" : "animate"](e), g.isNew = !1); G && G.enabled && d.stacking && d.stacking.renderStackTotals(); d.old = { len: d.len, max: d.max, min: d.min, transA: d.transA, userMax: d.userMax, userMin: d.userMin }; d.isDirty = !1; m(this, "afterRender")
            }; a.prototype.redraw = function () { this.visible && (this.render(), this.plotLinesAndBands.forEach(function (b) { b.render() })); this.series.forEach(function (b) { b.isDirty = !0 }) }; a.prototype.getKeepProps = function () { return this.keepProps || a.keepProps }; a.prototype.destroy =
                function (b) {
                    var d = this, c = d.plotLinesAndBands, a = this.eventOptions; m(this, "destroy", { keepEvents: b }); b || P(d);[d.ticks, d.minorTicks, d.alternateBands].forEach(function (b) { u(b) }); if (c) for (b = c.length; b--;)c[b].destroy(); "axisLine axisTitle axisGroup gridGroup labelGroup cross scrollbar".split(" ").forEach(function (b) { d[b] && (d[b] = d[b].destroy()) }); for (var e in d.plotLinesAndBandsGroups) d.plotLinesAndBandsGroups[e] = d.plotLinesAndBandsGroups[e].destroy(); z(d, function (b, c) {
                        -1 === d.getKeepProps().indexOf(c) &&
                        delete d[c]
                    }); this.eventOptions = a
                }; a.prototype.drawCrosshair = function (b, d) {
                    var c = this.crosshair, a = q(c && c.snap, !0), e = this.chart, h, l = this.cross; m(this, "drawCrosshair", { e: b, point: d }); b || (b = this.cross && this.cross.e); if (c && !1 !== (v(d) || !a)) {
                        a ? v(d) && (h = q("colorAxis" !== this.coll ? d.crosshairPos : null, this.isXAxis ? d.plotX : this.len - d.plotY)) : h = b && (this.horiz ? b.chartX - this.pos : this.len - b.chartY + this.pos); if (v(h)) {
                            var g = { value: d && (this.isXAxis ? d.x : q(d.stackY, d.y)), translatedValue: h }; e.polar && r(g, {
                                isCrosshair: !0,
                                chartX: b && b.chartX, chartY: b && b.chartY, point: d
                            }); g = this.getPlotLinePath(g) || null
                        } if (!v(g)) { this.hideCrosshair(); return } a = this.categories && !this.isRadial; l || (this.cross = l = e.renderer.path().addClass("highcharts-crosshair highcharts-crosshair-" + (a ? "category " : "thin ") + (c.className || "")).attr({ zIndex: q(c.zIndex, 2) }).add(), e.styledMode || (l.attr({ stroke: c.color || (a ? B.parse("#ccd6eb").setOpacity(.25).get() : "#cccccc"), "stroke-width": q(c.width, 1) }).css({ "pointer-events": "none" }), c.dashStyle && l.attr({ dashstyle: c.dashStyle })));
                        l.show().attr({ d: g }); a && !c.width && l.attr({ "stroke-width": this.transA }); this.cross.e = b
                    } else this.hideCrosshair(); m(this, "afterDrawCrosshair", { e: b, point: d })
                }; a.prototype.hideCrosshair = function () { this.cross && this.cross.hide(); m(this, "afterHideCrosshair") }; a.prototype.hasVerticalPanning = function () { var b = this.chart.options.chart.panning; return !!(b && b.enabled && /y/.test(b.type)) }; a.prototype.validatePositiveValue = function (d) { return b(d) && 0 < d }; a.prototype.update = function (b, c) {
                    var a = this.chart; b = d(this.userOptions,
                        b); this.destroy(!0); this.init(a, b); a.isDirtyBox = !0; q(c, !0) && a.redraw()
                }; a.prototype.remove = function (b) { for (var d = this.chart, c = this.coll, a = this.series, e = a.length; e--;)a[e] && a[e].remove(!1); w(d.axes, this); w(d[c], this); d[c].forEach(function (b, d) { b.options.index = b.userOptions.index = d }); this.destroy(); d.isDirtyBox = !0; q(b, !0) && d.redraw() }; a.prototype.setTitle = function (b, d) { this.update({ title: b }, d) }; a.prototype.setCategories = function (b, d) { this.update({ categories: b }, d) }; a.defaultOptions = f.defaultXAxisOptions;
            a.keepProps = "extKey hcEvents names series userMax userMin".split(" "); return a
        }(); ""; return a
    }); L(f, "Core/Axis/DateTimeAxis.js", [f["Core/Utilities.js"]], function (a) {
        var f = a.addEvent, B = a.getMagnitude, F = a.normalizeTickInterval, y = a.timeUnits, I; (function (a) {
            function C() { return this.chart.time.getTimeTicks.apply(this.chart.time, arguments) } function x(a) { "datetime" !== a.userOptions.type ? this.dateTime = void 0 : this.dateTime || (this.dateTime = new g(this)) } var p = []; a.compose = function (a) {
                -1 === p.indexOf(a) && (p.push(a),
                    a.keepProps.push("dateTime"), a.prototype.getTimeTicks = C, f(a, "init", x)); return a
            }; var g = function () {
                function a(c) { this.axis = c } a.prototype.normalizeTimeTickInterval = function (c, a) {
                    var e = a || [["millisecond", [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], ["second", [1, 2, 5, 10, 15, 30]], ["minute", [1, 2, 5, 10, 15, 30]], ["hour", [1, 2, 3, 4, 6, 8, 12]], ["day", [1, 2]], ["week", [1, 2]], ["month", [1, 2, 3, 4, 6]], ["year", null]]; a = e[e.length - 1]; var g = y[a[0]], f = a[1], n; for (n = 0; n < e.length && !(a = e[n], g = y[a[0]], f = a[1], e[n + 1] && c <= (g * f[f.length - 1] + y[e[n +
                        1][0]]) / 2); n++); g === y.year && c < 5 * g && (f = [1, 2, 5]); c = F(c / g, f, "year" === a[0] ? Math.max(B(c / g), 1) : 1); return { unitRange: g, count: c, unitName: a[0] }
                }; a.prototype.getXDateFormat = function (c, a) { var e = this.axis; return e.closestPointRange ? e.chart.time.getDateFormat(e.closestPointRange, c, e.options.startOfWeek, a) || a.year : a.day }; return a
            }(); a.Additions = g
        })(I || (I = {})); return I
    }); L(f, "Core/Axis/LogarithmicAxis.js", [f["Core/Utilities.js"]], function (a) {
        var f = a.addEvent, B = a.normalizeTickInterval, F = a.pick, y; (function (a) {
            function y(a) {
                var e =
                    this.logarithmic; "logarithmic" !== a.userOptions.type ? this.logarithmic = void 0 : e || (this.logarithmic = new p(this))
            } function C() { var a = this.logarithmic; a && (this.lin2val = function (e) { return a.lin2log(e) }, this.val2lin = function (e) { return a.log2lin(e) }) } var x = []; a.compose = function (a) { -1 === x.indexOf(a) && (x.push(a), a.keepProps.push("logarithmic"), f(a, "init", y), f(a, "afterInit", C)); return a }; var p = function () {
                function a(a) { this.axis = a } a.prototype.getLogTickPositions = function (a, c, g, k) {
                    var e = this.axis, f = e.len, n = e.options,
                    p = []; k || (this.minorAutoInterval = void 0); if (.5 <= a) a = Math.round(a), p = e.getLinearTickPositions(a, c, g); else if (.08 <= a) { var A = Math.floor(c), r, m = n = void 0; for (f = .3 < a ? [1, 2, 4] : .15 < a ? [1, 2, 4, 6, 8] : [1, 2, 3, 4, 5, 6, 7, 8, 9]; A < g + 1 && !m; A++) { var h = f.length; for (r = 0; r < h && !m; r++) { var b = this.log2lin(this.lin2log(A) * f[r]); b > c && (!k || n <= g) && "undefined" !== typeof n && p.push(n); n > g && (m = !0); n = b } } } else c = this.lin2log(c), g = this.lin2log(g), a = k ? e.getMinorTickInterval() : n.tickInterval, a = F("auto" === a ? null : a, this.minorAutoInterval, n.tickPixelInterval /
                        (k ? 5 : 1) * (g - c) / ((k ? f / e.tickPositions.length : f) || 1)), a = B(a), p = e.getLinearTickPositions(a, c, g).map(this.log2lin), k || (this.minorAutoInterval = a / 5); k || (e.tickInterval = a); return p
                }; a.prototype.lin2log = function (a) { return Math.pow(10, a) }; a.prototype.log2lin = function (a) { return Math.log(a) / Math.LN10 }; return a
            }(); a.Additions = p
        })(y || (y = {})); return y
    }); L(f, "Core/Axis/PlotLineOrBand/PlotLineOrBandAxis.js", [f["Core/Utilities.js"]], function (a) {
        var f = a.erase, B = a.extend, F = a.isNumber, y; (function (a) {
            var y = [], C; a.compose =
                function (a, g) { C || (C = a); -1 === y.indexOf(g) && (y.push(g), B(g.prototype, x.prototype)); return g }; var x = function () {
                    function a() { } a.prototype.getPlotBandPath = function (a, e, c) {
                        void 0 === c && (c = this.options); var g = this.getPlotLinePath({ value: e, force: !0, acrossPanes: c.acrossPanes }), f = [], p = this.horiz; e = !F(this.min) || !F(this.max) || a < this.min && e < this.min || a > this.max && e > this.max; a = this.getPlotLinePath({ value: a, force: !0, acrossPanes: c.acrossPanes }); c = 1; if (a && g) {
                            if (e) { var v = a.toString() === g.toString(); c = 0 } for (e = 0; e < a.length; e +=
                                2) { var u = a[e], w = a[e + 1], A = g[e], r = g[e + 1]; "M" !== u[0] && "L" !== u[0] || "M" !== w[0] && "L" !== w[0] || "M" !== A[0] && "L" !== A[0] || "M" !== r[0] && "L" !== r[0] || (p && A[1] === u[1] ? (A[1] += c, r[1] += c) : p || A[2] !== u[2] || (A[2] += c, r[2] += c), f.push(["M", u[1], u[2]], ["L", w[1], w[2]], ["L", r[1], r[2]], ["L", A[1], A[2]], ["Z"])); f.isFlat = v }
                        } return f
                    }; a.prototype.addPlotBand = function (a) { return this.addPlotBandOrLine(a, "plotBands") }; a.prototype.addPlotLine = function (a) { return this.addPlotBandOrLine(a, "plotLines") }; a.prototype.addPlotBandOrLine = function (a,
                        e) { var c = this, g = this.userOptions, f = new C(this, a); this.visible && (f = f.render()); if (f) { this._addedPlotLB || (this._addedPlotLB = !0, (g.plotLines || []).concat(g.plotBands || []).forEach(function (a) { c.addPlotBandOrLine(a) })); if (e) { var p = g[e] || []; p.push(a); g[e] = p } this.plotLinesAndBands.push(f) } return f }; a.prototype.removePlotBandOrLine = function (a) {
                            var e = this.plotLinesAndBands, c = this.options, g = this.userOptions; if (e) {
                                for (var k = e.length; k--;)e[k].id === a && e[k].destroy();[c.plotLines || [], g.plotLines || [], c.plotBands ||
                                    [], g.plotBands || []].forEach(function (c) { for (k = c.length; k--;)(c[k] || {}).id === a && f(c, c[k]) })
                            }
                        }; a.prototype.removePlotBand = function (a) { this.removePlotBandOrLine(a) }; a.prototype.removePlotLine = function (a) { this.removePlotBandOrLine(a) }; return a
                }()
        })(y || (y = {})); return y
    }); L(f, "Core/Axis/PlotLineOrBand/PlotLineOrBand.js", [f["Core/Axis/PlotLineOrBand/PlotLineOrBandAxis.js"], f["Core/Utilities.js"]], function (a, f) {
        var D = f.arrayMax, F = f.arrayMin, y = f.defined, I = f.destroyObjectProperties, J = f.erase, C = f.fireEvent,
        x = f.merge, p = f.objectEach, g = f.pick; f = function () {
            function e(c, a) { this.axis = c; a && (this.options = a, this.id = a.id) } e.compose = function (c) { return a.compose(e, c) }; e.prototype.render = function () {
                C(this, "render"); var c = this, a = c.axis, e = a.horiz, f = a.logarithmic, v = c.options, u = v.color, w = g(v.zIndex, 0), A = v.events, r = {}, m = a.chart.renderer, h = v.label, b = c.label, l = v.to, d = v.from, G = v.value, z = c.svgElem, q = [], O = y(d) && y(l); q = y(G); var P = !z, S = { "class": "highcharts-plot-" + (O ? "band " : "line ") + (v.className || "") }, N = O ? "bands" : "lines"; f &&
                    (d = f.log2lin(d), l = f.log2lin(l), G = f.log2lin(G)); a.chart.styledMode || (q ? (S.stroke = u || "#999999", S["stroke-width"] = g(v.width, 1), v.dashStyle && (S.dashstyle = v.dashStyle)) : O && (S.fill = u || "#e6ebf5", v.borderWidth && (S.stroke = v.borderColor, S["stroke-width"] = v.borderWidth))); r.zIndex = w; N += "-" + w; (f = a.plotLinesAndBandsGroups[N]) || (a.plotLinesAndBandsGroups[N] = f = m.g("plot-" + N).attr(r).add()); P && (c.svgElem = z = m.path().attr(S).add(f)); if (q) q = a.getPlotLinePath({ value: G, lineWidth: z.strokeWidth(), acrossPanes: v.acrossPanes });
                else if (O) q = a.getPlotBandPath(d, l, v); else return; !c.eventsAdded && A && (p(A, function (b, d) { z.on(d, function (b) { A[d].apply(c, [b]) }) }), c.eventsAdded = !0); (P || !z.d) && q && q.length ? z.attr({ d: q }) : z && (q ? (z.show(), z.animate({ d: q })) : z.d && (z.hide(), b && (c.label = b = b.destroy()))); h && (y(h.text) || y(h.formatter)) && q && q.length && 0 < a.width && 0 < a.height && !q.isFlat ? (h = x({ align: e && O && "center", x: e ? !O && 4 : 10, verticalAlign: !e && O && "middle", y: e ? O ? 16 : 10 : O ? 6 : -4, rotation: e && !O && 90 }, h), this.renderLabel(h, q, O, w)) : b && b.hide(); return c
            }; e.prototype.renderLabel =
                function (a, e, g, f) {
                    var c = this.axis, k = c.chart.renderer, n = this.label; n || (this.label = n = k.text(this.getLabelText(a), 0, 0, a.useHTML).attr({ align: a.textAlign || a.align, rotation: a.rotation, "class": "highcharts-plot-" + (g ? "band" : "line") + "-label " + (a.className || ""), zIndex: f }).add(), c.chart.styledMode || n.css(x({ textOverflow: "ellipsis" }, a.style))); f = e.xBounds || [e[0][1], e[1][1], g ? e[2][1] : e[0][1]]; e = e.yBounds || [e[0][2], e[1][2], g ? e[2][2] : e[0][2]]; g = F(f); k = F(e); n.align(a, !1, { x: g, y: k, width: D(f) - g, height: D(e) - k }); n.alignValue &&
                        "left" !== n.alignValue || n.css({ width: (90 === n.rotation ? c.height - (n.alignAttr.y - c.top) : c.width - (n.alignAttr.x - c.left)) + "px" }); n.show(!0)
                }; e.prototype.getLabelText = function (a) { return y(a.formatter) ? a.formatter.call(this) : a.text }; e.prototype.destroy = function () { J(this.axis.plotLinesAndBands, this); delete this.axis; I(this) }; return e
        }(); ""; ""; return f
    }); L(f, "Core/Tooltip.js", [f["Core/FormatUtilities.js"], f["Core/Globals.js"], f["Core/Renderer/RendererUtilities.js"], f["Core/Renderer/RendererRegistry.js"], f["Core/Utilities.js"]],
        function (a, f, B, F, y) {
            var D = a.format, J = f.doc, C = B.distribute, x = y.addEvent, p = y.clamp, g = y.css, e = y.defined, c = y.discardElement, n = y.extend, k = y.fireEvent, t = y.isArray, v = y.isNumber, u = y.isString, w = y.merge, A = y.pick, r = y.splat, m = y.syncTimeout; a = function () {
                function a(b, a) { this.allowShared = !0; this.container = void 0; this.crosshairs = []; this.distance = 0; this.isHidden = !0; this.isSticky = !1; this.now = {}; this.options = {}; this.outside = !1; this.chart = b; this.init(b, a) } a.prototype.applyFilter = function () {
                    var b = this.chart; b.renderer.definition({
                        tagName: "filter",
                        attributes: { id: "drop-shadow-" + b.index, opacity: .5 }, children: [{ tagName: "feGaussianBlur", attributes: { "in": "SourceAlpha", stdDeviation: 1 } }, { tagName: "feOffset", attributes: { dx: 1, dy: 1 } }, { tagName: "feComponentTransfer", children: [{ tagName: "feFuncA", attributes: { type: "linear", slope: .3 } }] }, { tagName: "feMerge", children: [{ tagName: "feMergeNode" }, { tagName: "feMergeNode", attributes: { "in": "SourceGraphic" } }] }]
                    })
                }; a.prototype.bodyFormatter = function (b) {
                    return b.map(function (b) {
                        var d = b.series.tooltipOptions; return (d[(b.point.formatPrefix ||
                            "point") + "Formatter"] || b.point.tooltipFormatter).call(b.point, d[(b.point.formatPrefix || "point") + "Format"] || "")
                    })
                }; a.prototype.cleanSplit = function (b) { this.chart.series.forEach(function (a) { var d = a && a.tt; d && (!d.isActive || b ? a.tt = d.destroy() : d.isActive = !1) }) }; a.prototype.defaultFormatter = function (b) { var a = this.points || r(this); var d = [b.tooltipFooterHeaderFormatter(a[0])]; d = d.concat(b.bodyFormatter(a)); d.push(b.tooltipFooterHeaderFormatter(a[0], !0)); return d }; a.prototype.destroy = function () {
                    this.label && (this.label =
                        this.label.destroy()); this.split && this.tt && (this.cleanSplit(!0), this.tt = this.tt.destroy()); this.renderer && (this.renderer = this.renderer.destroy(), c(this.container)); y.clearTimeout(this.hideTimer); y.clearTimeout(this.tooltipTimeout)
                }; a.prototype.getAnchor = function (b, a) {
                    var d = this.chart, c = d.pointer, e = d.inverted, h = d.plotTop, l = d.plotLeft, g, m, f = 0, k = 0; b = r(b); this.followPointer && a ? ("undefined" === typeof a.chartX && (a = c.normalize(a)), c = [a.chartX - l, a.chartY - h]) : b[0].tooltipPos ? c = b[0].tooltipPos : (b.forEach(function (b) {
                        g =
                        b.series.yAxis; m = b.series.xAxis; f += b.plotX || 0; k += b.plotLow ? (b.plotLow + (b.plotHigh || 0)) / 2 : b.plotY || 0; m && g && (e ? (f += h + d.plotHeight - m.len - m.pos, k += l + d.plotWidth - g.len - g.pos) : (f += m.pos - l, k += g.pos - h))
                    }), f /= b.length, k /= b.length, c = [e ? d.plotWidth - k : f, e ? d.plotHeight - f : k], this.shared && 1 < b.length && a && (e ? c[0] = a.chartX - l : c[1] = a.chartY - h)); return c.map(Math.round)
                }; a.prototype.getLabel = function () {
                    var b = this, a = this.chart.styledMode, d = this.options, c = this.split && this.allowShared, h = "tooltip" + (e(d.className) ? " " + d.className :
                        ""), m = d.style.pointerEvents || (!this.followPointer && d.stickOnContact ? "auto" : "none"), k = function () { b.inContact = !0 }, n = function (d) { var a = b.chart.hoverSeries; b.inContact = b.shouldStickOnContact() && b.chart.pointer.inClass(d.relatedTarget, "highcharts-tooltip"); if (!b.inContact && a && a.onMouseOut) a.onMouseOut() }, r, p = this.chart.renderer; if (b.label) { var v = !b.label.hasClass("highcharts-label"); (c && !v || !c && v) && b.destroy() } if (!this.label) {
                            if (this.outside) {
                                v = this.chart.options.chart.style; var t = F.getRendererType();
                                this.container = r = f.doc.createElement("div"); r.className = "highcharts-tooltip-container"; g(r, { position: "absolute", top: "1px", pointerEvents: m, zIndex: Math.max(this.options.style.zIndex || 0, (v && v.zIndex || 0) + 3) }); x(r, "mouseenter", k); x(r, "mouseleave", n); f.doc.body.appendChild(r); this.renderer = p = new t(r, 0, 0, v, void 0, void 0, p.styledMode)
                            } c ? this.label = p.g(h) : (this.label = p.label("", 0, 0, d.shape, void 0, void 0, d.useHTML, void 0, h).attr({ padding: d.padding, r: d.borderRadius }), a || this.label.attr({
                                fill: d.backgroundColor,
                                "stroke-width": d.borderWidth
                            }).css(d.style).css({ pointerEvents: m }).shadow(d.shadow)); a && d.shadow && (this.applyFilter(), this.label.attr({ filter: "url(#drop-shadow-" + this.chart.index + ")" })); if (b.outside && !b.split) { var w = this.label, A = w.xSetter, u = w.ySetter; w.xSetter = function (d) { A.call(w, b.distance); r.style.left = d + "px" }; w.ySetter = function (d) { u.call(w, b.distance); r.style.top = d + "px" } } this.label.on("mouseenter", k).on("mouseleave", n).attr({ zIndex: 8 }).add()
                        } return this.label
                }; a.prototype.getPosition = function (b,
                    a, d) {
                        var c = this.chart, e = this.distance, h = {}, l = c.inverted && d.h || 0, g = this.outside, m = g ? J.documentElement.clientWidth - 2 * e : c.chartWidth, f = g ? Math.max(J.body.scrollHeight, J.documentElement.scrollHeight, J.body.offsetHeight, J.documentElement.offsetHeight, J.documentElement.clientHeight) : c.chartHeight, k = c.pointer.getChartPosition(), n = function (h) {
                            var l = "x" === h; return [h, l ? m : f, l ? b : a].concat(g ? [l ? b * k.scaleX : a * k.scaleY, l ? k.left - e + (d.plotX + c.plotLeft) * k.scaleX : k.top - e + (d.plotY + c.plotTop) * k.scaleY, 0, l ? m : f] : [l ? b : a, l ?
                                d.plotX + c.plotLeft : d.plotY + c.plotTop, l ? c.plotLeft : c.plotTop, l ? c.plotLeft + c.plotWidth : c.plotTop + c.plotHeight])
                        }, r = n("y"), p = n("x"), v; n = !!d.negative; !c.polar && c.hoverSeries && c.hoverSeries.yAxis && c.hoverSeries.yAxis.reversed && (n = !n); var t = !this.followPointer && A(d.ttBelow, !c.inverted === n), w = function (b, d, a, c, m, f, q) {
                            var z = g ? "y" === b ? e * k.scaleY : e * k.scaleX : e, G = (a - c) / 2, E = c < m - e, n = m + e + c < d, r = m - z - a + G; m = m + z - G; if (t && n) h[b] = m; else if (!t && E) h[b] = r; else if (E) h[b] = Math.min(q - c, 0 > r - l ? r : r - l); else if (n) h[b] = Math.max(f, m +
                                l + a > d ? m : m + l); else return !1
                        }, u = function (b, d, a, c, l) { var g; l < e || l > d - e ? g = !1 : h[b] = l < a / 2 ? 1 : l > d - c / 2 ? d - c - 2 : l - a / 2; return g }, E = function (b) { var d = r; r = p; p = d; v = b }, T = function () { !1 !== w.apply(0, r) ? !1 !== u.apply(0, p) || v || (E(!0), T()) : v ? h.x = h.y = 0 : (E(!0), T()) }; (c.inverted || 1 < this.len) && E(); T(); return h
                }; a.prototype.hide = function (b) { var a = this; y.clearTimeout(this.hideTimer); b = A(b, this.options.hideDelay); this.isHidden || (this.hideTimer = m(function () { a.getLabel().fadeOut(b ? void 0 : b); a.isHidden = !0 }, b)) }; a.prototype.init = function (b,
                    a) { this.chart = b; this.options = a; this.crosshairs = []; this.now = { x: 0, y: 0 }; this.isHidden = !0; this.split = a.split && !b.inverted && !b.polar; this.shared = a.shared || this.split; this.outside = A(a.outside, !(!b.scrollablePixelsX && !b.scrollablePixelsY)) }; a.prototype.shouldStickOnContact = function () { return !(this.followPointer || !this.options.stickOnContact) }; a.prototype.isStickyOnContact = function () { return !(!this.shouldStickOnContact() || !this.inContact) }; a.prototype.move = function (b, a, d, c) {
                        var e = this, h = e.now, l = !1 !== e.options.animation &&
                            !e.isHidden && (1 < Math.abs(b - h.x) || 1 < Math.abs(a - h.y)), g = e.followPointer || 1 < e.len; n(h, { x: l ? (2 * h.x + b) / 3 : b, y: l ? (h.y + a) / 2 : a, anchorX: g ? void 0 : l ? (2 * h.anchorX + d) / 3 : d, anchorY: g ? void 0 : l ? (h.anchorY + c) / 2 : c }); e.getLabel().attr(h); e.drawTracker(); l && (y.clearTimeout(this.tooltipTimeout), this.tooltipTimeout = setTimeout(function () { e && e.move(b, a, d, c) }, 32))
                    }; a.prototype.refresh = function (b, a) {
                        var d = this.chart, c = this.options, e = r(b), h = e[0], l = [], g = c.formatter || this.defaultFormatter, m = this.shared, f = d.styledMode, n = {}; if (c.enabled &&
                            h.series) {
                                y.clearTimeout(this.hideTimer); this.allowShared = !(!t(b) && b.series && b.series.noSharedTooltip); this.followPointer = !this.split && h.series.tooltipOptions.followPointer; b = this.getAnchor(b, a); var p = b[0], v = b[1]; m && this.allowShared ? (d.pointer.applyInactiveState(e), e.forEach(function (b) { b.setState("hover"); l.push(b.getLabelConfig()) }), n = { x: h.category, y: h.y }, n.points = l) : n = h.getLabelConfig(); this.len = l.length; g = g.call(n, this); m = h.series; this.distance = A(m.tooltipOptions.distance, 16); if (!1 === g) this.hide();
                                else {
                                    if (this.split && this.allowShared) this.renderSplit(g, e); else {
                                        var w = p, u = v; a && d.pointer.isDirectTouch && (w = a.chartX - d.plotLeft, u = a.chartY - d.plotTop); if (d.polar || !1 === m.options.clip || e.some(function (b) { return b.series.shouldShowTooltip(w, u) })) a = this.getLabel(), c.style.width && !f || a.css({ width: this.chart.spacingBox.width + "px" }), a.attr({ text: g && g.join ? g.join("") : g }), a.removeClass(/highcharts-color-[\d]+/g).addClass("highcharts-color-" + A(h.colorIndex, m.colorIndex)), f || a.attr({
                                            stroke: c.borderColor || h.color ||
                                                m.color || "#666666"
                                        }), this.updatePosition({ plotX: p, plotY: v, negative: h.negative, ttBelow: h.ttBelow, h: b[2] || 0 }); else { this.hide(); return }
                                    } this.isHidden && this.label && this.label.attr({ opacity: 1 }).show(); this.isHidden = !1
                                } k(this, "refresh")
                        }
                    }; a.prototype.renderSplit = function (b, a) {
                        function d(b, d, a, e, h) { void 0 === h && (h = !0); a ? (d = Y ? 0 : I, b = p(b - e / 2, M.left, M.right - e - (c.outside ? R : 0))) : (d -= B, b = h ? b - e - D : b + D, b = p(b, h ? b : M.left, M.right)); return { x: b, y: d } } var c = this, e = c.chart, h = c.chart, l = h.chartWidth, g = h.chartHeight, m = h.plotHeight,
                            f = h.plotLeft, k = h.plotTop, r = h.pointer, v = h.scrollablePixelsY; v = void 0 === v ? 0 : v; var t = h.scrollablePixelsX, w = h.scrollingContainer; w = void 0 === w ? { scrollLeft: 0, scrollTop: 0 } : w; var x = w.scrollLeft; w = w.scrollTop; var y = h.styledMode, D = c.distance, E = c.options, T = c.options.positioner, M = c.outside && "number" !== typeof t ? J.documentElement.getBoundingClientRect() : { left: x, right: x + l, top: w, bottom: w + g }, U = c.getLabel(), V = this.renderer || e.renderer, Y = !(!e.xAxis[0] || !e.xAxis[0].opposite); e = r.getChartPosition(); var R = e.left; e = e.top;
                        var B = k + w, aa = 0, I = m - v; u(b) && (b = [!1, b]); b = b.slice(0, a.length + 1).reduce(function (b, e, h) {
                            if (!1 !== e && "" !== e) {
                                h = a[h - 1] || { isHeader: !0, plotX: a[0].plotX, plotY: m, series: {} }; var l = h.isHeader, g = l ? c : h.series; e = e.toString(); var q = g.tt, z = h.isHeader; var n = h.series; var G = "highcharts-color-" + A(h.colorIndex, n.colorIndex, "none"); q || (q = { padding: E.padding, r: E.borderRadius }, y || (q.fill = E.backgroundColor, q["stroke-width"] = E.borderWidth), q = V.label("", 0, 0, E[z ? "headerShape" : "shape"], void 0, void 0, E.useHTML).addClass((z ? "highcharts-tooltip-header " :
                                    "") + "highcharts-tooltip-box " + G).attr(q).add(U)); q.isActive = !0; q.attr({ text: e }); y || q.css(E.style).shadow(E.shadow).attr({ stroke: E.borderColor || h.color || n.color || "#333333" }); g = g.tt = q; z = g.getBBox(); e = z.width + g.strokeWidth(); l && (aa = z.height, I += aa, Y && (B -= aa)); n = h.plotX; n = void 0 === n ? 0 : n; G = h.plotY; G = void 0 === G ? 0 : G; q = h.series; if (h.isHeader) { n = f + n; var r = k + m / 2 } else { var v = q.xAxis, fa = q.yAxis; n = v.pos + p(n, -D, v.len + D); q.shouldShowTooltip(0, fa.pos - k + G, { ignoreX: !0 }) && (r = fa.pos + G) } n = p(n, M.left - D, M.right + D); "number" ===
                                        typeof r ? (z = z.height + 1, G = T ? T.call(c, e, z, h) : d(n, r, l, e), b.push({ align: T ? 0 : void 0, anchorX: n, anchorY: r, boxWidth: e, point: h, rank: A(G.rank, l ? 1 : 0), size: z, target: G.y, tt: g, x: G.x })) : g.isActive = !1
                            } return b
                        }, []); !T && b.some(function (b) { var d = (c.outside ? R : 0) + b.anchorX; return d < M.left && d + b.boxWidth < M.right ? !0 : d < R - M.left + b.boxWidth && M.right - d > d }) && (b = b.map(function (b) { var a = d(b.anchorX, b.anchorY, b.point.isHeader, b.boxWidth, !1); return n(b, { target: a.y, x: a.x }) })); c.cleanSplit(); C(b, I); var F = R, ba = R; b.forEach(function (b) {
                            var d =
                                b.x, a = b.boxWidth; b = b.isHeader; b || (c.outside && R + d < F && (F = R + d), !b && c.outside && F + a > ba && (ba = R + d))
                        }); b.forEach(function (b) { var d = b.x, a = b.anchorX, e = b.pos, h = b.point.isHeader; e = { visibility: "undefined" === typeof e ? "hidden" : "inherit", x: d, y: e + B, anchorX: a, anchorY: b.anchorY }; if (c.outside && d < a) { var l = R - F; 0 < l && (h || (e.x = d + l, e.anchorX = a + l), h && (e.x = (ba - F) / 2, e.anchorX = a + l)) } b.tt.attr(e) }); b = c.container; v = c.renderer; c.outside && b && v && (h = U.getBBox(), v.setSize(h.width + h.x, h.height + h.y, !1), b.style.left = F + "px", b.style.top =
                            e + "px")
                    }; a.prototype.drawTracker = function () {
                        if (this.followPointer || !this.options.stickOnContact) this.tracker && this.tracker.destroy(); else {
                            var b = this.chart, a = this.label, d = this.shared ? b.hoverPoints : b.hoverPoint; if (a && d) {
                                var c = { x: 0, y: 0, width: 0, height: 0 }; d = this.getAnchor(d); var e = a.getBBox(); d[0] += b.plotLeft - a.translateX; d[1] += b.plotTop - a.translateY; c.x = Math.min(0, d[0]); c.y = Math.min(0, d[1]); c.width = 0 > d[0] ? Math.max(Math.abs(d[0]), e.width - d[0]) : Math.max(Math.abs(d[0]), e.width); c.height = 0 > d[1] ? Math.max(Math.abs(d[1]),
                                    e.height - Math.abs(d[1])) : Math.max(Math.abs(d[1]), e.height); this.tracker ? this.tracker.attr(c) : (this.tracker = a.renderer.rect(c).addClass("highcharts-tracker").add(a), b.styledMode || this.tracker.attr({ fill: "rgba(0,0,0,0)" }))
                            }
                        }
                    }; a.prototype.styledModeFormat = function (b) { return b.replace('style="font-size: 10px"', 'class="highcharts-header"').replace(/style="color:{(point|series)\.color}"/g, 'class="highcharts-color-{$1.colorIndex}"') }; a.prototype.tooltipFooterHeaderFormatter = function (b, a) {
                        var d = b.series,
                        c = d.tooltipOptions, e = d.xAxis, h = e && e.dateTime; e = { isFooter: a, labelConfig: b }; var l = c.xDateFormat, g = c[a ? "footerFormat" : "headerFormat"]; k(this, "headerFormatter", e, function (a) { h && !l && v(b.key) && (l = h.getXDateFormat(b.key, c.dateTimeLabelFormats)); h && l && (b.point && b.point.tooltipDateKeys || ["key"]).forEach(function (b) { g = g.replace("{point." + b + "}", "{point." + b + ":" + l + "}") }); d.chart.styledMode && (g = this.styledModeFormat(g)); a.text = D(g, { point: b, series: d }, this.chart) }); return e.text
                    }; a.prototype.update = function (b) {
                        this.destroy();
                        w(!0, this.chart.options.tooltip.userOptions, b); this.init(this.chart, w(!0, this.options, b))
                    }; a.prototype.updatePosition = function (b) {
                        var a = this.chart, d = this.options, c = a.pointer, e = this.getLabel(); c = c.getChartPosition(); var h = (d.positioner || this.getPosition).call(this, e.width, e.height, b), m = b.plotX + a.plotLeft; b = b.plotY + a.plotTop; if (this.outside) {
                            d = d.borderWidth + 2 * this.distance; this.renderer.setSize(e.width + d, e.height + d, !1); if (1 !== c.scaleX || 1 !== c.scaleY) g(this.container, {
                                transform: "scale(".concat(c.scaleX,
                                    ", ").concat(c.scaleY, ")")
                            }), m *= c.scaleX, b *= c.scaleY; m += c.left - h.x; b += c.top - h.y
                        } this.move(Math.round(h.x), Math.round(h.y || 0), m, b)
                    }; return a
            }(); ""; return a
        }); L(f, "Core/Series/Point.js", [f["Core/Renderer/HTML/AST.js"], f["Core/Animation/AnimationUtilities.js"], f["Core/DefaultOptions.js"], f["Core/FormatUtilities.js"], f["Core/Utilities.js"]], function (a, f, B, F, y) {
            var D = f.animObject, J = B.defaultOptions, C = F.format, x = y.addEvent, p = y.defined, g = y.erase, e = y.extend, c = y.fireEvent, n = y.getNestedProperty, k = y.isArray,
            t = y.isFunction, v = y.isNumber, u = y.isObject, w = y.merge, A = y.objectEach, r = y.pick, m = y.syncTimeout, h = y.removeEvent, b = y.uniqueKey; f = function () {
                function l() { this.colorIndex = this.category = void 0; this.formatPrefix = "point"; this.id = void 0; this.isNull = !1; this.percentage = this.options = this.name = void 0; this.selected = !1; this.total = this.shapeArgs = this.series = void 0; this.visible = !0; this.x = void 0 } l.prototype.animateBeforeDestroy = function () {
                    var b = this, a = { x: b.startXPos, opacity: 0 }, c = b.getGraphicalProps(); c.singular.forEach(function (d) {
                        b[d] =
                        b[d].animate("dataLabel" === d ? { x: b[d].startXPos, y: b[d].startYPos, opacity: 0 } : a)
                    }); c.plural.forEach(function (d) { b[d].forEach(function (d) { d.element && d.animate(e({ x: b.startXPos }, d.startYPos ? { x: d.startXPos, y: d.startYPos } : {})) }) })
                }; l.prototype.applyOptions = function (b, a) {
                    var d = this.series, c = d.options.pointValKey || d.pointValKey; b = l.prototype.optionsToObject.call(this, b); e(this, b); this.options = this.options ? e(this.options, b) : b; b.group && delete this.group; b.dataLabels && delete this.dataLabels; c && (this.y = l.prototype.getNestedProperty.call(this,
                        c)); this.formatPrefix = (this.isNull = r(this.isValid && !this.isValid(), null === this.x || !v(this.y))) ? "null" : "point"; this.selected && (this.state = "select"); "name" in this && "undefined" === typeof a && d.xAxis && d.xAxis.hasNames && (this.x = d.xAxis.nameToX(this)); "undefined" === typeof this.x && d ? this.x = "undefined" === typeof a ? d.autoIncrement() : a : v(b.x) && d.options.relativeXValue && (this.x = d.autoIncrement(b.x)); return this
                }; l.prototype.destroy = function () {
                    function b() {
                        if (a.graphic || a.dataLabel || a.dataLabels) h(a), a.destroyElements();
                        for (k in a) a[k] = null
                    } var a = this, c = a.series, e = c.chart; c = c.options.dataSorting; var l = e.hoverPoints, f = D(a.series.chart.renderer.globalAnimation), k; a.legendItem && e.legend.destroyItem(a); l && (a.setState(), g(l, a), l.length || (e.hoverPoints = null)); if (a === e.hoverPoint) a.onMouseOut(); c && c.enabled ? (this.animateBeforeDestroy(), m(b, f.duration)) : b(); e.pointCount--
                }; l.prototype.destroyElements = function (b) {
                    var d = this; b = d.getGraphicalProps(b); b.singular.forEach(function (b) { d[b] = d[b].destroy() }); b.plural.forEach(function (b) {
                        d[b].forEach(function (b) {
                            b.element &&
                            b.destroy()
                        }); delete d[b]
                    })
                }; l.prototype.firePointEvent = function (b, a, e) { var d = this, h = this.series.options; (h.point.events[b] || d.options && d.options.events && d.options.events[b]) && d.importEvents(); "click" === b && h.allowPointSelect && (e = function (b) { d.select && d.select(null, b.ctrlKey || b.metaKey || b.shiftKey) }); c(d, b, a, e) }; l.prototype.getClassName = function () {
                    return "highcharts-point" + (this.selected ? " highcharts-point-select" : "") + (this.negative ? " highcharts-negative" : "") + (this.isNull ? " highcharts-null-point" : "") +
                        ("undefined" !== typeof this.colorIndex ? " highcharts-color-" + this.colorIndex : "") + (this.options.className ? " " + this.options.className : "") + (this.zone && this.zone.className ? " " + this.zone.className.replace("highcharts-negative", "") : "")
                }; l.prototype.getGraphicalProps = function (b) {
                    var d = this, a = [], c = { singular: [], plural: [] }, e; b = b || { graphic: 1, dataLabel: 1 }; b.graphic && a.push("graphic", "upperGraphic", "shadowGroup"); b.dataLabel && a.push("dataLabel", "dataLabelUpper", "connector"); for (e = a.length; e--;) {
                        var h = a[e]; d[h] &&
                            c.singular.push(h)
                    } ["dataLabel", "connector"].forEach(function (a) { var e = a + "s"; b[a] && d[e] && c.plural.push(e) }); return c
                }; l.prototype.getLabelConfig = function () { return { x: this.category, y: this.y, color: this.color, colorIndex: this.colorIndex, key: this.name || this.category, series: this.series, point: this, percentage: this.percentage, total: this.total || this.stackTotal } }; l.prototype.getNestedProperty = function (b) { if (b) return 0 === b.indexOf("custom.") ? n(b, this.options) : this[b] }; l.prototype.getZone = function () {
                    var b = this.series,
                    a = b.zones; b = b.zoneAxis || "y"; var c, e = 0; for (c = a[e]; this[b] >= c.value;)c = a[++e]; this.nonZonedColor || (this.nonZonedColor = this.color); this.color = c && c.color && !this.options.color ? c.color : this.nonZonedColor; return c
                }; l.prototype.hasNewShapeType = function () { return (this.graphic && (this.graphic.symbolName || this.graphic.element.nodeName)) !== this.shapeType }; l.prototype.init = function (d, a, e) {
                    this.series = d; this.applyOptions(a, e); this.id = p(this.id) ? this.id : b(); this.resolveColor(); d.chart.pointCount++; c(this, "afterInit");
                    return this
                }; l.prototype.optionsToObject = function (b) { var d = this.series, a = d.options.keys, c = a || d.pointArrayMap || ["y"], e = c.length, h = {}, g = 0, m = 0; if (v(b) || null === b) h[c[0]] = b; else if (k(b)) for (!a && b.length > e && (d = typeof b[0], "string" === d ? h.name = b[0] : "number" === d && (h.x = b[0]), g++); m < e;)a && "undefined" === typeof b[g] || (0 < c[m].indexOf(".") ? l.prototype.setNestedProperty(h, b[g], c[m]) : h[c[m]] = b[g]), g++, m++; else "object" === typeof b && (h = b, b.dataLabels && (d._hasPointLabels = !0), b.marker && (d._hasPointMarkers = !0)); return h };
                l.prototype.resolveColor = function () { var b = this.series, a = b.chart.styledMode; var c = b.chart.options.chart.colorCount; delete this.nonZonedColor; if (b.options.colorByPoint) { if (!a) { c = b.options.colors || b.chart.options.colors; var e = c[b.colorCounter]; c = c.length } a = b.colorCounter; b.colorCounter++; b.colorCounter === c && (b.colorCounter = 0) } else a || (e = b.color), a = b.colorIndex; this.colorIndex = r(this.options.colorIndex, a); this.color = r(this.options.color, e) }; l.prototype.setNestedProperty = function (b, a, c) {
                    c.split(".").reduce(function (b,
                        d, c, e) { b[d] = e.length - 1 === c ? a : u(b[d], !0) ? b[d] : {}; return b[d] }, b); return b
                }; l.prototype.tooltipFormatter = function (b) { var d = this.series, a = d.tooltipOptions, c = r(a.valueDecimals, ""), e = a.valuePrefix || "", h = a.valueSuffix || ""; d.chart.styledMode && (b = d.chart.tooltip.styledModeFormat(b)); (d.pointArrayMap || ["y"]).forEach(function (d) { d = "{point." + d; if (e || h) b = b.replace(RegExp(d + "}", "g"), e + d + "}" + h); b = b.replace(RegExp(d + "}", "g"), d + ":,." + c + "f}") }); return C(b, { point: this, series: this.series }, d.chart) }; l.prototype.update =
                    function (b, a, c, e) {
                        function d() {
                            h.applyOptions(b); var d = g && h.hasDummyGraphic; d = null === h.y ? !d : d; g && d && (h.graphic = g.destroy(), delete h.hasDummyGraphic); u(b, !0) && (g && g.element && b && b.marker && "undefined" !== typeof b.marker.symbol && (h.graphic = g.destroy()), b && b.dataLabels && h.dataLabel && (h.dataLabel = h.dataLabel.destroy()), h.connector && (h.connector = h.connector.destroy())); k = h.index; l.updateParallelArrays(h, k); f.data[k] = u(f.data[k], !0) || u(b, !0) ? h.options : r(b, f.data[k]); l.isDirty = l.isDirtyData = !0; !l.fixedBox &&
                                l.hasCartesianSeries && (m.isDirtyBox = !0); "point" === f.legendType && (m.isDirtyLegend = !0); a && m.redraw(c)
                        } var h = this, l = h.series, g = h.graphic, m = l.chart, f = l.options, k; a = r(a, !0); !1 === e ? d() : h.firePointEvent("update", { options: b }, d)
                    }; l.prototype.remove = function (b, a) { this.series.removePoint(this.series.data.indexOf(this), b, a) }; l.prototype.select = function (b, a) {
                        var d = this, c = d.series, e = c.chart; this.selectedStaging = b = r(b, !d.selected); d.firePointEvent(b ? "select" : "unselect", { accumulate: a }, function () {
                            d.selected = d.options.selected =
                                b; c.options.data[c.data.indexOf(d)] = d.options; d.setState(b && "select"); a || e.getSelectedPoints().forEach(function (b) { var a = b.series; b.selected && b !== d && (b.selected = b.options.selected = !1, a.options.data[a.data.indexOf(b)] = b.options, b.setState(e.hoverPoints && a.options.inactiveOtherPoints ? "inactive" : ""), b.firePointEvent("unselect")) })
                        }); delete this.selectedStaging
                    }; l.prototype.onMouseOver = function (b) {
                        var d = this.series.chart, a = d.pointer; b = b ? a.normalize(b) : a.getChartCoordinatesFromPoint(this, d.inverted); a.runPointActions(b,
                            this)
                    }; l.prototype.onMouseOut = function () { var b = this.series.chart; this.firePointEvent("mouseOut"); this.series.options.inactiveOtherPoints || (b.hoverPoints || []).forEach(function (b) { b.setState() }); b.hoverPoints = b.hoverPoint = null }; l.prototype.importEvents = function () { if (!this.hasImportedEvents) { var b = this, a = w(b.series.options.point, b.options).events; b.events = a; A(a, function (d, a) { t(d) && x(b, a, d) }); this.hasImportedEvents = !0 } }; l.prototype.setState = function (b, h) {
                        var d = this.series, l = this.state, g = d.options.states[b ||
                            "normal"] || {}, m = J.plotOptions[d.type].marker && d.options.marker, f = m && !1 === m.enabled, k = m && m.states && m.states[b || "normal"] || {}, n = !1 === k.enabled, p = this.marker || {}, G = d.chart, t = m && d.markerAttribs, w = d.halo, A, u = d.stateMarkerGraphic; b = b || ""; if (!(b === this.state && !h || this.selected && "select" !== b || !1 === g.enabled || b && (n || f && !1 === k.enabled) || b && p.states && p.states[b] && !1 === p.states[b].enabled)) {
                                this.state = b; t && (A = d.markerAttribs(this, b)); if (this.graphic && !this.hasDummyGraphic) {
                                    l && this.graphic.removeClass("highcharts-point-" +
                                        l); b && this.graphic.addClass("highcharts-point-" + b); if (!G.styledMode) { var x = d.pointAttribs(this, b); var E = r(G.options.chart.animation, g.animation); d.options.inactiveOtherPoints && v(x.opacity) && ((this.dataLabels || []).forEach(function (b) { b && b.animate({ opacity: x.opacity }, E) }), this.connector && this.connector.animate({ opacity: x.opacity }, E)); this.graphic.animate(x, E) } A && this.graphic.animate(A, r(G.options.chart.animation, k.animation, m.animation)); u && u.hide()
                                } else {
                                    if (b && k) {
                                        l = p.symbol || d.symbol; u && u.currentSymbol !==
                                            l && (u = u.destroy()); if (A) if (u) u[h ? "animate" : "attr"]({ x: A.x, y: A.y }); else l && (d.stateMarkerGraphic = u = G.renderer.symbol(l, A.x, A.y, A.width, A.height).add(d.markerGroup), u.currentSymbol = l); !G.styledMode && u && "inactive" !== this.state && u.attr(d.pointAttribs(this, b))
                                    } u && (u[b && this.isInside ? "show" : "hide"](), u.element.point = this, u.addClass(this.getClassName(), !0))
                                } g = g.halo; A = (u = this.graphic || u) && u.visibility || "inherit"; g && g.size && u && "hidden" !== A && !this.isCluster ? (w || (d.halo = w = G.renderer.path().add(u.parentGroup)),
                                    w.show()[h ? "animate" : "attr"]({ d: this.haloPath(g.size) }), w.attr({ "class": "highcharts-halo highcharts-color-" + r(this.colorIndex, d.colorIndex) + (this.className ? " " + this.className : ""), visibility: A, zIndex: -1 }), w.point = this, G.styledMode || w.attr(e({ fill: this.color || d.color, "fill-opacity": g.opacity }, a.filterUserAttributes(g.attributes || {})))) : w && w.point && w.point.haloPath && w.animate({ d: w.point.haloPath(0) }, null, w.hide); c(this, "afterSetState", { state: b })
                            }
                    }; l.prototype.haloPath = function (b) {
                        return this.series.chart.renderer.symbols.circle(Math.floor(this.plotX) -
                            b, this.plotY - b, 2 * b, 2 * b)
                    }; return l
            }(); ""; return f
        }); L(f, "Core/Pointer.js", [f["Core/Color/Color.js"], f["Core/Globals.js"], f["Core/Tooltip.js"], f["Core/Utilities.js"]], function (a, f, B, F) {
            var y = a.parse, D = f.charts, J = f.noop, C = F.addEvent, x = F.attr, p = F.css, g = F.defined, e = F.extend, c = F.find, n = F.fireEvent, k = F.isNumber, t = F.isObject, v = F.objectEach, u = F.offset, w = F.pick, A = F.splat; a = function () {
                function a(a, c) {
                    this.lastValidTouch = {}; this.pinchDown = []; this.runChartClick = !1; this.eventsToUnbind = []; this.chart = a; this.hasDragged =
                        !1; this.options = c; this.init(a, c)
                } a.prototype.applyInactiveState = function (a) { var c = [], b; (a || []).forEach(function (a) { b = a.series; c.push(b); b.linkedParent && c.push(b.linkedParent); b.linkedSeries && (c = c.concat(b.linkedSeries)); b.navigatorSeries && c.push(b.navigatorSeries) }); this.chart.series.forEach(function (b) { -1 === c.indexOf(b) ? b.setState("inactive", !0) : b.options.inactiveOtherPoints && b.setAllPointsToState("inactive") }) }; a.prototype.destroy = function () {
                    var c = this; this.eventsToUnbind.forEach(function (a) { return a() });
                    this.eventsToUnbind = []; f.chartCount || (a.unbindDocumentMouseUp && (a.unbindDocumentMouseUp = a.unbindDocumentMouseUp()), a.unbindDocumentTouchEnd && (a.unbindDocumentTouchEnd = a.unbindDocumentTouchEnd())); clearInterval(c.tooltipTimeout); v(c, function (a, b) { c[b] = void 0 })
                }; a.prototype.drag = function (a) {
                    var c = this.chart, b = c.options.chart, e = this.zoomHor, d = this.zoomVert, g = c.plotLeft, m = c.plotTop, f = c.plotWidth, k = c.plotHeight, n = this.mouseDownX || 0, p = this.mouseDownY || 0, r = t(b.panning) ? b.panning && b.panning.enabled : b.panning,
                    v = b.panKey && a[b.panKey + "Key"], w = a.chartX, A = a.chartY, u = this.selectionMarker; if (!u || !u.touch) if (w < g ? w = g : w > g + f && (w = g + f), A < m ? A = m : A > m + k && (A = m + k), this.hasDragged = Math.sqrt(Math.pow(n - w, 2) + Math.pow(p - A, 2)), 10 < this.hasDragged) {
                        var x = c.isInsidePlot(n - g, p - m, { visiblePlotOnly: !0 }); !c.hasCartesianSeries && !c.mapView || !this.zoomX && !this.zoomY || !x || v || u || (this.selectionMarker = u = c.renderer.rect(g, m, e ? 1 : f, d ? 1 : k, 0).attr({ "class": "highcharts-selection-marker", zIndex: 7 }).add(), c.styledMode || u.attr({
                            fill: b.selectionMarkerFill ||
                                y("#335cad").setOpacity(.25).get()
                        })); u && e && (e = w - n, u.attr({ width: Math.abs(e), x: (0 < e ? 0 : e) + n })); u && d && (e = A - p, u.attr({ height: Math.abs(e), y: (0 < e ? 0 : e) + p })); x && !u && r && c.pan(a, b.panning)
                    }
                }; a.prototype.dragStart = function (a) { var c = this.chart; c.mouseIsDown = a.type; c.cancelClick = !1; c.mouseDownX = this.mouseDownX = a.chartX; c.mouseDownY = this.mouseDownY = a.chartY }; a.prototype.drop = function (a) {
                    var c = this, b = this.chart, l = this.hasPinched; if (this.selectionMarker) {
                        var d = this.selectionMarker, m = d.attr ? d.attr("x") : d.x, f = d.attr ?
                            d.attr("y") : d.y, q = d.attr ? d.attr("width") : d.width, r = d.attr ? d.attr("height") : d.height, v = { originalEvent: a, xAxis: [], yAxis: [], x: m, y: f, width: q, height: r }, w = !!b.mapView; if (this.hasDragged || l) b.axes.forEach(function (b) { if (b.zoomEnabled && g(b.min) && (l || c[{ xAxis: "zoomX", yAxis: "zoomY" }[b.coll]]) && k(m) && k(f)) { var d = b.horiz, e = "touchend" === a.type ? b.minPixelPadding : 0, h = b.toValue((d ? m : f) + e); d = b.toValue((d ? m + q : f + r) - e); v[b.coll].push({ axis: b, min: Math.min(h, d), max: Math.max(h, d) }); w = !0 } }), w && n(b, "selection", v, function (d) {
                                b.zoom(e(d,
                                    l ? { animation: !1 } : null))
                            }); k(b.index) && (this.selectionMarker = this.selectionMarker.destroy()); l && this.scaleGroups()
                    } b && k(b.index) && (p(b.container, { cursor: b._cursor }), b.cancelClick = 10 < this.hasDragged, b.mouseIsDown = this.hasDragged = this.hasPinched = !1, this.pinchDown = [])
                }; a.prototype.findNearestKDPoint = function (a, c, b) {
                    var e = this.chart, d = e.hoverPoint; e = e.tooltip; if (d && e && e.isStickyOnContact()) return d; var h; a.forEach(function (d) {
                        var a = !(d.noSharedTooltip && c) && 0 > d.options.findNearestPointBy.indexOf("y"); d =
                            d.searchPoint(b, a); if ((a = t(d, !0) && d.series) && !(a = !t(h, !0))) { a = h.distX - d.distX; var e = h.dist - d.dist, l = (d.series.group && d.series.group.zIndex) - (h.series.group && h.series.group.zIndex); a = 0 < (0 !== a && c ? a : 0 !== e ? e : 0 !== l ? l : h.series.index > d.series.index ? -1 : 1) } a && (h = d)
                    }); return h
                }; a.prototype.getChartCoordinatesFromPoint = function (a, c) {
                    var b = a.series, e = b.xAxis; b = b.yAxis; var d = a.shapeArgs; if (e && b) {
                        var h = w(a.clientX, a.plotX), g = a.plotY || 0; a.isNode && d && k(d.x) && k(d.y) && (h = d.x, g = d.y); return c ? {
                            chartX: b.len + b.pos - g, chartY: e.len +
                                e.pos - h
                        } : { chartX: h + e.pos, chartY: g + b.pos }
                    } if (d && d.x && d.y) return { chartX: d.x, chartY: d.y }
                }; a.prototype.getChartPosition = function () { if (this.chartPosition) return this.chartPosition; var a = this.chart.container, c = u(a); this.chartPosition = { left: c.left, top: c.top, scaleX: 1, scaleY: 1 }; var b = a.offsetWidth; a = a.offsetHeight; 2 < b && 2 < a && (this.chartPosition.scaleX = c.width / b, this.chartPosition.scaleY = c.height / a); return this.chartPosition }; a.prototype.getCoordinates = function (a) {
                    var c = { xAxis: [], yAxis: [] }; this.chart.axes.forEach(function (b) {
                        c[b.isXAxis ?
                            "xAxis" : "yAxis"].push({ axis: b, value: b.toValue(a[b.horiz ? "chartX" : "chartY"]) })
                    }); return c
                }; a.prototype.getHoverData = function (a, e, b, l, d, g) {
                    var h = []; l = !(!l || !a); var m = { chartX: g ? g.chartX : void 0, chartY: g ? g.chartY : void 0, shared: d }; n(this, "beforeGetHoverData", m); var f = e && !e.stickyTracking ? [e] : b.filter(function (b) { return m.filter ? m.filter(b) : b.visible && !(!d && b.directTouch) && w(b.options.enableMouseTracking, !0) && b.stickyTracking }); var k = l || !g ? a : this.findNearestKDPoint(f, d, g); e = k && k.series; k && (d && !e.noSharedTooltip ?
                        (f = b.filter(function (b) { return m.filter ? m.filter(b) : b.visible && !(!d && b.directTouch) && w(b.options.enableMouseTracking, !0) && !b.noSharedTooltip }), f.forEach(function (b) { var d = c(b.points, function (b) { return b.x === k.x && !b.isNull }); t(d) && (b.chart.isBoosting && (d = b.getPoint(d)), h.push(d)) })) : h.push(k)); m = { hoverPoint: k }; n(this, "afterGetHoverData", m); return { hoverPoint: m.hoverPoint, hoverSeries: e, hoverPoints: h }
                }; a.prototype.getPointFromEvent = function (a) { a = a.target; for (var c; a && !c;)c = a.point, a = a.parentNode; return c };
                a.prototype.onTrackerMouseOut = function (a) { a = a.relatedTarget || a.toElement; var c = this.chart.hoverSeries; this.isDirectTouch = !1; if (!(!c || !a || c.stickyTracking || this.inClass(a, "highcharts-tooltip") || this.inClass(a, "highcharts-series-" + c.index) && this.inClass(a, "highcharts-tracker"))) c.onMouseOut() }; a.prototype.inClass = function (a, c) { for (var b; a;) { if (b = x(a, "class")) { if (-1 !== b.indexOf(c)) return !0; if (-1 !== b.indexOf("highcharts-container")) return !1 } a = a.parentElement } }; a.prototype.init = function (a, c) {
                    this.options =
                    c; this.chart = a; this.runChartClick = !(!c.chart.events || !c.chart.events.click); this.pinchDown = []; this.lastValidTouch = {}; B && (a.tooltip = new B(a, c.tooltip), this.followTouchMove = w(c.tooltip.followTouchMove, !0)); this.setDOMEvents()
                }; a.prototype.normalize = function (a, c) { var b = a.touches, h = b ? b.length ? b.item(0) : w(b.changedTouches, a.changedTouches)[0] : a; c || (c = this.getChartPosition()); b = h.pageX - c.left; h = h.pageY - c.top; b /= c.scaleX; h /= c.scaleY; return e(a, { chartX: Math.round(b), chartY: Math.round(h) }) }; a.prototype.onContainerClick =
                    function (a) { var c = this.chart, b = c.hoverPoint; a = this.normalize(a); var g = c.plotLeft, d = c.plotTop; c.cancelClick || (b && this.inClass(a.target, "highcharts-tracker") ? (n(b.series, "click", e(a, { point: b })), c.hoverPoint && b.firePointEvent("click", a)) : (e(a, this.getCoordinates(a)), c.isInsidePlot(a.chartX - g, a.chartY - d, { visiblePlotOnly: !0 }) && n(c, "click", a))) }; a.prototype.onContainerMouseDown = function (a) {
                        var c = 1 === ((a.buttons || a.button) & 1); a = this.normalize(a); if (f.isFirefox && 0 !== a.button) this.onContainerMouseMove(a);
                        if ("undefined" === typeof a.button || c) this.zoomOption(a), c && a.preventDefault && a.preventDefault(), this.dragStart(a)
                    }; a.prototype.onContainerMouseLeave = function (c) { var e = D[w(a.hoverChartIndex, -1)], b = this.chart.tooltip; b && b.shouldStickOnContact() && this.inClass(c.relatedTarget, "highcharts-tooltip-container") || (c = this.normalize(c), e && (c.relatedTarget || c.toElement) && (e.pointer.reset(), e.pointer.chartPosition = void 0), b && !b.isHidden && this.reset()) }; a.prototype.onContainerMouseEnter = function (a) { delete this.chartPosition };
                a.prototype.onContainerMouseMove = function (a) { var c = this.chart; a = this.normalize(a); this.setHoverChartIndex(); a.preventDefault || (a.returnValue = !1); ("mousedown" === c.mouseIsDown || this.touchSelect(a)) && this.drag(a); c.openMenu || !this.inClass(a.target, "highcharts-tracker") && !c.isInsidePlot(a.chartX - c.plotLeft, a.chartY - c.plotTop, { visiblePlotOnly: !0 }) || (this.inClass(a.target, "highcharts-no-tooltip") ? this.reset(!1, 0) : this.runPointActions(a)) }; a.prototype.onDocumentTouchEnd = function (c) {
                    var e = D[w(a.hoverChartIndex,
                        -1)]; e && e.pointer.drop(c)
                }; a.prototype.onContainerTouchMove = function (a) { if (this.touchSelect(a)) this.onContainerMouseMove(a); else this.touch(a) }; a.prototype.onContainerTouchStart = function (a) { if (this.touchSelect(a)) this.onContainerMouseDown(a); else this.zoomOption(a), this.touch(a, !0) }; a.prototype.onDocumentMouseMove = function (a) {
                    var c = this.chart, b = this.chartPosition; a = this.normalize(a, b); var e = c.tooltip; !b || e && e.isStickyOnContact() || c.isInsidePlot(a.chartX - c.plotLeft, a.chartY - c.plotTop, { visiblePlotOnly: !0 }) ||
                        this.inClass(a.target, "highcharts-tracker") || this.reset()
                }; a.prototype.onDocumentMouseUp = function (c) { var e = D[w(a.hoverChartIndex, -1)]; e && e.pointer.drop(c) }; a.prototype.pinch = function (a) {
                    var c = this, b = c.chart, g = c.pinchDown, d = a.touches || [], f = d.length, k = c.lastValidTouch, m = c.hasZoom, p = {}, r = 1 === f && (c.inClass(a.target, "highcharts-tracker") && b.runTrackerClick || c.runChartClick), v = {}, t = c.selectionMarker; 1 < f ? c.initiated = !0 : 1 === f && this.followTouchMove && (c.initiated = !1); m && c.initiated && !r && !1 !== a.cancelable &&
                        a.preventDefault();[].map.call(d, function (b) { return c.normalize(b) }); "touchstart" === a.type ? ([].forEach.call(d, function (b, a) { g[a] = { chartX: b.chartX, chartY: b.chartY } }), k.x = [g[0].chartX, g[1] && g[1].chartX], k.y = [g[0].chartY, g[1] && g[1].chartY], b.axes.forEach(function (a) {
                            if (a.zoomEnabled) {
                                var d = b.bounds[a.horiz ? "h" : "v"], c = a.minPixelPadding, e = a.toPixels(Math.min(w(a.options.min, a.dataMin), a.dataMin)), h = a.toPixels(Math.max(w(a.options.max, a.dataMax), a.dataMax)), g = Math.max(e, h); d.min = Math.min(a.pos, Math.min(e,
                                    h) - c); d.max = Math.max(a.pos + a.len, g + c)
                            }
                        }), c.res = !0) : c.followTouchMove && 1 === f ? this.runPointActions(c.normalize(a)) : g.length && (n(b, "touchpan", { originalEvent: a }, function () { t || (c.selectionMarker = t = e({ destroy: J, touch: !0 }, b.plotBox)); c.pinchTranslate(g, d, p, t, v, k); c.hasPinched = m; c.scaleGroups(p, v) }), c.res && (c.res = !1, this.reset(!1, 0)))
                }; a.prototype.pinchTranslate = function (a, c, b, e, d, g) { this.zoomHor && this.pinchTranslateDirection(!0, a, c, b, e, d, g); this.zoomVert && this.pinchTranslateDirection(!1, a, c, b, e, d, g) }; a.prototype.pinchTranslateDirection =
                    function (a, c, b, e, d, g, f, k) {
                        var h = this.chart, l = a ? "x" : "y", m = a ? "X" : "Y", q = "chart" + m, n = a ? "width" : "height", p = h["plot" + (a ? "Left" : "Top")], r = h.inverted, z = h.bounds[a ? "h" : "v"], v = 1 === c.length, w = c[0][q], G = !v && c[1][q]; c = function () { "number" === typeof u && 20 < Math.abs(w - G) && (A = k || Math.abs(M - u) / Math.abs(w - G)); E = (p - M) / A + w; t = h["plot" + (a ? "Width" : "Height")] / A }; var t, E, A = k || 1, M = b[0][q], u = !v && b[1][q]; c(); b = E; if (b < z.min) { b = z.min; var x = !0 } else b + t > z.max && (b = z.max - t, x = !0); x ? (M -= .8 * (M - f[l][0]), "number" === typeof u && (u -= .8 * (u - f[l][1])),
                            c()) : f[l] = [M, u]; r || (g[l] = E - p, g[n] = t); g = r ? 1 / A : A; d[n] = t; d[l] = b; e[r ? a ? "scaleY" : "scaleX" : "scale" + m] = A; e["translate" + m] = g * p + (M - g * w)
                    }; a.prototype.reset = function (a, c) {
                        var b = this.chart, e = b.hoverSeries, d = b.hoverPoint, h = b.hoverPoints, g = b.tooltip, f = g && g.shared ? h : d; a && f && A(f).forEach(function (b) { b.series.isCartesian && "undefined" === typeof b.plotX && (a = !1) }); if (a) g && f && A(f).length && (g.refresh(f), g.shared && h ? h.forEach(function (b) {
                            b.setState(b.state, !0); b.series.isCartesian && (b.series.xAxis.crosshair && b.series.xAxis.drawCrosshair(null,
                                b), b.series.yAxis.crosshair && b.series.yAxis.drawCrosshair(null, b))
                        }) : d && (d.setState(d.state, !0), b.axes.forEach(function (b) { b.crosshair && d.series[b.coll] === b && b.drawCrosshair(null, d) }))); else { if (d) d.onMouseOut(); h && h.forEach(function (b) { b.setState() }); if (e) e.onMouseOut(); g && g.hide(c); this.unDocMouseMove && (this.unDocMouseMove = this.unDocMouseMove()); b.axes.forEach(function (b) { b.hideCrosshair() }); this.hoverX = b.hoverPoints = b.hoverPoint = null }
                    }; a.prototype.runPointActions = function (e, h) {
                        var b = this.chart,
                        g = b.tooltip && b.tooltip.options.enabled ? b.tooltip : void 0, d = g ? g.shared : !1, f = h || b.hoverPoint, k = f && f.series || b.hoverSeries; h = this.getHoverData(f, k, b.series, (!e || "touchmove" !== e.type) && (!!h || k && k.directTouch && this.isDirectTouch), d, e); f = h.hoverPoint; k = h.hoverSeries; var m = h.hoverPoints; h = k && k.tooltipOptions.followPointer && !k.tooltipOptions.split; var n = d && k && !k.noSharedTooltip; if (f && (f !== b.hoverPoint || g && g.isHidden)) {
                            (b.hoverPoints || []).forEach(function (b) { -1 === m.indexOf(b) && b.setState() }); if (b.hoverSeries !==
                                k) k.onMouseOver(); this.applyInactiveState(m); (m || []).forEach(function (b) { b.setState("hover") }); b.hoverPoint && b.hoverPoint.firePointEvent("mouseOut"); if (!f.series) return; b.hoverPoints = m; b.hoverPoint = f; f.firePointEvent("mouseOver", void 0, function () { g && f && g.refresh(n ? m : f, e) })
                        } else h && g && !g.isHidden && (d = g.getAnchor([{}], e), b.isInsidePlot(d[0], d[1], { visiblePlotOnly: !0 }) && g.updatePosition({ plotX: d[0], plotY: d[1] })); this.unDocMouseMove || (this.unDocMouseMove = C(b.container.ownerDocument, "mousemove", function (b) {
                            var d =
                                D[a.hoverChartIndex]; if (d) d.pointer.onDocumentMouseMove(b)
                        }), this.eventsToUnbind.push(this.unDocMouseMove)); b.axes.forEach(function (a) { var d = w((a.crosshair || {}).snap, !0), h; d && ((h = b.hoverPoint) && h.series[a.coll] === a || (h = c(m, function (b) { return b.series && b.series[a.coll] === a }))); h || !d ? a.drawCrosshair(e, h) : a.hideCrosshair() })
                    }; a.prototype.scaleGroups = function (a, c) {
                        var b = this.chart; b.series.forEach(function (e) {
                            var d = a || e.getPlotBox(); e.group && (e.xAxis && e.xAxis.zoomEnabled || b.mapView) && (e.group.attr(d),
                                e.markerGroup && (e.markerGroup.attr(d), e.markerGroup.clip(c ? b.clipRect : null)), e.dataLabelsGroup && e.dataLabelsGroup.attr(d))
                        }); b.clipRect.attr(c || b.clipBox)
                    }; a.prototype.setDOMEvents = function () {
                        var c = this, e = this.chart.container, b = e.ownerDocument; e.onmousedown = this.onContainerMouseDown.bind(this); e.onmousemove = this.onContainerMouseMove.bind(this); e.onclick = this.onContainerClick.bind(this); this.eventsToUnbind.push(C(e, "mouseenter", this.onContainerMouseEnter.bind(this))); this.eventsToUnbind.push(C(e, "mouseleave",
                            this.onContainerMouseLeave.bind(this))); a.unbindDocumentMouseUp || (a.unbindDocumentMouseUp = C(b, "mouseup", this.onDocumentMouseUp.bind(this))); for (var g = this.chart.renderTo.parentElement; g && "BODY" !== g.tagName;)this.eventsToUnbind.push(C(g, "scroll", function () { delete c.chartPosition })), g = g.parentElement; f.hasTouch && (this.eventsToUnbind.push(C(e, "touchstart", this.onContainerTouchStart.bind(this), { passive: !1 })), this.eventsToUnbind.push(C(e, "touchmove", this.onContainerTouchMove.bind(this), { passive: !1 })),
                                a.unbindDocumentTouchEnd || (a.unbindDocumentTouchEnd = C(b, "touchend", this.onDocumentTouchEnd.bind(this), { passive: !1 })))
                    }; a.prototype.setHoverChartIndex = function () { var c = this.chart, e = f.charts[w(a.hoverChartIndex, -1)]; if (e && e !== c) e.pointer.onContainerMouseLeave({ relatedTarget: c.container }); e && e.mouseIsDown || (a.hoverChartIndex = c.index) }; a.prototype.touch = function (a, c) {
                        var b = this.chart, e; this.setHoverChartIndex(); if (1 === a.touches.length) if (a = this.normalize(a), (e = b.isInsidePlot(a.chartX - b.plotLeft, a.chartY -
                            b.plotTop, { visiblePlotOnly: !0 })) && !b.openMenu) { c && this.runPointActions(a); if ("touchmove" === a.type) { c = this.pinchDown; var d = c[0] ? 4 <= Math.sqrt(Math.pow(c[0].chartX - a.chartX, 2) + Math.pow(c[0].chartY - a.chartY, 2)) : !1 } w(d, !0) && this.pinch(a) } else c && this.reset(); else 2 === a.touches.length && this.pinch(a)
                    }; a.prototype.touchSelect = function (a) { return !(!this.chart.options.chart.zoomBySingleTouch || !a.touches || 1 !== a.touches.length) }; a.prototype.zoomOption = function (a) {
                        var c = this.chart, b = c.options.chart; c = c.inverted;
                        var e = b.zoomType || ""; /touch/.test(a.type) && (e = w(b.pinchType, e)); this.zoomX = a = /x/.test(e); this.zoomY = b = /y/.test(e); this.zoomHor = a && !c || b && c; this.zoomVert = b && !c || a && c; this.hasZoom = a || b
                    }; return a
            }(); ""; return a
        }); L(f, "Core/MSPointer.js", [f["Core/Globals.js"], f["Core/Pointer.js"], f["Core/Utilities.js"]], function (a, f, B) {
            function D() { var a = []; a.item = function (a) { return this[a] }; c(k, function (c) { a.push({ pageX: c.pageX, pageY: c.pageY, target: c.target }) }); return a } function y(a, c, e, g) {
                var k = J[f.hoverChartIndex ||
                    NaN]; "touch" !== a.pointerType && a.pointerType !== a.MSPOINTER_TYPE_TOUCH || !k || (k = k.pointer, g(a), k[c]({ type: e, target: a.currentTarget, preventDefault: x, touches: D() }))
            } var I = this && this.__extends || function () {
                var a = function (c, e) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, c) { a.__proto__ = c } || function (a, c) { for (var e in c) c.hasOwnProperty(e) && (a[e] = c[e]) }; return a(c, e) }; return function (c, e) {
                    function g() { this.constructor = c } a(c, e); c.prototype = null === e ? Object.create(e) : (g.prototype = e.prototype,
                        new g)
                }
            }(), J = a.charts, C = a.doc, x = a.noop, p = a.win, g = B.addEvent, e = B.css, c = B.objectEach, n = B.removeEvent, k = {}, t = !!p.PointerEvent; return function (c) {
                function f() { return null !== c && c.apply(this, arguments) || this } I(f, c); f.isRequired = function () { return !(a.hasTouch || !p.PointerEvent && !p.MSPointerEvent) }; f.prototype.batchMSEvents = function (a) {
                    a(this.chart.container, t ? "pointerdown" : "MSPointerDown", this.onContainerPointerDown); a(this.chart.container, t ? "pointermove" : "MSPointerMove", this.onContainerPointerMove); a(C, t ?
                        "pointerup" : "MSPointerUp", this.onDocumentPointerUp)
                }; f.prototype.destroy = function () { this.batchMSEvents(n); c.prototype.destroy.call(this) }; f.prototype.init = function (a, g) { c.prototype.init.call(this, a, g); this.hasZoom && e(a.container, { "-ms-touch-action": "none", "touch-action": "none" }) }; f.prototype.onContainerPointerDown = function (a) { y(a, "onContainerTouchStart", "touchstart", function (a) { k[a.pointerId] = { pageX: a.pageX, pageY: a.pageY, target: a.currentTarget } }) }; f.prototype.onContainerPointerMove = function (a) {
                    y(a,
                        "onContainerTouchMove", "touchmove", function (a) { k[a.pointerId] = { pageX: a.pageX, pageY: a.pageY }; k[a.pointerId].target || (k[a.pointerId].target = a.currentTarget) })
                }; f.prototype.onDocumentPointerUp = function (a) { y(a, "onDocumentTouchEnd", "touchend", function (a) { delete k[a.pointerId] }) }; f.prototype.setDOMEvents = function () { c.prototype.setDOMEvents.call(this); (this.hasZoom || this.followTouchMove) && this.batchMSEvents(g) }; return f
            }(f)
        }); L(f, "Core/Legend/Legend.js", [f["Core/Animation/AnimationUtilities.js"], f["Core/FormatUtilities.js"],
        f["Core/Globals.js"], f["Core/Series/Point.js"], f["Core/Renderer/RendererUtilities.js"], f["Core/Utilities.js"]], function (a, f, B, F, y, I) {
            var D = a.animObject, C = a.setAnimation, x = f.format; a = B.isFirefox; var p = B.marginNames; B = B.win; var g = y.distribute, e = I.addEvent, c = I.createElement, n = I.css, k = I.defined, t = I.discardElement, v = I.find, u = I.fireEvent, w = I.isNumber, A = I.merge, r = I.pick, m = I.relativeLength, h = I.stableSort, b = I.syncTimeout; y = I.wrap; I = function () {
                function a(b, a) {
                    this.allItems = []; this.contentGroup = this.box = void 0;
                    this.display = !1; this.group = void 0; this.offsetWidth = this.maxLegendWidth = this.maxItemWidth = this.legendWidth = this.legendHeight = this.lastLineHeight = this.lastItemY = this.itemY = this.itemX = this.itemMarginTop = this.itemMarginBottom = this.itemHeight = this.initialItemY = 0; this.options = void 0; this.padding = 0; this.pages = []; this.proximate = !1; this.scrollGroup = void 0; this.widthOption = this.totalItemWidth = this.titleHeight = this.symbolWidth = this.symbolHeight = 0; this.chart = b; this.init(b, a)
                } a.prototype.init = function (b, a) {
                    this.chart =
                    b; this.setOptions(a); a.enabled && (this.render(), e(this.chart, "endResize", function () { this.legend.positionCheckboxes() }), this.proximate ? this.unchartrender = e(this.chart, "render", function () { this.legend.proximatePositions(); this.legend.positionItems() }) : this.unchartrender && this.unchartrender())
                }; a.prototype.setOptions = function (b) {
                    var a = r(b.padding, 8); this.options = b; this.chart.styledMode || (this.itemStyle = b.itemStyle, this.itemHiddenStyle = A(this.itemStyle, b.itemHiddenStyle)); this.itemMarginTop = b.itemMarginTop ||
                        0; this.itemMarginBottom = b.itemMarginBottom || 0; this.padding = a; this.initialItemY = a - 5; this.symbolWidth = r(b.symbolWidth, 16); this.pages = []; this.proximate = "proximate" === b.layout && !this.chart.inverted; this.baseline = void 0
                }; a.prototype.update = function (b, a) { var d = this.chart; this.setOptions(A(!0, this.options, b)); this.destroy(); d.isDirtyLegend = d.isDirtyBox = !0; r(a, !0) && d.redraw(); u(this, "afterUpdate") }; a.prototype.colorizeItem = function (b, a) {
                    b.legendGroup[a ? "removeClass" : "addClass"]("highcharts-legend-item-hidden");
                    if (!this.chart.styledMode) { var d = this.options, c = b.legendItem, e = b.legendLine, g = b.legendSymbol, h = this.itemHiddenStyle.color; d = a ? d.itemStyle.color : h; var f = a ? b.color || h : h, l = b.options && b.options.marker, k = { fill: f }; c && c.css({ fill: d, color: d }); e && e.attr({ stroke: f }); g && (l && g.isMarker && (k = b.pointAttribs(), a || (k.stroke = k.fill = h)), g.attr(k)) } u(this, "afterColorizeItem", { item: b, visible: a })
                }; a.prototype.positionItems = function () { this.allItems.forEach(this.positionItem, this); this.chart.isResizing || this.positionCheckboxes() };
                a.prototype.positionItem = function (b) { var a = this, d = this.options, c = d.symbolPadding, e = !d.rtl, g = b._legendItemPos; d = g[0]; g = g[1]; var h = b.checkbox, f = b.legendGroup; f && f.element && (c = { translateX: e ? d : this.legendWidth - d - 2 * c - 4, translateY: g }, e = function () { u(a, "afterPositionItem", { item: b }) }, k(f.translateY) ? f.animate(c, void 0, e) : (f.attr(c), e())); h && (h.x = d, h.y = g) }; a.prototype.destroyItem = function (b) {
                    var a = b.checkbox;["legendItem", "legendLine", "legendSymbol", "legendGroup"].forEach(function (a) { b[a] && (b[a] = b[a].destroy()) });
                    a && t(b.checkbox)
                }; a.prototype.destroy = function () { function b(b) { this[b] && (this[b] = this[b].destroy()) } this.getAllItems().forEach(function (a) { ["legendItem", "legendGroup"].forEach(b, a) }); "clipRect up down pager nav box title group".split(" ").forEach(b, this); this.display = null }; a.prototype.positionCheckboxes = function () {
                    var b = this.group && this.group.alignAttr, a = this.clipHeight || this.legendHeight, c = this.titleHeight; if (b) {
                        var e = b.translateY; this.allItems.forEach(function (d) {
                            var g = d.checkbox; if (g) {
                                var h = e +
                                    c + g.y + (this.scrollOffset || 0) + 3; n(g, { left: b.translateX + d.checkboxOffset + g.x - 20 + "px", top: h + "px", display: this.proximate || h > e - 6 && h < e + a - 6 ? "" : "none" })
                            }
                        }, this)
                    }
                }; a.prototype.renderTitle = function () {
                    var b = this.options, a = this.padding, c = b.title, e = 0; c.text && (this.title || (this.title = this.chart.renderer.label(c.text, a - 3, a - 4, void 0, void 0, void 0, b.useHTML, void 0, "legend-title").attr({ zIndex: 1 }), this.chart.styledMode || this.title.css(c.style), this.title.add(this.group)), c.width || this.title.css({
                        width: this.maxLegendWidth +
                            "px"
                    }), b = this.title.getBBox(), e = b.height, this.offsetWidth = b.width, this.contentGroup.attr({ translateY: e })); this.titleHeight = e
                }; a.prototype.setText = function (b) { var a = this.options; b.legendItem.attr({ text: a.labelFormat ? x(a.labelFormat, b, this.chart) : a.labelFormatter.call(b) }) }; a.prototype.renderItem = function (b) {
                    var a = this.chart, d = a.renderer, c = this.options, e = this.symbolWidth, g = c.symbolPadding || 0, h = this.itemStyle, f = this.itemHiddenStyle, l = "horizontal" === c.layout ? r(c.itemDistance, 20) : 0, k = !c.rtl, m = !b.series,
                    n = !m && b.series.drawLegendSymbol ? b.series : b, p = n.options, v = this.createCheckboxForItem && p && p.showCheckbox, t = c.useHTML, w = b.options.className, E = b.legendItem; p = e + g + l + (v ? 20 : 0); E || (b.legendGroup = d.g("legend-item").addClass("highcharts-" + n.type + "-series highcharts-color-" + b.colorIndex + (w ? " " + w : "") + (m ? " highcharts-series-" + b.index : "")).attr({ zIndex: 1 }).add(this.scrollGroup), b.legendItem = E = d.text("", k ? e + g : -g, this.baseline || 0, t), a.styledMode || E.css(A(b.visible ? h : f)), E.attr({ align: k ? "left" : "right", zIndex: 2 }).add(b.legendGroup),
                        this.baseline || (this.fontMetrics = d.fontMetrics(a.styledMode ? 12 : h.fontSize, E), this.baseline = this.fontMetrics.f + 3 + this.itemMarginTop, E.attr("y", this.baseline), this.symbolHeight = c.symbolHeight || this.fontMetrics.f, c.squareSymbol && (this.symbolWidth = r(c.symbolWidth, Math.max(this.symbolHeight, 16)), p = this.symbolWidth + g + l + (v ? 20 : 0), k && E.attr("x", this.symbolWidth + g))), n.drawLegendSymbol(this, b), this.setItemEvents && this.setItemEvents(b, E, t)); v && !b.checkbox && this.createCheckboxForItem && this.createCheckboxForItem(b);
                    this.colorizeItem(b, b.visible); !a.styledMode && h.width || E.css({ width: (c.itemWidth || this.widthOption || a.spacingBox.width) - p + "px" }); this.setText(b); a = E.getBBox(); d = this.fontMetrics && this.fontMetrics.h || 0; b.itemWidth = b.checkboxOffset = c.itemWidth || b.legendItemWidth || a.width + p; this.maxItemWidth = Math.max(this.maxItemWidth, b.itemWidth); this.totalItemWidth += b.itemWidth; this.itemHeight = b.itemHeight = Math.round(b.legendItemHeight || (a.height > 1.5 * d ? a.height : d))
                }; a.prototype.layoutItem = function (b) {
                    var a = this.options,
                    c = this.padding, d = "horizontal" === a.layout, e = b.itemHeight, g = this.itemMarginBottom, h = this.itemMarginTop, f = d ? r(a.itemDistance, 20) : 0, l = this.maxLegendWidth; a = a.alignColumns && this.totalItemWidth > l ? this.maxItemWidth : b.itemWidth; d && this.itemX - c + a > l && (this.itemX = c, this.lastLineHeight && (this.itemY += h + this.lastLineHeight + g), this.lastLineHeight = 0); this.lastItemY = h + this.itemY + g; this.lastLineHeight = Math.max(e, this.lastLineHeight); b._legendItemPos = [this.itemX, this.itemY]; d ? this.itemX += a : (this.itemY += h + e + g, this.lastLineHeight =
                        e); this.offsetWidth = this.widthOption || Math.max((d ? this.itemX - c - (b.checkbox ? 0 : f) : a) + c, this.offsetWidth)
                }; a.prototype.getAllItems = function () { var b = []; this.chart.series.forEach(function (a) { var c = a && a.options; a && r(c.showInLegend, k(c.linkedTo) ? !1 : void 0, !0) && (b = b.concat(a.legendItems || ("point" === c.legendType ? a.data : a))) }); u(this, "afterGetAllItems", { allItems: b }); return b }; a.prototype.getAlignment = function () {
                    var b = this.options; return this.proximate ? b.align.charAt(0) + "tv" : b.floating ? "" : b.align.charAt(0) +
                        b.verticalAlign.charAt(0) + b.layout.charAt(0)
                }; a.prototype.adjustMargins = function (b, a) { var c = this.chart, d = this.options, e = this.getAlignment(); e && [/(lth|ct|rth)/, /(rtv|rm|rbv)/, /(rbh|cb|lbh)/, /(lbv|lm|ltv)/].forEach(function (g, h) { g.test(e) && !k(b[h]) && (c[p[h]] = Math.max(c[p[h]], c.legend[(h + 1) % 2 ? "legendHeight" : "legendWidth"] + [1, -1, -1, 1][h] * d[h % 2 ? "x" : "y"] + r(d.margin, 12) + a[h] + (c.titleOffset[h] || 0))) }) }; a.prototype.proximatePositions = function () {
                    var b = this.chart, a = [], c = "left" === this.options.align; this.allItems.forEach(function (d) {
                        var e;
                        var g = c; if (d.yAxis) { d.xAxis.options.reversed && (g = !g); d.points && (e = v(g ? d.points : d.points.slice(0).reverse(), function (b) { return w(b.plotY) })); g = this.itemMarginTop + d.legendItem.getBBox().height + this.itemMarginBottom; var h = d.yAxis.top - b.plotTop; d.visible ? (e = e ? e.plotY : d.yAxis.height, e += h - .3 * g) : e = h + d.yAxis.height; a.push({ target: e, size: g, item: d }) }
                    }, this); g(a, b.plotHeight).forEach(function (a) { a.item._legendItemPos && a.pos && (a.item._legendItemPos[1] = b.plotTop - b.spacing[0] + a.pos) })
                }; a.prototype.render = function () {
                    var b =
                        this.chart, a = b.renderer, c = this.options, e = this.padding, g = this.getAllItems(), f = this.group, l = this.box; this.itemX = e; this.itemY = this.initialItemY; this.lastItemY = this.offsetWidth = 0; this.widthOption = m(c.width, b.spacingBox.width - e); var k = b.spacingBox.width - 2 * e - c.x; -1 < ["rm", "lm"].indexOf(this.getAlignment().substring(0, 2)) && (k /= 2); this.maxLegendWidth = this.widthOption || k; f || (this.group = f = a.g("legend").addClass(c.className || "").attr({ zIndex: 7 }).add(), this.contentGroup = a.g().attr({ zIndex: 1 }).add(f), this.scrollGroup =
                            a.g().add(this.contentGroup)); this.renderTitle(); h(g, function (b, a) { return (b.options && b.options.legendIndex || 0) - (a.options && a.options.legendIndex || 0) }); c.reversed && g.reverse(); this.allItems = g; this.display = k = !!g.length; this.itemHeight = this.totalItemWidth = this.maxItemWidth = this.lastLineHeight = 0; g.forEach(this.renderItem, this); g.forEach(this.layoutItem, this); g = (this.widthOption || this.offsetWidth) + e; var n = this.lastItemY + this.lastLineHeight + this.titleHeight; n = this.handleOverflow(n); n += e; l || (this.box = l =
                                a.rect().addClass("highcharts-legend-box").attr({ r: c.borderRadius }).add(f)); b.styledMode || l.attr({ stroke: c.borderColor, "stroke-width": c.borderWidth || 0, fill: c.backgroundColor || "none" }).shadow(c.shadow); if (0 < g && 0 < n) l[l.placed ? "animate" : "attr"](l.crisp.call({}, { x: 0, y: 0, width: g, height: n }, l.strokeWidth())); l[k ? "show" : "hide"](); b.styledMode && "none" === f.getStyle("display") && (g = n = 0); this.legendWidth = g; this.legendHeight = n; k && this.align(); this.proximate || this.positionItems(); u(this, "afterRender")
                }; a.prototype.align =
                    function (b) { void 0 === b && (b = this.chart.spacingBox); var a = this.chart, c = this.options, d = b.y; /(lth|ct|rth)/.test(this.getAlignment()) && 0 < a.titleOffset[0] ? d += a.titleOffset[0] : /(lbh|cb|rbh)/.test(this.getAlignment()) && 0 < a.titleOffset[2] && (d -= a.titleOffset[2]); d !== b.y && (b = A(b, { y: d })); a.hasRendered || (this.group.placed = !1); this.group.align(A(c, { width: this.legendWidth, height: this.legendHeight, verticalAlign: this.proximate ? "top" : c.verticalAlign }), !0, b) }; a.prototype.handleOverflow = function (b) {
                        var a = this, c = this.chart,
                        d = c.renderer, e = this.options, g = e.y, h = "top" === e.verticalAlign, f = this.padding, l = e.maxHeight, k = e.navigation, m = r(k.animation, !0), n = k.arrowSize || 12, p = this.pages, v = this.allItems, t = function (b) { "number" === typeof b ? u.attr({ height: b }) : u && (a.clipRect = u.destroy(), a.contentGroup.clip()); a.contentGroup.div && (a.contentGroup.div.style.clip = b ? "rect(" + f + "px,9999px," + (f + b) + "px,0)" : "auto") }, w = function (b) { a[b] = d.circle(0, 0, 1.3 * n).translate(n / 2, n / 2).add(M); c.styledMode || a[b].attr("fill", "rgba(0,0,0,0.0001)"); return a[b] },
                        E, A; g = c.spacingBox.height + (h ? -g : g) - f; var M = this.nav, u = this.clipRect; "horizontal" !== e.layout || "middle" === e.verticalAlign || e.floating || (g /= 2); l && (g = Math.min(g, l)); p.length = 0; b && 0 < g && b > g && !1 !== k.enabled ? (this.clipHeight = E = Math.max(g - 20 - this.titleHeight - f, 0), this.currentPage = r(this.currentPage, 1), this.fullHeight = b, v.forEach(function (b, a) {
                            var c = b._legendItemPos[1], d = Math.round(b.legendItem.getBBox().height), e = p.length; if (!e || c - p[e - 1] > E && (A || c) !== p[e - 1]) p.push(A || c), e++; b.pageIx = e - 1; A && (v[a - 1].pageIx = e -
                                1); a === v.length - 1 && c + d - p[e - 1] > E && d <= E && (p.push(c), b.pageIx = e); c !== A && (A = c)
                        }), u || (u = a.clipRect = d.clipRect(0, f, 9999, 0), a.contentGroup.clip(u)), t(E), M || (this.nav = M = d.g().attr({ zIndex: 1 }).add(this.group), this.up = d.symbol("triangle", 0, 0, n, n).add(M), w("upTracker").on("click", function () { a.scroll(-1, m) }), this.pager = d.text("", 15, 10).addClass("highcharts-legend-navigation"), !c.styledMode && k.style && this.pager.css(k.style), this.pager.add(M), this.down = d.symbol("triangle-down", 0, 0, n, n).add(M), w("downTracker").on("click",
                            function () { a.scroll(1, m) })), a.scroll(0), b = g) : M && (t(), this.nav = M.destroy(), this.scrollGroup.attr({ translateY: 1 }), this.clipHeight = 0); return b
                    }; a.prototype.scroll = function (a, c) {
                        var d = this, e = this.chart, g = this.pages, h = g.length, f = this.clipHeight, l = this.options.navigation, k = this.pager, m = this.padding, n = this.currentPage + a; n > h && (n = h); 0 < n && ("undefined" !== typeof c && C(c, e), this.nav.attr({ translateX: m, translateY: f + this.padding + 7 + this.titleHeight, visibility: "inherit" }), [this.up, this.upTracker].forEach(function (b) {
                            b.attr({
                                "class": 1 ===
                                    n ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active"
                            })
                        }), k.attr({ text: n + "/" + h }), [this.down, this.downTracker].forEach(function (b) { b.attr({ x: 18 + this.pager.getBBox().width, "class": n === h ? "highcharts-legend-nav-inactive" : "highcharts-legend-nav-active" }) }, this), e.styledMode || (this.up.attr({ fill: 1 === n ? l.inactiveColor : l.activeColor }), this.upTracker.css({ cursor: 1 === n ? "default" : "pointer" }), this.down.attr({ fill: n === h ? l.inactiveColor : l.activeColor }), this.downTracker.css({ cursor: n === h ? "default" : "pointer" })),
                            this.scrollOffset = -g[n - 1] + this.initialItemY, this.scrollGroup.animate({ translateY: this.scrollOffset }), this.currentPage = n, this.positionCheckboxes(), a = D(r(c, e.renderer.globalAnimation, !0)), b(function () { u(d, "afterScroll", { currentPage: n }) }, a.duration))
                    }; a.prototype.setItemEvents = function (b, a, c) {
                        var d = this, e = d.chart.renderer.boxWrapper, g = b instanceof F, h = "highcharts-legend-" + (g ? "point" : "series") + "-active", f = d.chart.styledMode, l = function (a) {
                            d.allItems.forEach(function (c) {
                                b !== c && [c].concat(c.linkedSeries ||
                                    []).forEach(function (b) { b.setState(a, !g) })
                            })
                        }; (c ? [a, b.legendSymbol] : [b.legendGroup]).forEach(function (c) {
                            if (c) c.on("mouseover", function () { b.visible && l("inactive"); b.setState("hover"); b.visible && e.addClass(h); f || a.css(d.options.itemHoverStyle) }).on("mouseout", function () { d.chart.styledMode || a.css(A(b.visible ? d.itemStyle : d.itemHiddenStyle)); l(""); e.removeClass(h); b.setState() }).on("click", function (a) {
                                var c = function () { b.setVisible && b.setVisible(); l(b.visible ? "inactive" : "") }; e.removeClass(h); a = { browserEvent: a };
                                b.firePointEvent ? b.firePointEvent("legendItemClick", a, c) : u(b, "legendItemClick", a, c)
                            })
                        })
                    }; a.prototype.createCheckboxForItem = function (b) { b.checkbox = c("input", { type: "checkbox", className: "highcharts-legend-checkbox", checked: b.selected, defaultChecked: b.selected }, this.options.itemCheckboxStyle, this.chart.container); e(b.checkbox, "click", function (a) { u(b.series || b, "checkboxClick", { checked: a.target.checked, item: b }, function () { b.select() }) }) }; return a
            }(); (/Trident\/7\.0/.test(B.navigator && B.navigator.userAgent) ||
                a) && y(I.prototype, "positionItem", function (b, a) { var c = this, d = function () { a._legendItemPos && b.call(c, a) }; d(); c.bubbleLegend || setTimeout(d) }); ""; return I
        }); L(f, "Core/Series/SeriesRegistry.js", [f["Core/Globals.js"], f["Core/DefaultOptions.js"], f["Core/Series/Point.js"], f["Core/Utilities.js"]], function (a, f, B, F) {
            var y = f.defaultOptions, D = F.error, J = F.extendClass, C = F.merge, x; (function (f) {
                function g(a, c) {
                    var e = y.plotOptions || {}, g = c.defaultOptions; c.prototype.pointClass || (c.prototype.pointClass = B); c.prototype.type =
                        a; g && (e[a] = g); f.seriesTypes[a] = c
                } f.seriesTypes = a.seriesTypes; f.getSeries = function (a, c) { void 0 === c && (c = {}); var e = a.options.chart; e = c.type || e.type || e.defaultSeriesType || ""; var g = f.seriesTypes[e]; f || D(17, !0, a, { missingModuleFor: e }); e = new g; "function" === typeof e.init && e.init(a, c); return e }; f.registerSeriesType = g; f.seriesType = function (a, c, n, k, p) {
                    var e = y.plotOptions || {}; c = c || ""; e[a] = C(e[c], n); g(a, J(f.seriesTypes[c] || function () { }, k)); f.seriesTypes[a].prototype.type = a; p && (f.seriesTypes[a].prototype.pointClass =
                        J(B, p)); return f.seriesTypes[a]
                }
            })(x || (x = {})); return x
        }); L(f, "Core/Chart/Chart.js", [f["Core/Animation/AnimationUtilities.js"], f["Core/Axis/Axis.js"], f["Core/FormatUtilities.js"], f["Core/Foundation.js"], f["Core/Globals.js"], f["Core/Legend/Legend.js"], f["Core/MSPointer.js"], f["Core/DefaultOptions.js"], f["Core/Pointer.js"], f["Core/Renderer/RendererRegistry.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Renderer/SVG/SVGRenderer.js"], f["Core/Time.js"], f["Core/Utilities.js"], f["Core/Renderer/HTML/AST.js"]],
            function (a, f, B, F, y, I, J, C, x, p, g, e, c, n, k) {
                var t = a.animate, v = a.animObject, u = a.setAnimation, w = B.numberFormat, A = F.registerEventOptions, r = y.charts, m = y.doc, h = y.marginNames, b = y.svg, l = y.win, d = C.defaultOptions, G = C.defaultTime, z = g.seriesTypes, q = n.addEvent, D = n.attr, P = n.cleanRecursively, S = n.createElement, N = n.css, X = n.defined, W = n.discardElement, H = n.erase, K = n.error, L = n.extend, da = n.find, Q = n.fireEvent, ea = n.getStyle, E = n.isArray, T = n.isNumber, M = n.isObject, U = n.isString, V = n.merge, Y = n.objectEach, R = n.pick, ha = n.pInt, aa = n.relativeLength,
                ja = n.removeEvent, ia = n.splat, ba = n.syncTimeout, ka = n.uniqueKey; a = function () {
                    function a(b, a, c) {
                        this.series = this.renderTo = this.renderer = this.pointer = this.pointCount = this.plotWidth = this.plotTop = this.plotLeft = this.plotHeight = this.plotBox = this.options = this.numberFormatter = this.margin = this.legend = this.labelCollectors = this.isResizing = this.index = this.eventOptions = this.container = this.colorCounter = this.clipBox = this.chartWidth = this.chartHeight = this.bounds = this.axisOffset = this.axes = void 0; this.sharedClips = {}; this.yAxis =
                            this.xAxis = this.userOptions = this.titleOffset = this.time = this.symbolCounter = this.spacingBox = this.spacing = void 0; this.getArgs(b, a, c)
                    } a.chart = function (b, c, d) { return new a(b, c, d) }; a.prototype.getArgs = function (b, a, c) { U(b) || b.nodeName ? (this.renderTo = b, this.init(a, c)) : this.init(b, a) }; a.prototype.init = function (b, a) {
                        var e = b.plotOptions || {}; Q(this, "init", { args: arguments }, function () {
                            var g = V(d, b), h = g.chart; Y(g.plotOptions, function (b, a) { M(b) && (b.tooltip = e[a] && V(e[a].tooltip) || void 0) }); g.tooltip.userOptions = b.chart &&
                                b.chart.forExport && b.tooltip.userOptions || b.tooltip; this.userOptions = b; this.margin = []; this.spacing = []; this.bounds = { h: {}, v: {} }; this.labelCollectors = []; this.callback = a; this.isResizing = 0; this.options = g; this.axes = []; this.series = []; this.time = b.time && Object.keys(b.time).length ? new c(b.time) : y.time; this.numberFormatter = h.numberFormatter || w; this.styledMode = h.styledMode; this.hasCartesianSeries = h.showAxes; this.index = r.length; r.push(this); y.chartCount++; A(this, h); this.xAxis = []; this.yAxis = []; this.pointCount =
                                    this.colorCounter = this.symbolCounter = 0; Q(this, "afterInit"); this.firstRender()
                        })
                    }; a.prototype.initSeries = function (b) { var a = this.options.chart; a = b.type || a.type || a.defaultSeriesType; var c = z[a]; c || K(17, !0, this, { missingModuleFor: a }); a = new c; "function" === typeof a.init && a.init(this, b); return a }; a.prototype.setSeriesData = function () { this.getSeriesOrderByLinks().forEach(function (b) { b.points || b.data || !b.enabledDataSorting || b.setData(b.options.data, !1) }) }; a.prototype.getSeriesOrderByLinks = function () {
                        return this.series.concat().sort(function (b,
                            a) { return b.linkedSeries.length || a.linkedSeries.length ? a.linkedSeries.length - b.linkedSeries.length : 0 })
                    }; a.prototype.orderSeries = function (b) { var a = this.series; b = b || 0; for (var c = a.length; b < c; ++b)a[b] && (a[b].index = b, a[b].name = a[b].getName()) }; a.prototype.isInsidePlot = function (b, a, c) {
                        void 0 === c && (c = {}); var d = this.inverted, e = this.plotBox, g = this.plotLeft, h = this.plotTop, f = this.scrollablePlotBox, l = 0; var k = 0; c.visiblePlotOnly && this.scrollingContainer && (k = this.scrollingContainer, l = k.scrollLeft, k = k.scrollTop);
                        var m = c.series; e = c.visiblePlotOnly && f || e; f = c.inverted ? a : b; a = c.inverted ? b : a; b = { x: f, y: a, isInsidePlot: !0 }; if (!c.ignoreX) { var n = m && (d ? m.yAxis : m.xAxis) || { pos: g, len: Infinity }; f = c.paneCoordinates ? n.pos + f : g + f; f >= Math.max(l + g, n.pos) && f <= Math.min(l + g + e.width, n.pos + n.len) || (b.isInsidePlot = !1) } !c.ignoreY && b.isInsidePlot && (d = m && (d ? m.xAxis : m.yAxis) || { pos: h, len: Infinity }, c = c.paneCoordinates ? d.pos + a : h + a, c >= Math.max(k + h, d.pos) && c <= Math.min(k + h + e.height, d.pos + d.len) || (b.isInsidePlot = !1)); Q(this, "afterIsInsidePlot",
                            b); return b.isInsidePlot
                    }; a.prototype.redraw = function (b) {
                        Q(this, "beforeRedraw"); var a = this.hasCartesianSeries ? this.axes : this.colorAxis || [], c = this.series, d = this.pointer, e = this.legend, g = this.userOptions.legend, h = this.renderer, f = h.isHidden(), l = [], k = this.isDirtyBox, m = this.isDirtyLegend; this.setResponsive && this.setResponsive(!1); u(this.hasRendered ? b : !1, this); f && this.temporaryDisplay(); this.layOutTitles(); for (b = c.length; b--;) {
                            var n = c[b]; if (n.options.stacking || n.options.centerInCategory) {
                                var E = !0; if (n.isDirty) {
                                    var p =
                                        !0; break
                                }
                            }
                        } if (p) for (b = c.length; b--;)n = c[b], n.options.stacking && (n.isDirty = !0); c.forEach(function (b) { b.isDirty && ("point" === b.options.legendType ? ("function" === typeof b.updateTotals && b.updateTotals(), m = !0) : g && (g.labelFormatter || g.labelFormat) && (m = !0)); b.isDirtyData && Q(b, "updatedData") }); m && e && e.options.enabled && (e.render(), this.isDirtyLegend = !1); E && this.getStacks(); a.forEach(function (b) { b.updateNames(); b.setScale() }); this.getMargins(); a.forEach(function (b) { b.isDirty && (k = !0) }); a.forEach(function (b) {
                            var a =
                                b.min + "," + b.max; b.extKey !== a && (b.extKey = a, l.push(function () { Q(b, "afterSetExtremes", L(b.eventArgs, b.getExtremes())); delete b.eventArgs })); (k || E) && b.redraw()
                        }); k && this.drawChartBox(); Q(this, "predraw"); c.forEach(function (b) { (k || b.isDirty) && b.visible && b.redraw(); b.isDirtyData = !1 }); d && d.reset(!0); h.draw(); Q(this, "redraw"); Q(this, "render"); f && this.temporaryDisplay(!0); l.forEach(function (b) { b.call() })
                    }; a.prototype.get = function (b) {
                        function a(a) { return a.id === b || a.options && a.options.id === b } for (var c = this.series,
                            d = da(this.axes, a) || da(this.series, a), e = 0; !d && e < c.length; e++)d = da(c[e].points || [], a); return d
                    }; a.prototype.getAxes = function () { var b = this, a = this.options, c = a.xAxis = ia(a.xAxis || {}); a = a.yAxis = ia(a.yAxis || {}); Q(this, "getAxes"); c.forEach(function (b, a) { b.index = a; b.isX = !0 }); a.forEach(function (b, a) { b.index = a }); c.concat(a).forEach(function (a) { new f(b, a) }); Q(this, "afterGetAxes") }; a.prototype.getSelectedPoints = function () {
                        return this.series.reduce(function (b, a) {
                            a.getPointsCollection().forEach(function (a) {
                                R(a.selectedStaging,
                                    a.selected) && b.push(a)
                            }); return b
                        }, [])
                    }; a.prototype.getSelectedSeries = function () { return this.series.filter(function (b) { return b.selected }) }; a.prototype.setTitle = function (b, a, c) { this.applyDescription("title", b); this.applyDescription("subtitle", a); this.applyDescription("caption", void 0); this.layOutTitles(c) }; a.prototype.applyDescription = function (b, a) {
                        var c = this, d = "title" === b ? { color: "#333333", fontSize: this.options.isStock ? "16px" : "18px" } : { color: "#666666" }; d = this.options[b] = V(!this.styledMode && { style: d },
                            this.options[b], a); var e = this[b]; e && a && (this[b] = e = e.destroy()); d && !e && (e = this.renderer.text(d.text, 0, 0, d.useHTML).attr({ align: d.align, "class": "highcharts-" + b, zIndex: d.zIndex || 4 }).add(), e.update = function (a) { c[{ title: "setTitle", subtitle: "setSubtitle", caption: "setCaption" }[b]](a) }, this.styledMode || e.css(d.style), this[b] = e)
                    }; a.prototype.layOutTitles = function (b) {
                        var a = [0, 0, 0], c = this.renderer, d = this.spacingBox;["title", "subtitle", "caption"].forEach(function (b) {
                            var e = this[b], g = this.options[b], h = g.verticalAlign ||
                                "top"; b = "title" === b ? "top" === h ? -3 : 0 : "top" === h ? a[0] + 2 : 0; var f; if (e) { this.styledMode || (f = g.style && g.style.fontSize); f = c.fontMetrics(f, e).b; e.css({ width: (g.width || d.width + (g.widthAdjust || 0)) + "px" }); var l = Math.round(e.getBBox(g.useHTML).height); e.align(L({ y: "bottom" === h ? f : b + f, height: l }, g), !1, "spacingBox"); g.floating || ("top" === h ? a[0] = Math.ceil(a[0] + l) : "bottom" === h && (a[2] = Math.ceil(a[2] + l))) }
                        }, this); a[0] && "top" === (this.options.title.verticalAlign || "top") && (a[0] += this.options.title.margin); a[2] && "bottom" ===
                            this.options.caption.verticalAlign && (a[2] += this.options.caption.margin); var e = !this.titleOffset || this.titleOffset.join(",") !== a.join(","); this.titleOffset = a; Q(this, "afterLayOutTitles"); !this.isDirtyBox && e && (this.isDirtyBox = this.isDirtyLegend = e, this.hasRendered && R(b, !0) && this.isDirtyBox && this.redraw())
                    }; a.prototype.getChartSize = function () {
                        var b = this.options.chart, a = b.width; b = b.height; var c = this.renderTo; X(a) || (this.containerWidth = ea(c, "width")); X(b) || (this.containerHeight = ea(c, "height")); this.chartWidth =
                            Math.max(0, a || this.containerWidth || 600); this.chartHeight = Math.max(0, aa(b, this.chartWidth) || (1 < this.containerHeight ? this.containerHeight : 400))
                    }; a.prototype.temporaryDisplay = function (b) {
                        var a = this.renderTo; if (b) for (; a && a.style;)a.hcOrigStyle && (N(a, a.hcOrigStyle), delete a.hcOrigStyle), a.hcOrigDetached && (m.body.removeChild(a), a.hcOrigDetached = !1), a = a.parentNode; else for (; a && a.style;) {
                            m.body.contains(a) || a.parentNode || (a.hcOrigDetached = !0, m.body.appendChild(a)); if ("none" === ea(a, "display", !1) || a.hcOricDetached) a.hcOrigStyle =
                                { display: a.style.display, height: a.style.height, overflow: a.style.overflow }, b = { display: "block", overflow: "hidden" }, a !== this.renderTo && (b.height = 0), N(a, b), a.offsetWidth || a.style.setProperty("display", "block", "important"); a = a.parentNode; if (a === m.body) break
                        }
                    }; a.prototype.setClassName = function (b) { this.container.className = "highcharts-container " + (b || "") }; a.prototype.getContainer = function () {
                        var a = this.options, c = a.chart, d = ka(), g, h = this.renderTo; h || (this.renderTo = h = c.renderTo); U(h) && (this.renderTo = h = m.getElementById(h));
                        h || K(13, !0, this); var f = ha(D(h, "data-highcharts-chart")); T(f) && r[f] && r[f].hasRendered && r[f].destroy(); D(h, "data-highcharts-chart", this.index); h.innerHTML = k.emptyHTML; c.skipClone || h.offsetWidth || this.temporaryDisplay(); this.getChartSize(); f = this.chartWidth; var l = this.chartHeight; N(h, { overflow: "hidden" }); this.styledMode || (g = L({
                            position: "relative", overflow: "hidden", width: f + "px", height: l + "px", textAlign: "left", lineHeight: "normal", zIndex: 0, "-webkit-tap-highlight-color": "rgba(0,0,0,0)", userSelect: "none",
                            "touch-action": "manipulation", outline: "none"
                        }, c.style || {})); this.container = d = S("div", { id: d }, g, h); this._cursor = d.style.cursor; this.renderer = new (c.renderer || !b ? p.getRendererType(c.renderer) : e)(d, f, l, void 0, c.forExport, a.exporting && a.exporting.allowHTML, this.styledMode); u(void 0, this); this.setClassName(c.className); if (this.styledMode) for (var n in a.defs) this.renderer.definition(a.defs[n]); else this.renderer.setStyle(c.style); this.renderer.chartIndex = this.index; Q(this, "afterGetContainer")
                    }; a.prototype.getMargins =
                        function (b) { var a = this.spacing, c = this.margin, d = this.titleOffset; this.resetMargins(); d[0] && !X(c[0]) && (this.plotTop = Math.max(this.plotTop, d[0] + a[0])); d[2] && !X(c[2]) && (this.marginBottom = Math.max(this.marginBottom, d[2] + a[2])); this.legend && this.legend.display && this.legend.adjustMargins(c, a); Q(this, "getMargins"); b || this.getAxisMargins() }; a.prototype.getAxisMargins = function () {
                            var b = this, a = b.axisOffset = [0, 0, 0, 0], c = b.colorAxis, d = b.margin, e = function (b) { b.forEach(function (b) { b.visible && b.getOffset() }) }; b.hasCartesianSeries ?
                                e(b.axes) : c && c.length && e(c); h.forEach(function (c, e) { X(d[e]) || (b[c] += a[e]) }); b.setChartSize()
                        }; a.prototype.reflow = function (b) {
                            var a = this, c = a.options.chart, d = a.renderTo, e = X(c.width) && X(c.height), g = c.width || ea(d, "width"); c = c.height || ea(d, "height"); d = b ? b.target : l; delete a.pointer.chartPosition; if (!e && !a.isPrinting && g && c && (d === l || d === m)) {
                                if (g !== a.containerWidth || c !== a.containerHeight) n.clearTimeout(a.reflowTimeout), a.reflowTimeout = ba(function () { a.container && a.setSize(void 0, void 0, !1) }, b ? 100 : 0); a.containerWidth =
                                    g; a.containerHeight = c
                            }
                        }; a.prototype.setReflow = function (b) { var a = this; !1 === b || this.unbindReflow ? !1 === b && this.unbindReflow && (this.unbindReflow = this.unbindReflow()) : (this.unbindReflow = q(l, "resize", function (b) { a.options && a.reflow(b) }), q(this, "destroy", this.unbindReflow)) }; a.prototype.setSize = function (b, a, c) {
                            var d = this, e = d.renderer; d.isResizing += 1; u(c, d); c = e.globalAnimation; d.oldChartHeight = d.chartHeight; d.oldChartWidth = d.chartWidth; "undefined" !== typeof b && (d.options.chart.width = b); "undefined" !== typeof a &&
                                (d.options.chart.height = a); d.getChartSize(); d.styledMode || (c ? t : N)(d.container, { width: d.chartWidth + "px", height: d.chartHeight + "px" }, c); d.setChartSize(!0); e.setSize(d.chartWidth, d.chartHeight, c); d.axes.forEach(function (b) { b.isDirty = !0; b.setScale() }); d.isDirtyLegend = !0; d.isDirtyBox = !0; d.layOutTitles(); d.getMargins(); d.redraw(c); d.oldChartHeight = null; Q(d, "resize"); ba(function () { d && Q(d, "endResize", null, function () { --d.isResizing }) }, v(c).duration)
                        }; a.prototype.setChartSize = function (b) {
                            var a = this.inverted,
                            c = this.renderer, d = this.chartWidth, e = this.chartHeight, g = this.options.chart, h = this.spacing, f = this.clipOffset, l, k, m, n; this.plotLeft = l = Math.round(this.plotLeft); this.plotTop = k = Math.round(this.plotTop); this.plotWidth = m = Math.max(0, Math.round(d - l - this.marginRight)); this.plotHeight = n = Math.max(0, Math.round(e - k - this.marginBottom)); this.plotSizeX = a ? n : m; this.plotSizeY = a ? m : n; this.plotBorderWidth = g.plotBorderWidth || 0; this.spacingBox = c.spacingBox = { x: h[3], y: h[0], width: d - h[3] - h[1], height: e - h[0] - h[2] }; this.plotBox =
                                c.plotBox = { x: l, y: k, width: m, height: n }; a = 2 * Math.floor(this.plotBorderWidth / 2); d = Math.ceil(Math.max(a, f[3]) / 2); e = Math.ceil(Math.max(a, f[0]) / 2); this.clipBox = { x: d, y: e, width: Math.floor(this.plotSizeX - Math.max(a, f[1]) / 2 - d), height: Math.max(0, Math.floor(this.plotSizeY - Math.max(a, f[2]) / 2 - e)) }; b || (this.axes.forEach(function (b) { b.setAxisSize(); b.setAxisTranslation() }), c.alignElements()); Q(this, "afterSetChartSize", { skipAxes: b })
                        }; a.prototype.resetMargins = function () {
                            Q(this, "resetMargins"); var b = this, a = b.options.chart;
                            ["margin", "spacing"].forEach(function (c) { var d = a[c], e = M(d) ? d : [d, d, d, d];["Top", "Right", "Bottom", "Left"].forEach(function (d, g) { b[c][g] = R(a[c + d], e[g]) }) }); h.forEach(function (a, c) { b[a] = R(b.margin[c], b.spacing[c]) }); b.axisOffset = [0, 0, 0, 0]; b.clipOffset = [0, 0, 0, 0]
                        }; a.prototype.drawChartBox = function () {
                            var b = this.options.chart, a = this.renderer, c = this.chartWidth, d = this.chartHeight, e = this.styledMode, g = this.plotBGImage, h = b.backgroundColor, f = b.plotBackgroundColor, l = b.plotBackgroundImage, k = this.plotLeft, m = this.plotTop,
                            n = this.plotWidth, E = this.plotHeight, p = this.plotBox, q = this.clipRect, r = this.clipBox, v = this.chartBackground, M = this.plotBackground, t = this.plotBorder, w, A = "animate"; v || (this.chartBackground = v = a.rect().addClass("highcharts-background").add(), A = "attr"); if (e) var u = w = v.strokeWidth(); else { u = b.borderWidth || 0; w = u + (b.shadow ? 8 : 0); h = { fill: h || "none" }; if (u || v["stroke-width"]) h.stroke = b.borderColor, h["stroke-width"] = u; v.attr(h).shadow(b.shadow) } v[A]({ x: w / 2, y: w / 2, width: c - w - u % 2, height: d - w - u % 2, r: b.borderRadius }); A = "animate";
                            M || (A = "attr", this.plotBackground = M = a.rect().addClass("highcharts-plot-background").add()); M[A](p); e || (M.attr({ fill: f || "none" }).shadow(b.plotShadow), l && (g ? (l !== g.attr("href") && g.attr("href", l), g.animate(p)) : this.plotBGImage = a.image(l, k, m, n, E).add())); q ? q.animate({ width: r.width, height: r.height }) : this.clipRect = a.clipRect(r); A = "animate"; t || (A = "attr", this.plotBorder = t = a.rect().addClass("highcharts-plot-border").attr({ zIndex: 1 }).add()); e || t.attr({
                                stroke: b.plotBorderColor, "stroke-width": b.plotBorderWidth ||
                                    0, fill: "none"
                            }); t[A](t.crisp({ x: k, y: m, width: n, height: E }, -t.strokeWidth())); this.isDirtyBox = !1; Q(this, "afterDrawChartBox")
                        }; a.prototype.propFromSeries = function () { var b = this, a = b.options.chart, c = b.options.series, d, e, g;["inverted", "angular", "polar"].forEach(function (h) { e = z[a.type || a.defaultSeriesType]; g = a[h] || e && e.prototype[h]; for (d = c && c.length; !g && d--;)(e = z[c[d].type]) && e.prototype[h] && (g = !0); b[h] = g }) }; a.prototype.linkSeries = function () {
                            var b = this, a = b.series; a.forEach(function (b) {
                                b.linkedSeries.length =
                                0
                            }); a.forEach(function (a) { var c = a.options.linkedTo; U(c) && (c = ":previous" === c ? b.series[a.index - 1] : b.get(c)) && c.linkedParent !== a && (c.linkedSeries.push(a), a.linkedParent = c, c.enabledDataSorting && a.setDataSortingOptions(), a.visible = R(a.options.visible, c.options.visible, a.visible)) }); Q(this, "afterLinkSeries")
                        }; a.prototype.renderSeries = function () { this.series.forEach(function (b) { b.translate(); b.render() }) }; a.prototype.renderLabels = function () {
                            var b = this, a = b.options.labels; a.items && a.items.forEach(function (c) {
                                var d =
                                    L(a.style, c.style), e = ha(d.left) + b.plotLeft, g = ha(d.top) + b.plotTop + 12; delete d.left; delete d.top; b.renderer.text(c.html, e, g).attr({ zIndex: 2 }).css(d).add()
                            })
                        }; a.prototype.render = function () {
                            var b = this.axes, a = this.colorAxis, c = this.renderer, d = this.options, e = function (b) { b.forEach(function (b) { b.visible && b.render() }) }, g = 0; this.setTitle(); this.legend = new I(this, d.legend); this.getStacks && this.getStacks(); this.getMargins(!0); this.setChartSize(); d = this.plotWidth; b.some(function (b) {
                                if (b.horiz && b.visible && b.options.labels.enabled &&
                                    b.series.length) return g = 21, !0
                            }); var h = this.plotHeight = Math.max(this.plotHeight - g, 0); b.forEach(function (b) { b.setScale() }); this.getAxisMargins(); var f = 1.1 < d / this.plotWidth, l = 1.05 < h / this.plotHeight; if (f || l) b.forEach(function (b) { (b.horiz && f || !b.horiz && l) && b.setTickInterval(!0) }), this.getMargins(); this.drawChartBox(); this.hasCartesianSeries ? e(b) : a && a.length && e(a); this.seriesGroup || (this.seriesGroup = c.g("series-group").attr({ zIndex: 3 }).add()); this.renderSeries(); this.renderLabels(); this.addCredits(); this.setResponsive &&
                                this.setResponsive(); this.hasRendered = !0
                        }; a.prototype.addCredits = function (b) { var a = this, c = V(!0, this.options.credits, b); c.enabled && !this.credits && (this.credits = this.renderer.text(c.text + (this.mapCredits || ""), 0, 0).addClass("highcharts-credits").on("click", function () { c.href && (l.location.href = c.href) }).attr({ align: c.position.align, zIndex: 8 }), a.styledMode || this.credits.css(c.style), this.credits.add().align(c.position), this.credits.update = function (b) { a.credits = a.credits.destroy(); a.addCredits(b) }) }; a.prototype.destroy =
                            function () {
                                var b = this, a = b.axes, c = b.series, d = b.container, e = d && d.parentNode, g; Q(b, "destroy"); b.renderer.forExport ? H(r, b) : r[b.index] = void 0; y.chartCount--; b.renderTo.removeAttribute("data-highcharts-chart"); ja(b); for (g = a.length; g--;)a[g] = a[g].destroy(); this.scroller && this.scroller.destroy && this.scroller.destroy(); for (g = c.length; g--;)c[g] = c[g].destroy(); "title subtitle chartBackground plotBackground plotBGImage plotBorder seriesGroup clipRect credits pointer rangeSelector legend resetZoomButton tooltip renderer".split(" ").forEach(function (a) {
                                    var c =
                                        b[a]; c && c.destroy && (b[a] = c.destroy())
                                }); d && (d.innerHTML = k.emptyHTML, ja(d), e && W(d)); Y(b, function (a, c) { delete b[c] })
                            }; a.prototype.firstRender = function () {
                                var b = this, a = b.options; if (!b.isReadyToRender || b.isReadyToRender()) {
                                    b.getContainer(); b.resetMargins(); b.setChartSize(); b.propFromSeries(); b.getAxes(); (E(a.series) ? a.series : []).forEach(function (a) { b.initSeries(a) }); b.linkSeries(); b.setSeriesData(); Q(b, "beforeRender"); x && (J.isRequired() ? b.pointer = new J(b, a) : b.pointer = new x(b, a)); b.render(); b.pointer.getChartPosition();
                                    if (!b.renderer.imgCount && !b.hasLoaded) b.onload(); b.temporaryDisplay(!0)
                                }
                            }; a.prototype.onload = function () { this.callbacks.concat([this.callback]).forEach(function (b) { b && "undefined" !== typeof this.index && b.apply(this, [this]) }, this); Q(this, "load"); Q(this, "render"); X(this.index) && this.setReflow(this.options.chart.reflow); this.warnIfA11yModuleNotLoaded(); this.hasLoaded = !0 }; a.prototype.warnIfA11yModuleNotLoaded = function () {
                                var b = this.options; b && !this.accessibility && (this.renderer.boxWrapper.attr({
                                    role: "img",
                                    "aria-label": b.title && b.title.text || ""
                                }), b.accessibility && !1 === b.accessibility.enabled || K('Highcharts warning: Consider including the "accessibility.js" module to make your chart more usable for people with disabilities. Set the "accessibility.enabled" option to false to remove this warning. See https://www.highcharts.com/docs/accessibility/accessibility-module.', !1, this))
                            }; a.prototype.addSeries = function (b, a, c) {
                                var d = this, e; b && (a = R(a, !0), Q(d, "addSeries", { options: b }, function () {
                                    e = d.initSeries(b); d.isDirtyLegend =
                                        !0; d.linkSeries(); e.enabledDataSorting && e.setData(b.data, !1); Q(d, "afterAddSeries", { series: e }); a && d.redraw(c)
                                })); return e
                            }; a.prototype.addAxis = function (b, a, c, d) { return this.createAxis(a ? "xAxis" : "yAxis", { axis: b, redraw: c, animation: d }) }; a.prototype.addColorAxis = function (b, a, c) { return this.createAxis("colorAxis", { axis: b, redraw: a, animation: c }) }; a.prototype.createAxis = function (b, a) { b = new f(this, V(a.axis, { index: this[b].length, isX: "xAxis" === b })); R(a.redraw, !0) && this.redraw(a.animation); return b }; a.prototype.showLoading =
                                function (b) {
                                    var a = this, c = a.options, d = c.loading, e = function () { g && N(g, { left: a.plotLeft + "px", top: a.plotTop + "px", width: a.plotWidth + "px", height: a.plotHeight + "px" }) }, g = a.loadingDiv, h = a.loadingSpan; g || (a.loadingDiv = g = S("div", { className: "highcharts-loading highcharts-loading-hidden" }, null, a.container)); h || (a.loadingSpan = h = S("span", { className: "highcharts-loading-inner" }, null, g), q(a, "redraw", e)); g.className = "highcharts-loading"; k.setElementHTML(h, R(b, c.lang.loading, "")); a.styledMode || (N(g, L(d.style, { zIndex: 10 })),
                                        N(h, d.labelStyle), a.loadingShown || (N(g, { opacity: 0, display: "" }), t(g, { opacity: d.style.opacity || .5 }, { duration: d.showDuration || 0 }))); a.loadingShown = !0; e()
                                }; a.prototype.hideLoading = function () { var b = this.options, a = this.loadingDiv; a && (a.className = "highcharts-loading highcharts-loading-hidden", this.styledMode || t(a, { opacity: 0 }, { duration: b.loading.hideDuration || 100, complete: function () { N(a, { display: "none" }) } })); this.loadingShown = !1 }; a.prototype.update = function (b, a, d, e) {
                                    var g = this, h = {
                                        credits: "addCredits", title: "setTitle",
                                        subtitle: "setSubtitle", caption: "setCaption"
                                    }, f = b.isResponsiveOptions, l = [], k, m; Q(g, "update", { options: b }); f || g.setResponsive(!1, !0); b = P(b, g.options); g.userOptions = V(g.userOptions, b); var n = b.chart; if (n) {
                                        V(!0, g.options.chart, n); "className" in n && g.setClassName(n.className); "reflow" in n && g.setReflow(n.reflow); if ("inverted" in n || "polar" in n || "type" in n) { g.propFromSeries(); var E = !0 } "alignTicks" in n && (E = !0); "events" in n && A(this, n); Y(n, function (b, a) {
                                            -1 !== g.propsRequireUpdateSeries.indexOf("chart." + a) && (k = !0);
                                            -1 !== g.propsRequireDirtyBox.indexOf(a) && (g.isDirtyBox = !0); -1 !== g.propsRequireReflow.indexOf(a) && (f ? g.isDirtyBox = !0 : m = !0)
                                        }); !g.styledMode && n.style && g.renderer.setStyle(g.options.chart.style || {})
                                    } !g.styledMode && b.colors && (this.options.colors = b.colors); b.time && (this.time === G && (this.time = new c(b.time)), V(!0, g.options.time, b.time)); Y(b, function (a, c) {
                                        if (g[c] && "function" === typeof g[c].update) g[c].update(a, !1); else if ("function" === typeof g[h[c]]) g[h[c]](a); else "colors" !== c && -1 === g.collectionsWithUpdate.indexOf(c) &&
                                            V(!0, g.options[c], b[c]); "chart" !== c && -1 !== g.propsRequireUpdateSeries.indexOf(c) && (k = !0)
                                    }); this.collectionsWithUpdate.forEach(function (a) {
                                        if (b[a]) {
                                            var c = []; g[a].forEach(function (b, a) { b.options.isInternal || c.push(R(b.options.index, a)) }); ia(b[a]).forEach(function (b, e) {
                                                var h = X(b.id), f; h && (f = g.get(b.id)); !f && g[a] && (f = g[a][c ? c[e] : e]) && h && X(f.options.id) && (f = void 0); f && f.coll === a && (f.update(b, !1), d && (f.touched = !0)); !f && d && g.collectionsWithInit[a] && (g.collectionsWithInit[a][0].apply(g, [b].concat(g.collectionsWithInit[a][1] ||
                                                    []).concat([!1])).touched = !0)
                                            }); d && g[a].forEach(function (b) { b.touched || b.options.isInternal ? delete b.touched : l.push(b) })
                                        }
                                    }); l.forEach(function (b) { b.chart && b.remove && b.remove(!1) }); E && g.axes.forEach(function (b) { b.update({}, !1) }); k && g.getSeriesOrderByLinks().forEach(function (b) { b.chart && b.update({}, !1) }, this); E = n && n.width; n = n && (U(n.height) ? aa(n.height, E || g.chartWidth) : n.height); m || T(E) && E !== g.chartWidth || T(n) && n !== g.chartHeight ? g.setSize(E, n, e) : R(a, !0) && g.redraw(e); Q(g, "afterUpdate", {
                                        options: b, redraw: a,
                                        animation: e
                                    })
                                }; a.prototype.setSubtitle = function (b, a) { this.applyDescription("subtitle", b); this.layOutTitles(a) }; a.prototype.setCaption = function (b, a) { this.applyDescription("caption", b); this.layOutTitles(a) }; a.prototype.showResetZoom = function () {
                                    function b() { a.zoomOut() } var a = this, c = d.lang, e = a.options.chart.resetZoomButton, g = e.theme, h = "chart" === e.relativeTo || "spacingBox" === e.relativeTo ? null : "scrollablePlotBox"; Q(this, "beforeShowResetZoom", null, function () {
                                        a.resetZoomButton = a.renderer.button(c.resetZoom,
                                            null, null, b, g).attr({ align: e.position.align, title: c.resetZoomTitle }).addClass("highcharts-reset-zoom").add().align(e.position, !1, h)
                                    }); Q(this, "afterShowResetZoom")
                                }; a.prototype.zoomOut = function () { Q(this, "selection", { resetSelection: !0 }, this.zoom) }; a.prototype.zoom = function (b) {
                                    var a = this, c = a.pointer, d = a.inverted ? c.mouseDownX : c.mouseDownY, e = !1, g; !b || b.resetSelection ? (a.axes.forEach(function (b) { g = b.zoom() }), c.initiated = !1) : b.xAxis.concat(b.yAxis).forEach(function (b) {
                                        var h = b.axis, f = a.inverted ? h.left : h.top,
                                        l = a.inverted ? f + h.width : f + h.height, k = h.isXAxis, m = !1; if (!k && d >= f && d <= l || k || !X(d)) m = !0; c[k ? "zoomX" : "zoomY"] && m && (g = h.zoom(b.min, b.max), h.displayBtn && (e = !0))
                                    }); var h = a.resetZoomButton; e && !h ? a.showResetZoom() : !e && M(h) && (a.resetZoomButton = h.destroy()); g && a.redraw(R(a.options.chart.animation, b && b.animation, 100 > a.pointCount))
                                }; a.prototype.pan = function (b, a) {
                                    var c = this, d = c.hoverPoints; a = "object" === typeof a ? a : { enabled: a, type: "x" }; var e = c.options.chart; e && e.panning && (e.panning = a); var g = a.type, h; Q(this, "pan",
                                        { originalEvent: b }, function () {
                                            d && d.forEach(function (b) { b.setState() }); var a = c.xAxis; "xy" === g ? a = a.concat(c.yAxis) : "y" === g && (a = c.yAxis); var e = {}; a.forEach(function (a) {
                                                if (a.options.panningEnabled && !a.options.isInternal) {
                                                    var d = a.horiz, f = b[d ? "chartX" : "chartY"]; d = d ? "mouseDownX" : "mouseDownY"; var l = c[d], k = a.minPointOffset || 0, m = a.reversed && !c.inverted || !a.reversed && c.inverted ? -1 : 1, n = a.getExtremes(), E = a.toValue(l - f, !0) + k * m, p = a.toValue(l + a.len - f, !0) - (k * m || a.isXAxis && a.pointRangePadding || 0), q = p < E; m = a.hasVerticalPanning();
                                                    l = q ? p : E; E = q ? E : p; var r = a.panningState; !m || a.isXAxis || r && !r.isDirty || a.series.forEach(function (b) { var a = b.getProcessedData(!0); a = b.getExtremes(a.yData, !0); r || (r = { startMin: Number.MAX_VALUE, startMax: -Number.MAX_VALUE }); T(a.dataMin) && T(a.dataMax) && (r.startMin = Math.min(R(b.options.threshold, Infinity), a.dataMin, r.startMin), r.startMax = Math.max(R(b.options.threshold, -Infinity), a.dataMax, r.startMax)) }); m = Math.min(R(r && r.startMin, n.dataMin), k ? n.min : a.toValue(a.toPixels(n.min) - a.minPixelPadding)); p = Math.max(R(r &&
                                                        r.startMax, n.dataMax), k ? n.max : a.toValue(a.toPixels(n.max) + a.minPixelPadding)); a.panningState = r; a.isOrdinal || (k = m - l, 0 < k && (E += k, l = m), k = E - p, 0 < k && (E = p, l -= k), a.series.length && l !== n.min && E !== n.max && l >= m && E <= p && (a.setExtremes(l, E, !1, !1, { trigger: "pan" }), !c.resetZoomButton && l !== m && E !== p && g.match("y") && (c.showResetZoom(), a.displayBtn = !1), h = !0), e[d] = f)
                                                }
                                            }); Y(e, function (b, a) { c[a] = b }); h && c.redraw(!1); N(c.container, { cursor: "move" })
                                        })
                                }; return a
                }(); L(a.prototype, {
                    callbacks: [], collectionsWithInit: {
                        xAxis: [a.prototype.addAxis,
                        [!0]], yAxis: [a.prototype.addAxis, [!1]], series: [a.prototype.addSeries]
                    }, collectionsWithUpdate: ["xAxis", "yAxis", "series"], propsRequireDirtyBox: "backgroundColor borderColor borderWidth borderRadius plotBackgroundColor plotBackgroundImage plotBorderColor plotBorderWidth plotShadow shadow".split(" "), propsRequireReflow: "margin marginTop marginRight marginBottom marginLeft spacing spacingTop spacingRight spacingBottom spacingLeft".split(" "), propsRequireUpdateSeries: "chart.inverted chart.polar chart.ignoreHiddenSeries chart.type colors plotOptions time tooltip".split(" ")
                });
                ""; return a
            }); L(f, "Core/Legend/LegendSymbol.js", [f["Core/Utilities.js"]], function (a) {
                var f = a.merge, B = a.pick, F; (function (a) {
                    a.drawLineMarker = function (a) {
                        var y = this.options, C = a.symbolWidth, x = a.symbolHeight, p = x / 2, g = this.chart.renderer, e = this.legendGroup; a = a.baseline - Math.round(.3 * a.fontMetrics.b); var c = {}, n = y.marker; this.chart.styledMode || (c = { "stroke-width": y.lineWidth || 0 }, y.dashStyle && (c.dashstyle = y.dashStyle)); this.legendLine = g.path([["M", 0, a], ["L", C, a]]).addClass("highcharts-graph").attr(c).add(e);
                        n && !1 !== n.enabled && C && (y = Math.min(B(n.radius, p), p), 0 === this.symbol.indexOf("url") && (n = f(n, { width: x, height: x }), y = 0), this.legendSymbol = C = g.symbol(this.symbol, C / 2 - y, a - y, 2 * y, 2 * y, n).addClass("highcharts-point").add(e), C.isMarker = !0)
                    }; a.drawRectangle = function (a, f) { var y = a.symbolHeight, x = a.options.squareSymbol; f.legendSymbol = this.chart.renderer.rect(x ? (a.symbolWidth - y) / 2 : 0, a.baseline - y + 1, x ? y : a.symbolWidth, y, B(a.options.symbolRadius, y / 2)).addClass("highcharts-point").attr({ zIndex: 3 }).add(f.legendGroup) }
                })(F ||
                    (F = {})); return F
            }); L(f, "Core/Series/SeriesDefaults.js", [], function () {
                return {
                    lineWidth: 2, allowPointSelect: !1, crisp: !0, showCheckbox: !1, animation: { duration: 1E3 }, events: {}, marker: { enabledThreshold: 2, lineColor: "#ffffff", lineWidth: 0, radius: 4, states: { normal: { animation: !0 }, hover: { animation: { duration: 50 }, enabled: !0, radiusPlus: 2, lineWidthPlus: 1 }, select: { fillColor: "#cccccc", lineColor: "#000000", lineWidth: 2 } } }, point: { events: {} }, dataLabels: {
                        animation: {}, align: "center", defer: !0, formatter: function () {
                            var a = this.series.chart.numberFormatter;
                            return "number" !== typeof this.y ? "" : a(this.y, -1)
                        }, padding: 5, style: { fontSize: "11px", fontWeight: "bold", color: "contrast", textOutline: "1px contrast" }, verticalAlign: "bottom", x: 0, y: 0
                    }, cropThreshold: 300, opacity: 1, pointRange: 0, softThreshold: !0, states: { normal: { animation: !0 }, hover: { animation: { duration: 50 }, lineWidthPlus: 1, marker: {}, halo: { size: 10, opacity: .25 } }, select: { animation: { duration: 0 } }, inactive: { animation: { duration: 50 }, opacity: .2 } }, stickyTracking: !0, turboThreshold: 1E3, findNearestPointBy: "x"
                }
            }); L(f, "Core/Series/Series.js",
                [f["Core/Animation/AnimationUtilities.js"], f["Core/DefaultOptions.js"], f["Core/Foundation.js"], f["Core/Globals.js"], f["Core/Legend/LegendSymbol.js"], f["Core/Series/Point.js"], f["Core/Series/SeriesDefaults.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Renderer/SVG/SVGElement.js"], f["Core/Utilities.js"]], function (a, f, B, F, y, I, J, C, x, p) {
                    var g = a.animObject, e = a.setAnimation, c = f.defaultOptions, n = B.registerEventOptions, k = F.hasTouch, t = F.svg, v = F.win, u = C.seriesTypes, w = p.addEvent, A = p.arrayMax, r = p.arrayMin, m =
                        p.clamp, h = p.cleanRecursively, b = p.correctFloat, l = p.defined, d = p.erase, G = p.error, z = p.extend, q = p.find, D = p.fireEvent, P = p.getNestedProperty, S = p.isArray, N = p.isNumber, X = p.isString, W = p.merge, H = p.objectEach, K = p.pick, L = p.removeEvent, da = p.splat, Q = p.syncTimeout; a = function () {
                            function a() {
                                this.zones = this.yAxis = this.xAxis = this.userOptions = this.tooltipOptions = this.processedYData = this.processedXData = this.points = this.options = this.linkedSeries = this.index = this.eventsToUnbind = this.eventOptions = this.data = this.chart = this._i =
                                    void 0
                            } a.prototype.init = function (b, a) {
                                D(this, "init", { options: a }); var c = this, d = b.series; this.eventsToUnbind = []; c.chart = b; c.options = c.setOptions(a); a = c.options; c.linkedSeries = []; c.bindAxes(); z(c, { name: a.name, state: "", visible: !1 !== a.visible, selected: !0 === a.selected }); n(this, a); var e = a.events; if (e && e.click || a.point && a.point.events && a.point.events.click || a.allowPointSelect) b.runTrackerClick = !0; c.getColor(); c.getSymbol(); c.parallelArrays.forEach(function (b) { c[b + "Data"] || (c[b + "Data"] = []) }); c.isCartesian &&
                                    (b.hasCartesianSeries = !0); var g; d.length && (g = d[d.length - 1]); c._i = K(g && g._i, -1) + 1; c.opacity = c.options.opacity; b.orderSeries(this.insert(d)); a.dataSorting && a.dataSorting.enabled ? c.setDataSortingOptions() : c.points || c.data || c.setData(a.data, !1); D(this, "afterInit")
                            }; a.prototype.is = function (b) { return u[b] && this instanceof u[b] }; a.prototype.insert = function (b) {
                                var a = this.options.index, c; if (N(a)) {
                                    for (c = b.length; c--;)if (a >= K(b[c].options.index, b[c]._i)) { b.splice(c + 1, 0, this); break } -1 === c && b.unshift(this); c +=
                                        1
                                } else b.push(this); return K(c, b.length - 1)
                            }; a.prototype.bindAxes = function () { var b = this, a = b.options, c = b.chart, d; D(this, "bindAxes", null, function () { (b.axisTypes || []).forEach(function (e) { var g = 0; c[e].forEach(function (c) { d = c.options; if (a[e] === g && !d.isInternal || "undefined" !== typeof a[e] && a[e] === d.id || "undefined" === typeof a[e] && 0 === d.index) b.insert(c.series), b[e] = c, c.isDirty = !0; d.isInternal || g++ }); b[e] || b.optionalAxis === e || G(18, !0, c) }) }); D(this, "afterBindAxes") }; a.prototype.updateParallelArrays = function (b,
                                a) { var c = b.series, d = arguments, e = N(a) ? function (d) { var e = "y" === d && c.toYData ? c.toYData(b) : b[d]; c[d + "Data"][a] = e } : function (b) { Array.prototype[a].apply(c[b + "Data"], Array.prototype.slice.call(d, 2)) }; c.parallelArrays.forEach(e) }; a.prototype.hasData = function () { return this.visible && "undefined" !== typeof this.dataMax && "undefined" !== typeof this.dataMin || this.visible && this.yData && 0 < this.yData.length }; a.prototype.autoIncrement = function (b) {
                                    var a = this.options, c = a.pointIntervalUnit, d = a.relativeXValue, e = this.chart.time,
                                    g = this.xIncrement, h; g = K(g, a.pointStart, 0); this.pointInterval = h = K(this.pointInterval, a.pointInterval, 1); d && N(b) && (h *= b); c && (a = new e.Date(g), "day" === c ? e.set("Date", a, e.get("Date", a) + h) : "month" === c ? e.set("Month", a, e.get("Month", a) + h) : "year" === c && e.set("FullYear", a, e.get("FullYear", a) + h), h = a.getTime() - g); if (d && N(b)) return g + h; this.xIncrement = g + h; return g
                                }; a.prototype.setDataSortingOptions = function () {
                                    var b = this.options; z(this, { requireSorting: !1, sorted: !1, enabledDataSorting: !0, allowDG: !1 }); l(b.pointRange) ||
                                        (b.pointRange = 1)
                                }; a.prototype.setOptions = function (b) {
                                    var a = this.chart, d = a.options, e = d.plotOptions, g = a.userOptions || {}; b = W(b); a = a.styledMode; var h = { plotOptions: e, userOptions: b }; D(this, "setOptions", h); var f = h.plotOptions[this.type], k = g.plotOptions || {}; this.userOptions = h.userOptions; g = W(f, e.series, g.plotOptions && g.plotOptions[this.type], b); this.tooltipOptions = W(c.tooltip, c.plotOptions.series && c.plotOptions.series.tooltip, c.plotOptions[this.type].tooltip, d.tooltip.userOptions, e.series && e.series.tooltip,
                                        e[this.type].tooltip, b.tooltip); this.stickyTracking = K(b.stickyTracking, k[this.type] && k[this.type].stickyTracking, k.series && k.series.stickyTracking, this.tooltipOptions.shared && !this.noSharedTooltip ? !0 : g.stickyTracking); null === f.marker && delete g.marker; this.zoneAxis = g.zoneAxis; e = this.zones = (g.zones || []).slice(); !g.negativeColor && !g.negativeFillColor || g.zones || (d = { value: g[this.zoneAxis + "Threshold"] || g.threshold || 0, className: "highcharts-negative" }, a || (d.color = g.negativeColor, d.fillColor = g.negativeFillColor),
                                            e.push(d)); e.length && l(e[e.length - 1].value) && e.push(a ? {} : { color: this.color, fillColor: this.fillColor }); D(this, "afterSetOptions", { options: g }); return g
                                }; a.prototype.getName = function () { return K(this.options.name, "Series " + (this.index + 1)) }; a.prototype.getCyclic = function (b, a, c) {
                                    var d = this.chart, e = this.userOptions, g = b + "Index", h = b + "Counter", f = c ? c.length : K(d.options.chart[b + "Count"], d[b + "Count"]); if (!a) { var k = K(e[g], e["_" + g]); l(k) || (d.series.length || (d[h] = 0), e["_" + g] = k = d[h] % f, d[h] += 1); c && (a = c[k]) } "undefined" !==
                                        typeof k && (this[g] = k); this[b] = a
                                }; a.prototype.getColor = function () { this.chart.styledMode ? this.getCyclic("color") : this.options.colorByPoint ? this.color = "#cccccc" : this.getCyclic("color", this.options.color || c.plotOptions[this.type].color, this.chart.options.colors) }; a.prototype.getPointsCollection = function () { return (this.hasGroupedData ? this.points : this.data) || [] }; a.prototype.getSymbol = function () { this.getCyclic("symbol", this.options.marker.symbol, this.chart.options.symbols) }; a.prototype.findPointIndex = function (b,
                                    a) {
                                        var c = b.id, d = b.x, e = this.points, g = this.options.dataSorting, h, f; if (c) g = this.chart.get(c), g instanceof I && (h = g); else if (this.linkedParent || this.enabledDataSorting || this.options.relativeXValue) if (h = function (a) { return !a.touched && a.index === b.index }, g && g.matchByName ? h = function (a) { return !a.touched && a.name === b.name } : this.options.relativeXValue && (h = function (a) { return !a.touched && a.options.x === b.x }), h = q(e, h), !h) return; if (h) { var l = h && h.index; "undefined" !== typeof l && (f = !0) } "undefined" === typeof l && N(d) && (l = this.xData.indexOf(d,
                                            a)); -1 !== l && "undefined" !== typeof l && this.cropped && (l = l >= this.cropStart ? l - this.cropStart : l); !f && N(l) && e[l] && e[l].touched && (l = void 0); return l
                                }; a.prototype.updateData = function (b, a) {
                                    var c = this.options, d = c.dataSorting, e = this.points, g = [], h = this.requireSorting, f = b.length === e.length, k, m, n, E = !0; this.xIncrement = null; b.forEach(function (b, a) {
                                        var m = l(b) && this.pointClass.prototype.optionsToObject.call({ series: this }, b) || {}, E = m.x; if (m.id || N(E)) {
                                            if (m = this.findPointIndex(m, n), -1 === m || "undefined" === typeof m ? g.push(b) :
                                                e[m] && b !== c.data[m] ? (e[m].update(b, !1, null, !1), e[m].touched = !0, h && (n = m + 1)) : e[m] && (e[m].touched = !0), !f || a !== m || d && d.enabled || this.hasDerivedData) k = !0
                                        } else g.push(b)
                                    }, this); if (k) for (b = e.length; b--;)(m = e[b]) && !m.touched && m.remove && m.remove(!1, a); else !f || d && d.enabled ? E = !1 : (b.forEach(function (b, a) { b !== e[a].y && e[a].update && e[a].update(b, !1, null, !1) }), g.length = 0); e.forEach(function (b) { b && (b.touched = !1) }); if (!E) return !1; g.forEach(function (b) { this.addPoint(b, !1, null, null, !1) }, this); null === this.xIncrement &&
                                        this.xData && this.xData.length && (this.xIncrement = A(this.xData), this.autoIncrement()); return !0
                                }; a.prototype.setData = function (b, a, c, d) {
                                    var e = this, g = e.points, h = g && g.length || 0, f = e.options, l = e.chart, k = f.dataSorting, m = e.xAxis, n = f.turboThreshold, p = this.xData, E = this.yData, q = e.pointArrayMap; q = q && q.length; var r = f.keys, v, t = 0, w = 1, A = null; if (!l.options.chart.allowMutatingData) { f.data && delete e.options.data; e.userOptions.data && delete e.userOptions.data; var u = W(!0, b) } b = u || b || []; u = b.length; a = K(a, !0); k && k.enabled &&
                                        (b = this.sortData(b)); l.options.chart.allowMutatingData && !1 !== d && u && h && !e.cropped && !e.hasGroupedData && e.visible && !e.isSeriesBoosting && (v = this.updateData(b, c)); if (!v) {
                                            e.xIncrement = null; e.colorCounter = 0; this.parallelArrays.forEach(function (b) { e[b + "Data"].length = 0 }); if (n && u > n) if (A = e.getFirstValidPoint(b), N(A)) for (c = 0; c < u; c++)p[c] = this.autoIncrement(), E[c] = b[c]; else if (S(A)) if (q) if (A.length === q) for (c = 0; c < u; c++)p[c] = this.autoIncrement(), E[c] = b[c]; else for (c = 0; c < u; c++)d = b[c], p[c] = d[0], E[c] = d.slice(1, q + 1);
                                            else if (r && (t = r.indexOf("x"), w = r.indexOf("y"), t = 0 <= t ? t : 0, w = 0 <= w ? w : 1), 1 === A.length && (w = 0), t === w) for (c = 0; c < u; c++)p[c] = this.autoIncrement(), E[c] = b[c][w]; else for (c = 0; c < u; c++)d = b[c], p[c] = d[t], E[c] = d[w]; else G(12, !1, l); else for (c = 0; c < u; c++)"undefined" !== typeof b[c] && (d = { series: e }, e.pointClass.prototype.applyOptions.apply(d, [b[c]]), e.updateParallelArrays(d, c)); E && X(E[0]) && G(14, !0, l); e.data = []; e.options.data = e.userOptions.data = b; for (c = h; c--;)g[c] && g[c].destroy && g[c].destroy(); m && (m.minRange = m.userMinRange);
                                            e.isDirty = l.isDirtyBox = !0; e.isDirtyData = !!g; c = !1
                                        } "point" === f.legendType && (this.processData(), this.generatePoints()); a && l.redraw(c)
                                }; a.prototype.sortData = function (b) {
                                    var a = this, c = a.options.dataSorting.sortKey || "y", d = function (b, a) { return l(a) && b.pointClass.prototype.optionsToObject.call({ series: b }, a) || {} }; b.forEach(function (c, e) { b[e] = d(a, c); b[e].index = e }, this); b.concat().sort(function (b, a) { b = P(c, b); a = P(c, a); return a < b ? -1 : a > b ? 1 : 0 }).forEach(function (b, a) { b.x = a }, this); a.linkedSeries && a.linkedSeries.forEach(function (a) {
                                        var c =
                                            a.options, e = c.data; c.dataSorting && c.dataSorting.enabled || !e || (e.forEach(function (c, g) { e[g] = d(a, c); b[g] && (e[g].x = b[g].x, e[g].index = g) }), a.setData(e, !1))
                                    }); return b
                                }; a.prototype.getProcessedData = function (b) {
                                    var a = this.xAxis, c = this.options, d = c.cropThreshold, e = b || this.getExtremesFromAll || c.getExtremesFromAll, g = this.isCartesian; b = a && a.val2lin; c = !(!a || !a.logarithmic); var h = 0, f = this.xData, l = this.yData, k = this.requireSorting; var m = !1; var n = f.length; if (a) {
                                        m = a.getExtremes(); var p = m.min; var q = m.max; m = !(!a.categories ||
                                            a.names.length)
                                    } if (g && this.sorted && !e && (!d || n > d || this.forceCrop)) if (f[n - 1] < p || f[0] > q) f = [], l = []; else if (this.yData && (f[0] < p || f[n - 1] > q)) { var E = this.cropData(this.xData, this.yData, p, q); f = E.xData; l = E.yData; h = E.start; E = !0 } for (d = f.length || 1; --d;)if (a = c ? b(f[d]) - b(f[d - 1]) : f[d] - f[d - 1], 0 < a && ("undefined" === typeof r || a < r)) var r = a; else 0 > a && k && !m && (G(15, !1, this.chart), k = !1); return { xData: f, yData: l, cropped: E, cropStart: h, closestPointRange: r }
                                }; a.prototype.processData = function (b) {
                                    var a = this.xAxis; if (this.isCartesian &&
                                        !this.isDirty && !a.isDirty && !this.yAxis.isDirty && !b) return !1; b = this.getProcessedData(); this.cropped = b.cropped; this.cropStart = b.cropStart; this.processedXData = b.xData; this.processedYData = b.yData; this.closestPointRange = this.basePointRange = b.closestPointRange; D(this, "afterProcessData")
                                }; a.prototype.cropData = function (b, a, c, d, e) {
                                    var g = b.length, h, f = 0, l = g; e = K(e, this.cropShoulder); for (h = 0; h < g; h++)if (b[h] >= c) { f = Math.max(0, h - e); break } for (c = h; c < g; c++)if (b[c] > d) { l = c + e; break } return {
                                        xData: b.slice(f, l), yData: a.slice(f,
                                            l), start: f, end: l
                                    }
                                }; a.prototype.generatePoints = function () {
                                    var b = this.options, a = this.processedData || b.data, c = this.processedXData, d = this.processedYData, e = this.pointClass, g = c.length, h = this.cropStart || 0, f = this.hasGroupedData, l = b.keys, k = []; b = b.dataGrouping && b.dataGrouping.groupAll ? h : 0; var m, n, p = this.data; if (!p && !f) { var q = []; q.length = a.length; p = this.data = q } l && f && (this.options.keys = !1); for (n = 0; n < g; n++) {
                                        q = h + n; if (f) {
                                            var r = (new e).init(this, [c[n]].concat(da(d[n]))); r.dataGroup = this.groupMap[b + n]; r.dataGroup.options &&
                                                (r.options = r.dataGroup.options, z(r, r.dataGroup.options), delete r.dataLabels)
                                        } else (r = p[q]) || "undefined" === typeof a[q] || (p[q] = r = (new e).init(this, a[q], c[n])); r && (r.index = f ? b + n : q, k[n] = r)
                                    } this.options.keys = l; if (p && (g !== (m = p.length) || f)) for (n = 0; n < m; n++)n !== h || f || (n += g), p[n] && (p[n].destroyElements(), p[n].plotX = void 0); this.data = p; this.points = k; D(this, "afterGeneratePoints")
                                }; a.prototype.getXExtremes = function (b) { return { min: r(b), max: A(b) } }; a.prototype.getExtremes = function (b, a) {
                                    var c = this.xAxis, d = this.yAxis,
                                    e = this.processedXData || this.xData, g = [], h = this.requireSorting ? this.cropShoulder : 0; d = d ? d.positiveValuesOnly : !1; var f, l = 0, k = 0, m = 0; b = b || this.stackedYData || this.processedYData || []; var n = b.length; if (c) { var p = c.getExtremes(); l = p.min; k = p.max } for (f = 0; f < n; f++) { var q = e[f]; p = b[f]; var E = (N(p) || S(p)) && (p.length || 0 < p || !d); q = a || this.getExtremesFromAll || this.options.getExtremesFromAll || this.cropped || !c || (e[f + h] || q) >= l && (e[f - h] || q) <= k; if (E && q) if (E = p.length) for (; E--;)N(p[E]) && (g[m++] = p[E]); else g[m++] = p } b = {
                                        activeYData: g,
                                        dataMin: r(g), dataMax: A(g)
                                    }; D(this, "afterGetExtremes", { dataExtremes: b }); return b
                                }; a.prototype.applyExtremes = function () { var b = this.getExtremes(); this.dataMin = b.dataMin; this.dataMax = b.dataMax; return b }; a.prototype.getFirstValidPoint = function (b) { for (var a = b.length, c = 0, d = null; null === d && c < a;)d = b[c], c++; return d }; a.prototype.translate = function () {
                                    this.processedXData || this.processData(); this.generatePoints(); var a = this.options, c = a.stacking, d = this.xAxis, e = d.categories, g = this.enabledDataSorting, h = this.yAxis,
                                        f = this.points, k = f.length, n = this.pointPlacementToXValue(), p = !!n, q = a.threshold, r = a.startFromThreshold ? q : 0, v = this.zoneAxis || "y", t, w, A = Number.MAX_VALUE; for (t = 0; t < k; t++) {
                                            var u = f[t], z = u.x, x = void 0, G = void 0, y = u.y, C = u.low, B = c && h.stacking && h.stacking.stacks[(this.negStacks && y < (r ? 0 : q) ? "-" : "") + this.stackKey]; if (h.positiveValuesOnly && !h.validatePositiveValue(y) || d.positiveValuesOnly && !d.validatePositiveValue(z)) u.isNull = !0; u.plotX = w = b(m(d.translate(z, 0, 0, 0, 1, n, "flags" === this.type), -1E5, 1E5)); if (c && this.visible &&
                                                B && B[z]) { var H = this.getStackIndicator(H, z, this.index); u.isNull || (x = B[z], G = x.points[H.key]) } S(G) && (C = G[0], y = G[1], C === r && H.key === B[z].base && (C = K(N(q) && q, h.min)), h.positiveValuesOnly && 0 >= C && (C = null), u.total = u.stackTotal = x.total, u.percentage = x.total && u.y / x.total * 100, u.stackY = y, this.irregularWidths || x.setOffset(this.pointXOffset || 0, this.barW || 0)); u.yBottom = l(C) ? m(h.translate(C, 0, 1, 0, 1), -1E5, 1E5) : null; this.dataModify && (y = this.dataModify.modifyValue(y, t)); u.plotY = void 0; N(y) && (x = h.translate(y, !1, !0, !1, !0),
                                                    "undefined" !== typeof x && (u.plotY = m(x, -1E5, 1E5))); u.isInside = this.isPointInside(u); u.clientX = p ? b(d.translate(z, 0, 0, 0, 1, n)) : w; u.negative = u[v] < (a[v + "Threshold"] || q || 0); u.category = K(e && e[u.x], u.x); if (!u.isNull && !1 !== u.visible) { "undefined" !== typeof F && (A = Math.min(A, Math.abs(w - F))); var F = w } u.zone = this.zones.length ? u.getZone() : void 0; !u.graphic && this.group && g && (u.isNew = !0)
                                        } this.closestPointRangePx = A; D(this, "afterTranslate")
                                }; a.prototype.getValidPoints = function (b, a, c) {
                                    var d = this.chart; return (b || this.points ||
                                        []).filter(function (b) { return a && !d.isInsidePlot(b.plotX, b.plotY, { inverted: d.inverted }) ? !1 : !1 !== b.visible && (c || !b.isNull) })
                                }; a.prototype.getClipBox = function () { var b = this.chart, a = this.xAxis, c = this.yAxis, d = W(b.clipBox); a && a.len !== b.plotSizeX && (d.width = a.len); c && c.len !== b.plotSizeY && (d.height = c.len); return d }; a.prototype.getSharedClipKey = function () { return this.sharedClipKey = (this.options.xAxis || 0) + "," + (this.options.yAxis || 0) }; a.prototype.setClip = function () {
                                    var b = this.chart, a = this.group, c = this.markerGroup,
                                    d = b.sharedClips; b = b.renderer; var e = this.getClipBox(), g = this.getSharedClipKey(), h = d[g]; h ? h.animate(e) : d[g] = h = b.clipRect(e); a && a.clip(!1 === this.options.clip ? void 0 : h); c && c.clip()
                                }; a.prototype.animate = function (b) {
                                    var a = this.chart, c = this.group, d = this.markerGroup, e = a.inverted, h = g(this.options.animation), f = [this.getSharedClipKey(), h.duration, h.easing, h.defer].join(), l = a.sharedClips[f], k = a.sharedClips[f + "m"]; if (b && c) h = this.getClipBox(), l ? l.attr("height", h.height) : (h.width = 0, e && (h.x = a.plotHeight), l = a.renderer.clipRect(h),
                                        a.sharedClips[f] = l, k = a.renderer.clipRect({ x: e ? (a.plotSizeX || 0) + 99 : -99, y: e ? -a.plotLeft : -a.plotTop, width: 99, height: e ? a.chartWidth : a.chartHeight }), a.sharedClips[f + "m"] = k), c.clip(l), d && d.clip(k); else if (l && !l.hasClass("highcharts-animating")) { a = this.getClipBox(); var m = h.step; d && d.element.childNodes.length && (h.step = function (b, a) { m && m.apply(a, arguments); k && k.element && k.attr(a.prop, "width" === a.prop ? b + 99 : b) }); l.addClass("highcharts-animating").animate(a, h) }
                                }; a.prototype.afterAnimate = function () {
                                    var b = this;
                                    this.setClip(); H(this.chart.sharedClips, function (a, c, d) { a && !b.chart.container.querySelector('[clip-path="url(#'.concat(a.id, ')"]')) && (a.destroy(), delete d[c]) }); this.finishedAnimating = !0; D(this, "afterAnimate")
                                }; a.prototype.drawPoints = function () {
                                    var b = this.points, a = this.chart, c = this.options.marker, d = this[this.specialGroup] || this.markerGroup, e = this.xAxis, g = K(c.enabled, !e || e.isRadial ? !0 : null, this.closestPointRangePx >= c.enabledThreshold * c.radius), h, f; if (!1 !== c.enabled || this._hasPointMarkers) for (h = 0; h <
                                        b.length; h++) {
                                            var l = b[h]; var k = (f = l.graphic) ? "animate" : "attr"; var m = l.marker || {}; var n = !!l.marker; if ((g && "undefined" === typeof m.enabled || m.enabled) && !l.isNull && !1 !== l.visible) {
                                                var p = K(m.symbol, this.symbol, "rect"); var q = this.markerAttribs(l, l.selected && "select"); this.enabledDataSorting && (l.startXPos = e.reversed ? -(q.width || 0) : e.width); var r = !1 !== l.isInside; f ? f[r ? "show" : "hide"](r).animate(q) : r && (0 < (q.width || 0) || l.hasImage) && (l.graphic = f = a.renderer.symbol(p, q.x, q.y, q.width, q.height, n ? m : c).add(d), this.enabledDataSorting &&
                                                    a.hasRendered && (f.attr({ x: l.startXPos }), k = "animate")); f && "animate" === k && f[r ? "show" : "hide"](r).animate(q); if (f && !a.styledMode) f[k](this.pointAttribs(l, l.selected && "select")); f && f.addClass(l.getClassName(), !0)
                                            } else f && (l.graphic = f.destroy())
                                    }
                                }; a.prototype.markerAttribs = function (b, a) {
                                    var c = this.options, d = c.marker, e = b.marker || {}, g = e.symbol || d.symbol, h = K(e.radius, d && d.radius); a && (d = d.states[a], a = e.states && e.states[a], h = K(a && a.radius, d && d.radius, h && h + (d && d.radiusPlus || 0))); b.hasImage = g && 0 === g.indexOf("url");
                                    b.hasImage && (h = 0); b = N(h) ? { x: c.crisp ? Math.floor(b.plotX - h) : b.plotX - h, y: b.plotY - h } : {}; h && (b.width = b.height = 2 * h); return b
                                }; a.prototype.pointAttribs = function (b, a) {
                                    var c = this.options.marker, d = b && b.options, e = d && d.marker || {}, g = d && d.color, h = b && b.color, f = b && b.zone && b.zone.color, l = this.color; b = K(e.lineWidth, c.lineWidth); d = 1; l = g || f || h || l; g = e.fillColor || c.fillColor || l; h = e.lineColor || c.lineColor || l; a = a || "normal"; c = c.states[a] || {}; a = e.states && e.states[a] || {}; b = K(a.lineWidth, c.lineWidth, b + K(a.lineWidthPlus, c.lineWidthPlus,
                                        0)); g = a.fillColor || c.fillColor || g; h = a.lineColor || c.lineColor || h; d = K(a.opacity, c.opacity, d); return { stroke: h, "stroke-width": b, fill: g, opacity: d }
                                }; a.prototype.destroy = function (b) {
                                    var a = this, c = a.chart, e = /AppleWebKit\/533/.test(v.navigator.userAgent), g = a.data || [], h, f, l, k; D(a, "destroy", { keepEventsForUpdate: b }); this.removeEvents(b); (a.axisTypes || []).forEach(function (b) { (k = a[b]) && k.series && (d(k.series, a), k.isDirty = k.forceRedraw = !0) }); a.legendItem && a.chart.legend.destroyItem(a); for (f = g.length; f--;)(l = g[f]) &&
                                        l.destroy && l.destroy(); a.clips && a.clips.forEach(function (b) { return b.destroy() }); p.clearTimeout(a.animationTimeout); H(a, function (b, a) { b instanceof x && !b.survive && (h = e && "group" === a ? "hide" : "destroy", b[h]()) }); c.hoverSeries === a && (c.hoverSeries = void 0); d(c.series, a); c.orderSeries(); H(a, function (c, d) { b && "hcEvents" === d || delete a[d] })
                                }; a.prototype.applyZones = function () {
                                    var b = this, a = this.chart, c = a.renderer, d = this.zones, e = this.clips || [], g = this.graph, h = this.area, f = Math.max(a.chartWidth, a.chartHeight), l = this[(this.zoneAxis ||
                                        "y") + "Axis"], k = a.inverted, n, p, q, r, v, t, w, u, A = !1; if (d.length && (g || h) && l && "undefined" !== typeof l.min) {
                                            var z = l.reversed; var x = l.horiz; g && !this.showLine && g.hide(); h && h.hide(); var G = l.getExtremes(); d.forEach(function (d, E) {
                                                n = z ? x ? a.plotWidth : 0 : x ? 0 : l.toPixels(G.min) || 0; n = m(K(p, n), 0, f); p = m(Math.round(l.toPixels(K(d.value, G.max), !0) || 0), 0, f); A && (n = p = l.toPixels(G.max)); r = Math.abs(n - p); v = Math.min(n, p); t = Math.max(n, p); l.isXAxis ? (q = { x: k ? t : v, y: 0, width: r, height: f }, x || (q.x = a.plotHeight - q.x)) : (q = {
                                                    x: 0, y: k ? t : v, width: f,
                                                    height: r
                                                }, x && (q.y = a.plotWidth - q.y)); k && c.isVML && (q = l.isXAxis ? { x: 0, y: z ? v : t, height: q.width, width: a.chartWidth } : { x: q.y - a.plotLeft - a.spacingBox.x, y: 0, width: q.height, height: a.chartHeight }); e[E] ? e[E].animate(q) : e[E] = c.clipRect(q); w = b["zone-area-" + E]; u = b["zone-graph-" + E]; g && u && u.clip(e[E]); h && w && w.clip(e[E]); A = d.value > G.max; b.resetZones && 0 === p && (p = void 0)
                                            }); this.clips = e
                                        } else b.visible && (g && g.show(), h && h.show())
                                }; a.prototype.invertGroups = function (b) {
                                    function a() {
                                        ["group", "markerGroup"].forEach(function (a) {
                                            c[a] &&
                                            (d.renderer.isVML && c[a].attr({ width: c.yAxis.len, height: c.xAxis.len }), c[a].width = c.yAxis.len, c[a].height = c.xAxis.len, c[a].invert(c.isRadialSeries ? !1 : b))
                                        })
                                    } var c = this, d = c.chart; c.xAxis && (c.eventsToUnbind.push(w(d, "resize", a)), a(), c.invertGroups = a)
                                }; a.prototype.plotGroup = function (b, a, c, d, e) {
                                    var g = this[b], h = !g; c = { visibility: c, zIndex: d || .1 }; "undefined" === typeof this.opacity || this.chart.styledMode || "inactive" === this.state || (c.opacity = this.opacity); h && (this[b] = g = this.chart.renderer.g().add(e)); g.addClass("highcharts-" +
                                        a + " highcharts-series-" + this.index + " highcharts-" + this.type + "-series " + (l(this.colorIndex) ? "highcharts-color-" + this.colorIndex + " " : "") + (this.options.className || "") + (g.hasClass("highcharts-tracker") ? " highcharts-tracker" : ""), !0); g.attr(c)[h ? "attr" : "animate"](this.getPlotBox()); return g
                                }; a.prototype.getPlotBox = function () { var b = this.chart, a = this.xAxis, c = this.yAxis; b.inverted && (a = c, c = this.xAxis); return { translateX: a ? a.left : b.plotLeft, translateY: c ? c.top : b.plotTop, scaleX: 1, scaleY: 1 } }; a.prototype.removeEvents =
                                    function (b) { b || L(this); this.eventsToUnbind.length && (this.eventsToUnbind.forEach(function (b) { b() }), this.eventsToUnbind.length = 0) }; a.prototype.render = function () {
                                        var b = this, a = b.chart, c = b.options, d = g(c.animation), e = b.visible ? "inherit" : "hidden", h = c.zIndex, f = b.hasRendered, l = a.seriesGroup, k = a.inverted; a = !b.finishedAnimating && a.renderer.isSVG ? d.duration : 0; D(this, "render"); var m = b.plotGroup("group", "series", e, h, l); b.markerGroup = b.plotGroup("markerGroup", "markers", e, h, l); !1 !== c.clip && b.setClip(); b.animate &&
                                            a && b.animate(!0); m.inverted = K(b.invertible, b.isCartesian) ? k : !1; b.drawGraph && (b.drawGraph(), b.applyZones()); b.visible && b.drawPoints(); b.drawDataLabels && b.drawDataLabels(); b.redrawPoints && b.redrawPoints(); b.drawTracker && !1 !== b.options.enableMouseTracking && b.drawTracker(); b.invertGroups(k); b.animate && a && b.animate(); f || (a && d.defer && (a += d.defer), b.animationTimeout = Q(function () { b.afterAnimate() }, a || 0)); b.isDirty = !1; b.hasRendered = !0; D(b, "afterRender")
                                    }; a.prototype.redraw = function () {
                                        var b = this.chart, a = this.isDirty ||
                                            this.isDirtyData, c = this.group, d = this.xAxis, e = this.yAxis; c && (b.inverted && c.attr({ width: b.plotWidth, height: b.plotHeight }), c.animate({ translateX: K(d && d.left, b.plotLeft), translateY: K(e && e.top, b.plotTop) })); this.translate(); this.render(); a && delete this.kdTree
                                    }; a.prototype.searchPoint = function (b, a) { var c = this.xAxis, d = this.yAxis, e = this.chart.inverted; return this.searchKDTree({ clientX: e ? c.len - b.chartY + c.pos : b.chartX - c.pos, plotY: e ? d.len - b.chartX + d.pos : b.chartY - d.pos }, a, b) }; a.prototype.buildKDTree = function (b) {
                                        function a(b,
                                            d, e) { var g = b && b.length; if (g) { var h = c.kdAxisArray[d % e]; b.sort(function (b, a) { return b[h] - a[h] }); g = Math.floor(g / 2); return { point: b[g], left: a(b.slice(0, g), d + 1, e), right: a(b.slice(g + 1), d + 1, e) } } } this.buildingKdTree = !0; var c = this, d = -1 < c.options.findNearestPointBy.indexOf("y") ? 2 : 1; delete c.kdTree; Q(function () { c.kdTree = a(c.getValidPoints(null, !c.directTouch), d, d); c.buildingKdTree = !1 }, c.options.kdNow || b && "touchstart" === b.type ? 0 : 1)
                                    }; a.prototype.searchKDTree = function (b, a, c) {
                                        function d(b, a, c, k) {
                                            var m = a.point, n =
                                                e.kdAxisArray[c % k], p = m, q = l(b[g]) && l(m[g]) ? Math.pow(b[g] - m[g], 2) : null; var r = l(b[h]) && l(m[h]) ? Math.pow(b[h] - m[h], 2) : null; r = (q || 0) + (r || 0); m.dist = l(r) ? Math.sqrt(r) : Number.MAX_VALUE; m.distX = l(q) ? Math.sqrt(q) : Number.MAX_VALUE; n = b[n] - m[n]; r = 0 > n ? "left" : "right"; q = 0 > n ? "right" : "left"; a[r] && (r = d(b, a[r], c + 1, k), p = r[f] < p[f] ? r : m); a[q] && Math.sqrt(n * n) < p[f] && (b = d(b, a[q], c + 1, k), p = b[f] < p[f] ? b : p); return p
                                        } var e = this, g = this.kdAxisArray[0], h = this.kdAxisArray[1], f = a ? "distX" : "dist"; a = -1 < e.options.findNearestPointBy.indexOf("y") ?
                                            2 : 1; this.kdTree || this.buildingKdTree || this.buildKDTree(c); if (this.kdTree) return d(b, this.kdTree, a, a)
                                    }; a.prototype.pointPlacementToXValue = function () { var b = this.options, a = b.pointRange, c = this.xAxis; b = b.pointPlacement; "between" === b && (b = c.reversed ? -.5 : .5); return N(b) ? b * (a || c.pointRange) : 0 }; a.prototype.isPointInside = function (b) {
                                        var a = this.chart, c = this.xAxis, d = this.yAxis; return "undefined" !== typeof b.plotY && "undefined" !== typeof b.plotX && 0 <= b.plotY && b.plotY <= (d ? d.len : a.plotHeight) && 0 <= b.plotX && b.plotX <= (c ?
                                            c.len : a.plotWidth)
                                    }; a.prototype.drawTracker = function () {
                                        var b = this, a = b.options, c = a.trackByArea, d = [].concat(c ? b.areaPath : b.graphPath), e = b.chart, g = e.pointer, h = e.renderer, f = e.options.tooltip.snap, l = b.tracker, m = function (a) { if (e.hoverSeries !== b) b.onMouseOver() }, n = "rgba(192,192,192," + (t ? .0001 : .002) + ")"; l ? l.attr({ d: d }) : b.graph && (b.tracker = h.path(d).attr({ visibility: b.visible ? "inherit" : "hidden", zIndex: 2 }).addClass(c ? "highcharts-tracker-area" : "highcharts-tracker-line").add(b.group), e.styledMode || b.tracker.attr({
                                            "stroke-linecap": "round",
                                            "stroke-linejoin": "round", stroke: n, fill: c ? n : "none", "stroke-width": b.graph.strokeWidth() + (c ? 0 : 2 * f)
                                        }), [b.tracker, b.markerGroup, b.dataLabelsGroup].forEach(function (b) { if (b && (b.addClass("highcharts-tracker").on("mouseover", m).on("mouseout", function (b) { g.onTrackerMouseOut(b) }), a.cursor && !e.styledMode && b.css({ cursor: a.cursor }), k)) b.on("touchstart", m) })); D(this, "afterDrawTracker")
                                    }; a.prototype.addPoint = function (b, a, c, d, e) {
                                        var g = this.options, h = this.data, f = this.chart, l = this.xAxis; l = l && l.hasNames && l.names;
                                        var k = g.data, m = this.xData, n; a = K(a, !0); var p = { series: this }; this.pointClass.prototype.applyOptions.apply(p, [b]); var q = p.x; var r = m.length; if (this.requireSorting && q < m[r - 1]) for (n = !0; r && m[r - 1] > q;)r--; this.updateParallelArrays(p, "splice", r, 0, 0); this.updateParallelArrays(p, r); l && p.name && (l[q] = p.name); k.splice(r, 0, b); if (n || this.processedData) this.data.splice(r, 0, null), this.processData(); "point" === g.legendType && this.generatePoints(); c && (h[0] && h[0].remove ? h[0].remove(!1) : (h.shift(), this.updateParallelArrays(p,
                                            "shift"), k.shift())); !1 !== e && D(this, "addPoint", { point: p }); this.isDirtyData = this.isDirty = !0; a && f.redraw(d)
                                    }; a.prototype.removePoint = function (b, a, c) { var d = this, g = d.data, h = g[b], f = d.points, l = d.chart, k = function () { f && f.length === g.length && f.splice(b, 1); g.splice(b, 1); d.options.data.splice(b, 1); d.updateParallelArrays(h || { series: d }, "splice", b, 1); h && h.destroy(); d.isDirty = !0; d.isDirtyData = !0; a && l.redraw() }; e(c, l); a = K(a, !0); h ? h.firePointEvent("remove", null, k) : k() }; a.prototype.remove = function (b, a, c, d) {
                                        function e() {
                                            g.destroy(d);
                                            h.isDirtyLegend = h.isDirtyBox = !0; h.linkSeries(); K(b, !0) && h.redraw(a)
                                        } var g = this, h = g.chart; !1 !== c ? D(g, "remove", null, e) : e()
                                    }; a.prototype.update = function (b, a) {
                                        b = h(b, this.userOptions); D(this, "update", { options: b }); var c = this, d = c.chart, e = c.userOptions, g = c.initialType || c.type, f = d.options.plotOptions, l = u[g].prototype, k = c.finishedAnimating && { animation: !1 }, m = {}, n, p = ["eventOptions", "navigatorSeries", "baseSeries"], q = b.type || e.type || d.options.chart.type, r = !(this.hasDerivedData || q && q !== this.type || "undefined" !==
                                            typeof b.pointStart || "undefined" !== typeof b.pointInterval || "undefined" !== typeof b.relativeXValue || b.joinBy || b.mapData || c.hasOptionChanged("dataGrouping") || c.hasOptionChanged("pointStart") || c.hasOptionChanged("pointInterval") || c.hasOptionChanged("pointIntervalUnit") || c.hasOptionChanged("keys")); q = q || g; r && (p.push("data", "isDirtyData", "points", "processedData", "processedXData", "processedYData", "xIncrement", "cropped", "_hasPointMarkers", "_hasPointLabels", "clips", "nodes", "layout", "level", "mapMap", "mapData",
                                                "minY", "maxY", "minX", "maxX"), !1 !== b.visible && p.push("area", "graph"), c.parallelArrays.forEach(function (b) { p.push(b + "Data") }), b.data && (b.dataSorting && z(c.options.dataSorting, b.dataSorting), this.setData(b.data, !1))); b = W(e, k, { index: "undefined" === typeof e.index ? c.index : e.index, pointStart: K(f && f.series && f.series.pointStart, e.pointStart, c.xData[0]) }, !r && { data: c.options.data }, b); r && b.data && (b.data = c.options.data); p = ["group", "markerGroup", "dataLabelsGroup", "transformGroup"].concat(p); p.forEach(function (b) {
                                                    p[b] =
                                                    c[b]; delete c[b]
                                                }); f = !1; if (u[q]) { if (f = q !== c.type, c.remove(!1, !1, !1, !0), f) if (Object.setPrototypeOf) Object.setPrototypeOf(c, u[q].prototype); else { k = Object.hasOwnProperty.call(c, "hcEvents") && c.hcEvents; for (n in l) c[n] = void 0; z(c, u[q].prototype); k ? c.hcEvents = k : delete c.hcEvents } } else G(17, !0, d, { missingModuleFor: q }); p.forEach(function (b) { c[b] = p[b] }); c.init(d, b); if (r && this.points) {
                                                    var v = c.options; !1 === v.visible ? (m.graphic = 1, m.dataLabel = 1) : c._hasPointLabels || (b = v.marker, l = v.dataLabels, !b || !1 !== b.enabled &&
                                                        (e.marker && e.marker.symbol) === b.symbol || (m.graphic = 1), l && !1 === l.enabled && (m.dataLabel = 1)); this.points.forEach(function (b) { b && b.series && (b.resolveColor(), Object.keys(m).length && b.destroyElements(m), !1 === v.showInLegend && b.legendItem && d.legend.destroyItem(b)) }, this)
                                                } c.initialType = g; d.linkSeries(); f && c.linkedSeries.length && (c.isDirtyData = !0); D(this, "afterUpdate"); K(a, !0) && d.redraw(r ? void 0 : !1)
                                    }; a.prototype.setName = function (b) {
                                        this.name = this.options.name = this.userOptions.name = b; this.chart.isDirtyLegend =
                                            !0
                                    }; a.prototype.hasOptionChanged = function (b) { var a = this.options[b], c = this.chart.options.plotOptions, d = this.userOptions[b]; return d ? a !== d : a !== K(c && c[this.type] && c[this.type][b], c && c.series && c.series[b], a) }; a.prototype.onMouseOver = function () { var b = this.chart, a = b.hoverSeries; b.pointer.setHoverChartIndex(); if (a && a !== this) a.onMouseOut(); this.options.events.mouseOver && D(this, "mouseOver"); this.setState("hover"); b.hoverSeries = this }; a.prototype.onMouseOut = function () {
                                        var b = this.options, a = this.chart, c = a.tooltip,
                                        d = a.hoverPoint; a.hoverSeries = null; if (d) d.onMouseOut(); this && b.events.mouseOut && D(this, "mouseOut"); !c || this.stickyTracking || c.shared && !this.noSharedTooltip || c.hide(); a.series.forEach(function (b) { b.setState("", !0) })
                                    }; a.prototype.setState = function (b, a) {
                                        var c = this, d = c.options, e = c.graph, g = d.inactiveOtherPoints, h = d.states, f = K(h[b || "normal"] && h[b || "normal"].animation, c.chart.options.chart.animation), l = d.lineWidth, k = 0, m = d.opacity; b = b || ""; if (c.state !== b && ([c.group, c.markerGroup, c.dataLabelsGroup].forEach(function (a) {
                                            a &&
                                            (c.state && a.removeClass("highcharts-series-" + c.state), b && a.addClass("highcharts-series-" + b))
                                        }), c.state = b, !c.chart.styledMode)) { if (h[b] && !1 === h[b].enabled) return; b && (l = h[b].lineWidth || l + (h[b].lineWidthPlus || 0), m = K(h[b].opacity, m)); if (e && !e.dashstyle) for (d = { "stroke-width": l }, e.animate(d, f); c["zone-graph-" + k];)c["zone-graph-" + k].animate(d, f), k += 1; g || [c.group, c.markerGroup, c.dataLabelsGroup, c.labelBySeries].forEach(function (b) { b && b.animate({ opacity: m }, f) }) } a && g && c.points && c.setAllPointsToState(b || void 0)
                                    };
                            a.prototype.setAllPointsToState = function (b) { this.points.forEach(function (a) { a.setState && a.setState(b) }) }; a.prototype.setVisible = function (b, a) {
                                var c = this, d = c.chart, e = c.legendItem, g = d.options.chart.ignoreHiddenSeries, h = c.visible, f = (c.visible = b = c.options.visible = c.userOptions.visible = "undefined" === typeof b ? !h : b) ? "show" : "hide";["group", "dataLabelsGroup", "markerGroup", "tracker", "tt"].forEach(function (b) { if (c[b]) c[b][f]() }); if (d.hoverSeries === c || (d.hoverPoint && d.hoverPoint.series) === c) c.onMouseOut(); e &&
                                    d.legend.colorizeItem(c, b); c.isDirty = !0; c.options.stacking && d.series.forEach(function (b) { b.options.stacking && b.visible && (b.isDirty = !0) }); c.linkedSeries.forEach(function (a) { a.setVisible(b, !1) }); g && (d.isDirtyBox = !0); D(c, f); !1 !== a && d.redraw()
                            }; a.prototype.show = function () { this.setVisible(!0) }; a.prototype.hide = function () { this.setVisible(!1) }; a.prototype.select = function (b) {
                                this.selected = b = this.options.selected = "undefined" === typeof b ? !this.selected : b; this.checkbox && (this.checkbox.checked = b); D(this, b ? "select" :
                                    "unselect")
                            }; a.prototype.shouldShowTooltip = function (b, a, c) { void 0 === c && (c = {}); c.series = this; c.visiblePlotOnly = !0; return this.chart.isInsidePlot(b, a, c) }; a.defaultOptions = J; return a
                        }(); z(a.prototype, { axisTypes: ["xAxis", "yAxis"], coll: "series", colorCounter: 0, cropShoulder: 1, directTouch: !1, drawLegendSymbol: y.drawLineMarker, isCartesian: !0, kdAxisArray: ["clientX", "plotY"], parallelArrays: ["x", "y"], pointClass: I, requireSorting: !0, sorted: !0 }); C.series = a; ""; ""; return a
                }); L(f, "Extensions/ScrollablePlotArea.js",
                    [f["Core/Animation/AnimationUtilities.js"], f["Core/Axis/Axis.js"], f["Core/Chart/Chart.js"], f["Core/Series/Series.js"], f["Core/Renderer/RendererRegistry.js"], f["Core/Utilities.js"]], function (a, f, B, F, y, I) {
                        var D = a.stop, C = I.addEvent, x = I.createElement, p = I.merge, g = I.pick; C(B, "afterSetChartSize", function (a) {
                            var c = this.options.chart.scrollablePlotArea, e = c && c.minWidth; c = c && c.minHeight; if (!this.renderer.forExport) {
                                if (e) {
                                    if (this.scrollablePixelsX = e = Math.max(0, e - this.chartWidth)) {
                                        this.scrollablePlotBox = this.renderer.scrollablePlotBox =
                                            p(this.plotBox); this.plotBox.width = this.plotWidth += e; this.inverted ? this.clipBox.height += e : this.clipBox.width += e; var g = { 1: { name: "right", value: e } }
                                    }
                                } else c && (this.scrollablePixelsY = e = Math.max(0, c - this.chartHeight)) && (this.scrollablePlotBox = this.renderer.scrollablePlotBox = p(this.plotBox), this.plotBox.height = this.plotHeight += e, this.inverted ? this.clipBox.width += e : this.clipBox.height += e, g = { 2: { name: "bottom", value: e } }); g && !a.skipAxes && this.axes.forEach(function (a) {
                                    g[a.side] ? a.getPlotLinePath = function () {
                                        var c =
                                            g[a.side].name, e = this[c]; this[c] = e - g[a.side].value; var k = f.prototype.getPlotLinePath.apply(this, arguments); this[c] = e; return k
                                    } : (a.setAxisSize(), a.setAxisTranslation())
                                })
                            }
                        }); C(B, "render", function () { this.scrollablePixelsX || this.scrollablePixelsY ? (this.setUpScrolling && this.setUpScrolling(), this.applyFixed()) : this.fixedDiv && this.applyFixed() }); B.prototype.setUpScrolling = function () {
                            var a = this, c = { WebkitOverflowScrolling: "touch", overflowX: "hidden", overflowY: "hidden" }; this.scrollablePixelsX && (c.overflowX =
                                "auto"); this.scrollablePixelsY && (c.overflowY = "auto"); this.scrollingParent = x("div", { className: "highcharts-scrolling-parent" }, { position: "relative" }, this.renderTo); this.scrollingContainer = x("div", { className: "highcharts-scrolling" }, c, this.scrollingParent); C(this.scrollingContainer, "scroll", function () { a.pointer && delete a.pointer.chartPosition }); this.innerContainer = x("div", { className: "highcharts-inner-container" }, null, this.scrollingContainer); this.innerContainer.appendChild(this.container); this.setUpScrolling =
                                    null
                        }; B.prototype.moveFixedElements = function () {
                            var a = this.container, c = this.fixedRenderer, g = ".highcharts-contextbutton .highcharts-credits .highcharts-legend .highcharts-legend-checkbox .highcharts-navigator-series .highcharts-navigator-xaxis .highcharts-navigator-yaxis .highcharts-navigator .highcharts-reset-zoom .highcharts-drillup-button .highcharts-scrollbar .highcharts-subtitle .highcharts-title".split(" "), f; this.scrollablePixelsX && !this.inverted ? f = ".highcharts-yaxis" : this.scrollablePixelsX &&
                                this.inverted ? f = ".highcharts-xaxis" : this.scrollablePixelsY && !this.inverted ? f = ".highcharts-xaxis" : this.scrollablePixelsY && this.inverted && (f = ".highcharts-yaxis"); f && g.push("" + f + ":not(.highcharts-radial-axis)", "" + f + "-labels:not(.highcharts-radial-axis-labels)"); g.forEach(function (e) { [].forEach.call(a.querySelectorAll(e), function (a) { (a.namespaceURI === c.SVG_NS ? c.box : c.box.parentNode).appendChild(a); a.style.pointerEvents = "auto" }) })
                        }; B.prototype.applyFixed = function () {
                            var a = !this.fixedDiv, c = this.options.chart,
                            f = c.scrollablePlotArea, k = y.getRendererType(); a ? (this.fixedDiv = x("div", { className: "highcharts-fixed" }, { position: "absolute", overflow: "hidden", pointerEvents: "none", zIndex: (c.style && c.style.zIndex || 0) + 2, top: 0 }, null, !0), this.scrollingContainer && this.scrollingContainer.parentNode.insertBefore(this.fixedDiv, this.scrollingContainer), this.renderTo.style.overflow = "visible", this.fixedRenderer = c = new k(this.fixedDiv, this.chartWidth, this.chartHeight, this.options.chart.style), this.scrollableMask = c.path().attr({
                                fill: this.options.chart.backgroundColor ||
                                    "#fff", "fill-opacity": g(f.opacity, .85), zIndex: -1
                            }).addClass("highcharts-scrollable-mask").add(), C(this, "afterShowResetZoom", this.moveFixedElements), C(this, "afterApplyDrilldown", this.moveFixedElements), C(this, "afterLayOutTitles", this.moveFixedElements)) : this.fixedRenderer.setSize(this.chartWidth, this.chartHeight); if (this.scrollableDirty || a) this.scrollableDirty = !1, this.moveFixedElements(); c = this.chartWidth + (this.scrollablePixelsX || 0); k = this.chartHeight + (this.scrollablePixelsY || 0); D(this.container);
                            this.container.style.width = c + "px"; this.container.style.height = k + "px"; this.renderer.boxWrapper.attr({ width: c, height: k, viewBox: [0, 0, c, k].join(" ") }); this.chartBackground.attr({ width: c, height: k }); this.scrollingContainer.style.height = this.chartHeight + "px"; a && (f.scrollPositionX && (this.scrollingContainer.scrollLeft = this.scrollablePixelsX * f.scrollPositionX), f.scrollPositionY && (this.scrollingContainer.scrollTop = this.scrollablePixelsY * f.scrollPositionY)); k = this.axisOffset; a = this.plotTop - k[0] - 1; f = this.plotLeft -
                                k[3] - 1; c = this.plotTop + this.plotHeight + k[2] + 1; k = this.plotLeft + this.plotWidth + k[1] + 1; var p = this.plotLeft + this.plotWidth - (this.scrollablePixelsX || 0), v = this.plotTop + this.plotHeight - (this.scrollablePixelsY || 0); a = this.scrollablePixelsX ? [["M", 0, a], ["L", this.plotLeft - 1, a], ["L", this.plotLeft - 1, c], ["L", 0, c], ["Z"], ["M", p, a], ["L", this.chartWidth, a], ["L", this.chartWidth, c], ["L", p, c], ["Z"]] : this.scrollablePixelsY ? [["M", f, 0], ["L", f, this.plotTop - 1], ["L", k, this.plotTop - 1], ["L", k, 0], ["Z"], ["M", f, v], ["L", f, this.chartHeight],
                                ["L", k, this.chartHeight], ["L", k, v], ["Z"]] : [["M", 0, 0]]; "adjustHeight" !== this.redrawTrigger && this.scrollableMask.attr({ d: a })
                        }; C(f, "afterInit", function () { this.chart.scrollableDirty = !0 }); C(F, "show", function () { this.chart.scrollableDirty = !0 }); ""
                    }); L(f, "Core/Axis/StackingAxis.js", [f["Core/Animation/AnimationUtilities.js"], f["Core/Axis/Axis.js"], f["Core/Utilities.js"]], function (a, f, B) {
                        var D = a.getDeferredAnimation, y = B.addEvent, I = B.destroyObjectProperties, J = B.fireEvent, C = B.isNumber, x = B.objectEach, p; (function (a) {
                            function e() {
                                var a =
                                    this.stacking; if (a) { var c = a.stacks; x(c, function (a, e) { I(a); c[e] = null }); a && a.stackTotalGroup && a.stackTotalGroup.destroy() }
                            } function c() { this.stacking || (this.stacking = new f(this)) } var g = []; a.compose = function (a) { -1 === g.indexOf(a) && (g.push(a), y(a, "init", c), y(a, "destroy", e)); return a }; var f = function () {
                                function a(a) { this.oldStacks = {}; this.stacks = {}; this.stacksTouched = 0; this.axis = a } a.prototype.buildStacks = function () {
                                    var a = this.axis, c = a.series, e = a.options.reversedStacks, g = c.length, f; if (!a.isXAxis) {
                                        this.usePercentage =
                                        !1; for (f = g; f--;) { var k = c[e ? f : g - f - 1]; k.setStackedPoints(); k.setGroupedPoints() } for (f = 0; f < g; f++)c[f].modifyStacks(); J(a, "afterBuildStacks")
                                    }
                                }; a.prototype.cleanStacks = function () { if (!this.axis.isXAxis) { if (this.oldStacks) var a = this.stacks = this.oldStacks; x(a, function (a) { x(a, function (a) { a.cumulative = a.total }) }) } }; a.prototype.resetStacks = function () {
                                    var a = this, c = a.stacks; a.axis.isXAxis || x(c, function (c) {
                                        x(c, function (e, g) {
                                            C(e.touched) && e.touched < a.stacksTouched ? (e.destroy(), delete c[g]) : (e.total = null, e.cumulative =
                                                null)
                                        })
                                    })
                                }; a.prototype.renderStackTotals = function () { var a = this.axis, c = a.chart, e = c.renderer, g = this.stacks; a = D(c, a.options.stackLabels && a.options.stackLabels.animation || !1); var f = this.stackTotalGroup = this.stackTotalGroup || e.g("stack-labels").attr({ zIndex: 6, opacity: 0 }).add(); f.translate(c.plotLeft, c.plotTop); x(g, function (a) { x(a, function (a) { a.render(f) }) }); f.animate({ opacity: 1 }, a) }; return a
                            }(); a.Additions = f
                        })(p || (p = {})); return p
                    }); L(f, "Extensions/Stacking.js", [f["Core/Axis/Axis.js"], f["Core/Chart/Chart.js"],
                    f["Core/FormatUtilities.js"], f["Core/Globals.js"], f["Core/Series/Series.js"], f["Core/Axis/StackingAxis.js"], f["Core/Utilities.js"]], function (a, f, B, F, y, I, J) {
                        var D = B.format, x = J.correctFloat, p = J.defined, g = J.destroyObjectProperties, e = J.isArray, c = J.isNumber, n = J.objectEach, k = J.pick, t = function () {
                            function a(a, c, e, g, f) {
                                var h = a.chart.inverted; this.axis = a; this.isNegative = e; this.options = c = c || {}; this.x = g; this.total = null; this.points = {}; this.hasValidPoints = !1; this.stack = f; this.rightCliff = this.leftCliff = 0; this.alignOptions =
                                    { align: c.align || (h ? e ? "left" : "right" : "center"), verticalAlign: c.verticalAlign || (h ? "middle" : e ? "bottom" : "top"), y: c.y, x: c.x }; this.textAlign = c.textAlign || (h ? e ? "right" : "left" : "center")
                            } a.prototype.destroy = function () { g(this, this.axis) }; a.prototype.render = function (a) {
                                var c = this.axis.chart, e = this.options, g = e.format; g = g ? D(g, this, c) : e.formatter.call(this); this.label ? this.label.attr({ text: g, visibility: "hidden" }) : (this.label = c.renderer.label(g, null, null, e.shape, null, null, e.useHTML, !1, "stack-labels"), g = {
                                    r: e.borderRadius ||
                                        0, text: g, rotation: e.rotation, padding: k(e.padding, 5), visibility: "hidden"
                                }, c.styledMode || (g.fill = e.backgroundColor, g.stroke = e.borderColor, g["stroke-width"] = e.borderWidth, this.label.css(e.style)), this.label.attr(g), this.label.added || this.label.add(a)); this.label.labelrank = c.plotSizeY
                            }; a.prototype.setOffset = function (a, e, g, f, m) {
                                var h = this.axis, b = h.chart; f = h.translate(h.stacking.usePercentage ? 100 : f ? f : this.total, 0, 0, 0, 1); g = h.translate(g ? g : 0); g = p(f) && Math.abs(f - g); a = k(m, b.xAxis[0].translate(this.x)) + a; h =
                                    p(f) && this.getStackBox(b, this, a, f, e, g, h); e = this.label; g = this.isNegative; a = "justify" === k(this.options.overflow, "justify"); var l = this.textAlign; e && h && (m = e.getBBox(), f = e.padding, l = "left" === l ? b.inverted ? -f : f : "right" === l ? m.width : b.inverted && "center" === l ? m.width / 2 : b.inverted ? g ? m.width + f : -f : m.width / 2, g = b.inverted ? m.height / 2 : g ? -f : m.height, this.alignOptions.x = k(this.options.x, 0), this.alignOptions.y = k(this.options.y, 0), h.x -= l, h.y -= g, e.align(this.alignOptions, null, h), b.isInsidePlot(e.alignAttr.x + l - this.alignOptions.x,
                                        e.alignAttr.y + g - this.alignOptions.y) ? e.show() : (e.hide(), a = !1), a && y.prototype.justifyDataLabel.call(this.axis, e, this.alignOptions, e.alignAttr, m, h), e.attr({ x: e.alignAttr.x, y: e.alignAttr.y }), k(!a && this.options.crop, !0) && ((b = c(e.x) && c(e.y) && b.isInsidePlot(e.x - f + e.width, e.y) && b.isInsidePlot(e.x + f, e.y)) || e.hide()))
                            }; a.prototype.getStackBox = function (a, c, e, g, f, h, b) {
                                var l = c.axis.reversed, d = a.inverted, k = b.height + b.pos - (d ? a.plotLeft : a.plotTop); c = c.isNegative && !l || !c.isNegative && l; return {
                                    x: d ? c ? g - b.right : g - h +
                                        b.pos - a.plotLeft : e + a.xAxis[0].transB - a.plotLeft, y: d ? b.height - e - f : c ? k - g - h : k - g, width: d ? h : f, height: d ? f : h
                                }
                            }; return a
                        }(); f.prototype.getStacks = function () {
                            var a = this, c = a.inverted; a.yAxis.forEach(function (a) { a.stacking && a.stacking.stacks && a.hasVisibleSeries && (a.stacking.oldStacks = a.stacking.stacks) }); a.series.forEach(function (e) {
                                var g = e.xAxis && e.xAxis.options || {}; !e.options.stacking || !0 !== e.visible && !1 !== a.options.chart.ignoreHiddenSeries || (e.stackKey = [e.type, k(e.options.stack, ""), c ? g.top : g.left, c ? g.height :
                                    g.width].join())
                            })
                        }; I.compose(a); y.prototype.setGroupedPoints = function () { var a = this.yAxis.stacking; this.options.centerInCategory && (this.is("column") || this.is("columnrange")) && !this.options.stacking && 1 < this.chart.series.length ? y.prototype.setStackedPoints.call(this, "group") : a && n(a.stacks, function (c, e) { "group" === e.slice(-5) && (n(c, function (a) { return a.destroy() }), delete a.stacks[e]) }) }; y.prototype.setStackedPoints = function (a) {
                            var c = a || this.options.stacking; if (c && (!0 === this.visible || !1 === this.chart.options.chart.ignoreHiddenSeries)) {
                                var g =
                                    this.processedXData, f = this.processedYData, n = [], m = f.length, h = this.options, b = h.threshold, l = k(h.startFromThreshold && b, 0); h = h.stack; a = a ? "" + this.type + ",".concat(c) : this.stackKey; var d = "-" + a, v = this.negStacks, z = this.yAxis, q = z.stacking.stacks, y = z.stacking.oldStacks, D, C; z.stacking.stacksTouched += 1; for (C = 0; C < m; C++) {
                                        var B = g[C]; var F = f[C]; var J = this.getStackIndicator(J, B, this.index); var H = J.key; var K = (D = v && F < (l ? 0 : b)) ? d : a; q[K] || (q[K] = {}); q[K][B] || (y[K] && y[K][B] ? (q[K][B] = y[K][B], q[K][B].total = null) : q[K][B] = new t(z,
                                            z.options.stackLabels, D, B, h)); K = q[K][B]; null !== F ? (K.points[H] = K.points[this.index] = [k(K.cumulative, l)], p(K.cumulative) || (K.base = H), K.touched = z.stacking.stacksTouched, 0 < J.index && !1 === this.singleStacks && (K.points[H][0] = K.points[this.index + "," + B + ",0"][0])) : K.points[H] = K.points[this.index] = null; "percent" === c ? (D = D ? a : d, v && q[D] && q[D][B] ? (D = q[D][B], K.total = D.total = Math.max(D.total, K.total) + Math.abs(F) || 0) : K.total = x(K.total + (Math.abs(F) || 0))) : "group" === c ? (e(F) && (F = F[0]), null !== F && (K.total = (K.total || 0) + 1)) :
                                                K.total = x(K.total + (F || 0)); K.cumulative = "group" === c ? (K.total || 1) - 1 : k(K.cumulative, l) + (F || 0); null !== F && (K.points[H].push(K.cumulative), n[C] = K.cumulative, K.hasValidPoints = !0)
                                    } "percent" === c && (z.stacking.usePercentage = !0); "group" !== c && (this.stackedYData = n); z.stacking.oldStacks = {}
                            }
                        }; y.prototype.modifyStacks = function () {
                            var a = this, c = a.stackKey, e = a.yAxis.stacking.stacks, g = a.processedXData, f, k = a.options.stacking; a[k + "Stacker"] && [c, "-" + c].forEach(function (c) {
                                for (var b = g.length, h, d; b--;)if (h = g[b], f = a.getStackIndicator(f,
                                    h, a.index, c), d = (h = e[c] && e[c][h]) && h.points[f.key]) a[k + "Stacker"](d, h, b)
                            })
                        }; y.prototype.percentStacker = function (a, c, e) { c = c.total ? 100 / c.total : 0; a[0] = x(a[0] * c); a[1] = x(a[1] * c); this.stackedYData[e] = a[1] }; y.prototype.getStackIndicator = function (a, c, e, g) { !p(a) || a.x !== c || g && a.stackKey !== g ? a = { x: c, index: 0, key: g, stackKey: g } : a.index++; a.key = [e, c, a.index].join(); return a }; F.StackItem = t; ""; return F.StackItem
                    }); L(f, "Series/Line/LineSeries.js", [f["Core/Series/Series.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Utilities.js"]],
                        function (a, f, B) {
                            var D = this && this.__extends || function () { var a = function (f, x) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, g) { a.__proto__ = g } || function (a, g) { for (var e in g) g.hasOwnProperty(e) && (a[e] = g[e]) }; return a(f, x) }; return function (f, x) { function p() { this.constructor = f } a(f, x); f.prototype = null === x ? Object.create(x) : (p.prototype = x.prototype, new p) } }(), y = B.defined, I = B.merge; B = function (f) {
                                function C() {
                                    var a = null !== f && f.apply(this, arguments) || this; a.data = void 0; a.options = void 0; a.points =
                                        void 0; return a
                                } D(C, f); C.prototype.drawGraph = function () {
                                    var a = this, f = this.options, g = (this.gappedPath || this.getGraphPath).call(this), e = this.chart.styledMode, c = [["graph", "highcharts-graph"]]; e || c[0].push(f.lineColor || this.color || "#cccccc", f.dashStyle); c = a.getZonesGraphs(c); c.forEach(function (c, k) {
                                        var n = c[0], p = a[n], u = p ? "animate" : "attr"; p ? (p.endX = a.preventGraphAnimation ? null : g.xMap, p.animate({ d: g })) : g.length && (a[n] = p = a.chart.renderer.path(g).addClass(c[1]).attr({ zIndex: 1 }).add(a.group)); p && !e && (n = {
                                            stroke: c[2],
                                            "stroke-width": f.lineWidth, fill: a.fillGraph && a.color || "none"
                                        }, c[3] ? n.dashstyle = c[3] : "square" !== f.linecap && (n["stroke-linecap"] = n["stroke-linejoin"] = "round"), p[u](n).shadow(2 > k && f.shadow)); p && (p.startX = g.xMap, p.isArea = g.isArea)
                                    })
                                }; C.prototype.getGraphPath = function (a, f, g) {
                                    var e = this, c = e.options, n = [], k = [], p, v = c.step; a = a || e.points; var u = a.reversed; u && a.reverse(); (v = { right: 1, center: 2 }[v] || v && 3) && u && (v = 4 - v); a = this.getValidPoints(a, !1, !(c.connectNulls && !f && !g)); a.forEach(function (t, u) {
                                        var r = t.plotX, m = t.plotY,
                                        h = a[u - 1]; (t.leftCliff || h && h.rightCliff) && !g && (p = !0); t.isNull && !y(f) && 0 < u ? p = !c.connectNulls : t.isNull && !f ? p = !0 : (0 === u || p ? u = [["M", t.plotX, t.plotY]] : e.getPointSpline ? u = [e.getPointSpline(a, t, u)] : v ? (u = 1 === v ? [["L", h.plotX, m]] : 2 === v ? [["L", (h.plotX + r) / 2, h.plotY], ["L", (h.plotX + r) / 2, m]] : [["L", r, h.plotY]], u.push(["L", r, m])) : u = [["L", r, m]], k.push(t.x), v && (k.push(t.x), 2 === v && k.push(t.x)), n.push.apply(n, u), p = !1)
                                    }); n.xMap = k; return e.graphPath = n
                                }; C.prototype.getZonesGraphs = function (a) {
                                    this.zones.forEach(function (f,
                                        g) { g = ["zone-graph-" + g, "highcharts-graph highcharts-zone-graph-" + g + " " + (f.className || "")]; this.chart.styledMode || g.push(f.color || this.color, f.dashStyle || this.options.dashStyle); a.push(g) }, this); return a
                                }; C.defaultOptions = I(a.defaultOptions, {}); return C
                            }(a); f.registerSeriesType("line", B); ""; return B
                        }); L(f, "Series/Area/AreaSeries.js", [f["Core/Color/Color.js"], f["Core/Legend/LegendSymbol.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Utilities.js"]], function (a, f, B, F) {
                            var y = this && this.__extends || function () {
                                var a =
                                    function (e, c) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, c) { a.__proto__ = c } || function (a, c) { for (var e in c) c.hasOwnProperty(e) && (a[e] = c[e]) }; return a(e, c) }; return function (e, c) { function g() { this.constructor = e } a(e, c); e.prototype = null === c ? Object.create(c) : (g.prototype = c.prototype, new g) }
                            }(), D = a.parse, J = B.seriesTypes.line; a = F.extend; var C = F.merge, x = F.objectEach, p = F.pick; F = function (a) {
                                function e() {
                                    var c = null !== a && a.apply(this, arguments) || this; c.data = void 0; c.options = void 0; c.points =
                                        void 0; return c
                                } y(e, a); e.prototype.drawGraph = function () {
                                    this.areaPath = []; a.prototype.drawGraph.apply(this); var c = this, e = this.areaPath, g = this.options, f = [["area", "highcharts-area", this.color, g.fillColor]]; this.zones.forEach(function (a, e) { f.push(["zone-area-" + e, "highcharts-area highcharts-zone-area-" + e + " " + a.className, a.color || c.color, a.fillColor || g.fillColor]) }); f.forEach(function (a) {
                                        var f = a[0], k = c[f], n = k ? "animate" : "attr", r = {}; k ? (k.endX = c.preventGraphAnimation ? null : e.xMap, k.animate({ d: e })) : (r.zIndex =
                                            0, k = c[f] = c.chart.renderer.path(e).addClass(a[1]).add(c.group), k.isArea = !0); c.chart.styledMode || (r.fill = p(a[3], D(a[2]).setOpacity(p(g.fillOpacity, .75)).get())); k[n](r); k.startX = e.xMap; k.shiftUnit = g.step ? 2 : 1
                                    })
                                }; e.prototype.getGraphPath = function (a) {
                                    var c = J.prototype.getGraphPath, e = this.options, g = e.stacking, f = this.yAxis, u, w = [], A = [], r = this.index, m = f.stacking.stacks[this.stackKey], h = e.threshold, b = Math.round(f.getThreshold(e.threshold)); e = p(e.connectNulls, "percent" === g); var l = function (c, d, e) {
                                        var l = a[c];
                                        c = g && m[l.x].points[r]; var k = l[e + "Null"] || 0; e = l[e + "Cliff"] || 0; l = !0; if (e || k) { var n = (k ? c[0] : c[1]) + e; var p = c[0] + e; l = !!k } else !g && a[d] && a[d].isNull && (n = p = h); "undefined" !== typeof n && (A.push({ plotX: x, plotY: null === n ? b : f.getThreshold(n), isNull: l, isCliff: !0 }), w.push({ plotX: x, plotY: null === p ? b : f.getThreshold(p), doCurve: !1 }))
                                    }; a = a || this.points; g && (a = this.getStackPoints(a)); for (u = 0; u < a.length; u++) {
                                        g || (a[u].leftCliff = a[u].rightCliff = a[u].leftNull = a[u].rightNull = void 0); var d = a[u].isNull; var x = p(a[u].rectPlotX, a[u].plotX);
                                        var z = g ? p(a[u].yBottom, b) : b; if (!d || e) e || l(u, u - 1, "left"), d && !g && e || (A.push(a[u]), w.push({ x: u, plotX: x, plotY: z })), e || l(u, u + 1, "right")
                                    } u = c.call(this, A, !0, !0); w.reversed = !0; d = c.call(this, w, !0, !0); (z = d[0]) && "M" === z[0] && (d[0] = ["L", z[1], z[2]]); d = u.concat(d); d.length && d.push(["Z"]); c = c.call(this, A, !1, e); d.xMap = u.xMap; this.areaPath = d; return c
                                }; e.prototype.getStackPoints = function (a) {
                                    var c = this, e = [], g = [], f = this.xAxis, u = this.yAxis, w = u.stacking.stacks[this.stackKey], A = {}, r = u.series, m = r.length, h = u.options.reversedStacks ?
                                        1 : -1, b = r.indexOf(c); a = a || this.points; if (this.options.stacking) {
                                            for (var l = 0; l < a.length; l++)a[l].leftNull = a[l].rightNull = void 0, A[a[l].x] = a[l]; x(w, function (b, a) { null !== b.total && g.push(a) }); g.sort(function (b, a) { return b - a }); var d = r.map(function (b) { return b.visible }); g.forEach(function (a, l) {
                                                var k = 0, n, t; if (A[a] && !A[a].isNull) e.push(A[a]), [-1, 1].forEach(function (e) {
                                                    var f = 1 === e ? "rightNull" : "leftNull", k = 0, p = w[g[l + e]]; if (p) for (var q = b; 0 <= q && q < m;) {
                                                        var z = r[q].index; n = p.points[z]; n || (z === c.index ? A[a][f] = !0 : d[q] &&
                                                            (t = w[a].points[z]) && (k -= t[1] - t[0])); q += h
                                                    } A[a][1 === e ? "rightCliff" : "leftCliff"] = k
                                                }); else { for (var z = b; 0 <= z && z < m;) { if (n = w[a].points[r[z].index]) { k = n[1]; break } z += h } k = p(k, 0); k = u.translate(k, 0, 1, 0, 1); e.push({ isNull: !0, plotX: f.translate(a, 0, 0, 0, 1), x: a, plotY: k, yBottom: k }) }
                                            })
                                        } return e
                                }; e.defaultOptions = C(J.defaultOptions, { threshold: 0 }); return e
                            }(J); a(F.prototype, { singleStacks: !1, drawLegendSymbol: f.drawRectangle }); B.registerSeriesType("area", F); ""; return F
                        }); L(f, "Series/Spline/SplineSeries.js", [f["Core/Series/SeriesRegistry.js"],
                        f["Core/Utilities.js"]], function (a, f) {
                            var D = this && this.__extends || function () { var a = function (f, x) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, g) { a.__proto__ = g } || function (a, g) { for (var e in g) g.hasOwnProperty(e) && (a[e] = g[e]) }; return a(f, x) }; return function (f, x) { function p() { this.constructor = f } a(f, x); f.prototype = null === x ? Object.create(x) : (p.prototype = x.prototype, new p) } }(), F = a.seriesTypes.line, y = f.merge, I = f.pick; f = function (a) {
                                function f() {
                                    var f = null !== a && a.apply(this, arguments) ||
                                        this; f.data = void 0; f.options = void 0; f.points = void 0; return f
                                } D(f, a); f.prototype.getPointSpline = function (a, f, g) {
                                    var e = f.plotX || 0, c = f.plotY || 0, n = a[g - 1]; g = a[g + 1]; if (n && !n.isNull && !1 !== n.doCurve && !f.isCliff && g && !g.isNull && !1 !== g.doCurve && !f.isCliff) {
                                        a = n.plotY || 0; var k = g.plotX || 0; g = g.plotY || 0; var p = 0; var v = (1.5 * e + (n.plotX || 0)) / 2.5; var u = (1.5 * c + a) / 2.5; k = (1.5 * e + k) / 2.5; var w = (1.5 * c + g) / 2.5; k !== v && (p = (w - u) * (k - e) / (k - v) + c - w); u += p; w += p; u > a && u > c ? (u = Math.max(a, c), w = 2 * c - u) : u < a && u < c && (u = Math.min(a, c), w = 2 * c - u); w > g &&
                                            w > c ? (w = Math.max(g, c), u = 2 * c - w) : w < g && w < c && (w = Math.min(g, c), u = 2 * c - w); f.rightContX = k; f.rightContY = w
                                    } f = ["C", I(n.rightContX, n.plotX, 0), I(n.rightContY, n.plotY, 0), I(v, e, 0), I(u, c, 0), e, c]; n.rightContX = n.rightContY = void 0; return f
                                }; f.defaultOptions = y(F.defaultOptions); return f
                            }(F); a.registerSeriesType("spline", f); ""; return f
                        }); L(f, "Series/AreaSpline/AreaSplineSeries.js", [f["Series/Area/AreaSeries.js"], f["Series/Spline/SplineSeries.js"], f["Core/Legend/LegendSymbol.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Utilities.js"]],
                            function (a, f, B, F, y) {
                                var D = this && this.__extends || function () { var a = function (g, e) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, e) { a.__proto__ = e } || function (a, e) { for (var c in e) e.hasOwnProperty(c) && (a[c] = e[c]) }; return a(g, e) }; return function (g, e) { function c() { this.constructor = g } a(g, e); g.prototype = null === e ? Object.create(e) : (c.prototype = e.prototype, new c) } }(), J = a.prototype, C = y.extend, x = y.merge; y = function (p) {
                                    function g() {
                                        var a = null !== p && p.apply(this, arguments) || this; a.data = void 0; a.points =
                                            void 0; a.options = void 0; return a
                                    } D(g, p); g.defaultOptions = x(f.defaultOptions, a.defaultOptions); return g
                                }(f); C(y.prototype, { getGraphPath: J.getGraphPath, getStackPoints: J.getStackPoints, drawGraph: J.drawGraph, drawLegendSymbol: B.drawRectangle }); F.registerSeriesType("areaspline", y); ""; return y
                            }); L(f, "Series/Column/ColumnSeries.js", [f["Core/Animation/AnimationUtilities.js"], f["Core/Color/Color.js"], f["Core/Globals.js"], f["Core/Legend/LegendSymbol.js"], f["Core/Series/Series.js"], f["Core/Series/SeriesRegistry.js"],
                            f["Core/Utilities.js"]], function (a, f, B, F, y, I, J) {
                                var D = this && this.__extends || function () { var a = function (c, b) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (b, a) { b.__proto__ = a } || function (b, a) { for (var c in a) a.hasOwnProperty(c) && (b[c] = a[c]) }; return a(c, b) }; return function (c, b) { function e() { this.constructor = c } a(c, b); c.prototype = null === b ? Object.create(b) : (e.prototype = b.prototype, new e) } }(), x = a.animObject, p = f.parse, g = B.hasTouch; a = B.noop; var e = J.clamp, c = J.css, n = J.defined, k = J.extend, t = J.fireEvent,
                                    v = J.isArray, u = J.isNumber, w = J.merge, A = J.pick, r = J.objectEach; J = function (a) {
                                        function h() { var b = null !== a && a.apply(this, arguments) || this; b.borderWidth = void 0; b.data = void 0; b.group = void 0; b.options = void 0; b.points = void 0; return b } D(h, a); h.prototype.animate = function (b) {
                                            var a = this, c = this.yAxis, g = a.options, h = this.chart.inverted, f = {}, m = h ? "translateX" : "translateY"; if (b) f.scaleY = .001, b = e(c.toPixels(g.threshold), c.pos, c.pos + c.len), h ? f.translateX = b - c.len : f.translateY = b, a.clipBox && a.setClip(), a.group.attr(f); else {
                                                var n =
                                                    Number(a.group.attr(m)); a.group.animate({ scaleY: 1 }, k(x(a.options.animation), { step: function (b, d) { a.group && (f[m] = n + d.pos * (c.pos - n), a.group.attr(f)) } }))
                                            }
                                        }; h.prototype.init = function (b, c) { a.prototype.init.apply(this, arguments); var d = this; b = d.chart; b.hasRendered && b.series.forEach(function (b) { b.type === d.type && (b.isDirty = !0) }) }; h.prototype.getColumnMetrics = function () {
                                            var b = this, a = b.options, c = b.xAxis, e = b.yAxis, g = c.options.reversedStacks; g = c.reversed && !g || !c.reversed && g; var f = {}, h, k = 0; !1 === a.grouping ? k = 1 : b.chart.series.forEach(function (a) {
                                                var c =
                                                    a.yAxis, d = a.options; if (a.type === b.type && (a.visible || !b.chart.options.chart.ignoreHiddenSeries) && e.len === c.len && e.pos === c.pos) { if (d.stacking && "group" !== d.stacking) { h = a.stackKey; "undefined" === typeof f[h] && (f[h] = k++); var g = f[h] } else !1 !== d.grouping && (g = k++); a.columnIndex = g }
                                            }); var m = Math.min(Math.abs(c.transA) * (c.ordinal && c.ordinal.slope || a.pointRange || c.closestPointRange || c.tickInterval || 1), c.len), n = m * a.groupPadding, p = (m - 2 * n) / (k || 1); a = Math.min(a.maxPointWidth || c.len, A(a.pointWidth, p * (1 - 2 * a.pointPadding)));
                                            b.columnMetrics = { width: a, offset: (p - a) / 2 + (n + ((b.columnIndex || 0) + (g ? 1 : 0)) * p - m / 2) * (g ? -1 : 1), paddedWidth: p, columnCount: k }; return b.columnMetrics
                                        }; h.prototype.crispCol = function (b, a, c, e) { var d = this.chart, g = this.borderWidth, f = -(g % 2 ? .5 : 0); g = g % 2 ? .5 : 1; d.inverted && d.renderer.isVML && (g += 1); this.options.crisp && (c = Math.round(b + c) + f, b = Math.round(b) + f, c -= b); e = Math.round(a + e) + g; f = .5 >= Math.abs(a) && .5 < e; a = Math.round(a) + g; e -= a; f && e && (--a, e += 1); return { x: b, y: a, width: c, height: e } }; h.prototype.adjustForMissingColumns = function (b,
                                            a, c, e) { var d = this, g = this.options.stacking; if (!c.isNull && 1 < e.columnCount) { var f = this.yAxis.options.reversedStacks, h = 0, l = f ? 0 : -e.columnCount; r(this.yAxis.stacking && this.yAxis.stacking.stacks, function (b) { if ("number" === typeof c.x && (b = b[c.x.toString()])) { var a = b.points[d.index], e = b.total; g ? (a && (h = l), b.hasValidPoints && (f ? l++ : l--)) : v(a) && (h = a[1], l = e || 0) } }); b = (c.plotX || 0) + ((l - 1) * e.paddedWidth + a) / 2 - a - h * e.paddedWidth } return b }; h.prototype.translate = function () {
                                                var b = this, a = b.chart, c = b.options, g = b.dense = 2 > b.closestPointRange *
                                                    b.xAxis.transA; g = b.borderWidth = A(c.borderWidth, g ? 0 : 1); var f = b.xAxis, h = b.yAxis, k = c.threshold, m = b.translatedThreshold = h.getThreshold(k), p = A(c.minPointLength, 5), r = b.getColumnMetrics(), t = r.width, v = b.pointXOffset = r.offset, w = b.dataMin, x = b.dataMax, D = b.barW = Math.max(t, 1 + 2 * g); a.inverted && (m -= .5); c.pointPadding && (D = Math.ceil(D)); y.prototype.translate.apply(b); b.points.forEach(function (d) {
                                                        var g = A(d.yBottom, m), l = 999 + Math.abs(g), q = d.plotX || 0; l = e(d.plotY, -l, h.len + l); var z = Math.min(l, g), y = Math.max(l, g) - z, G = t, C =
                                                            q + v, B = D; p && Math.abs(y) < p && (y = p, q = !h.reversed && !d.negative || h.reversed && d.negative, u(k) && u(x) && d.y === k && x <= k && (h.min || 0) < k && (w !== x || (h.max || 0) <= k) && (q = !q), z = Math.abs(z - m) > p ? g - p : m - (q ? p : 0)); n(d.options.pointWidth) && (G = B = Math.ceil(d.options.pointWidth), C -= Math.round((G - t) / 2)); c.centerInCategory && (C = b.adjustForMissingColumns(C, G, d, r)); d.barX = C; d.pointWidth = G; d.tooltipPos = a.inverted ? [e(h.len + h.pos - a.plotLeft - l, h.pos - a.plotLeft, h.len + h.pos - a.plotLeft), f.len + f.pos - a.plotTop - C - B / 2, y] : [f.left - a.plotLeft + C +
                                                                B / 2, e(l + h.pos - a.plotTop, h.pos - a.plotTop, h.len + h.pos - a.plotTop), y]; d.shapeType = b.pointClass.prototype.shapeType || "rect"; d.shapeArgs = b.crispCol.apply(b, d.isNull ? [C, m, B, 0] : [C, z, B, y])
                                                    })
                                            }; h.prototype.drawGraph = function () { this.group[this.dense ? "addClass" : "removeClass"]("highcharts-dense-data") }; h.prototype.pointAttribs = function (b, a) {
                                                var c = this.options, e = this.pointAttrToOptions || {}, g = e.stroke || "borderColor", h = e["stroke-width"] || "borderWidth", f = b && b.color || this.color, l = b && b[g] || c[g] || f; e = b && b.options.dashStyle ||
                                                    c.dashStyle; var k = b && b[h] || c[h] || this[h] || 0, m = A(b && b.opacity, c.opacity, 1); if (b && this.zones.length) { var n = b.getZone(); f = b.options.color || n && (n.color || b.nonZonedColor) || this.color; n && (l = n.borderColor || l, e = n.dashStyle || e, k = n.borderWidth || k) } a && b && (b = w(c.states[a], b.options.states && b.options.states[a] || {}), a = b.brightness, f = b.color || "undefined" !== typeof a && p(f).brighten(b.brightness).get() || f, l = b[g] || l, k = b[h] || k, e = b.dashStyle || e, m = A(b.opacity, m)); g = { fill: f, stroke: l, "stroke-width": k, opacity: m }; e && (g.dashstyle =
                                                        e); return g
                                            }; h.prototype.drawPoints = function () {
                                                var b = this, a = this.chart, c = b.options, e = a.renderer, g = c.animationLimit || 250, h; b.points.forEach(function (d) {
                                                    var f = d.graphic, l = !!f, k = f && a.pointCount < g ? "animate" : "attr"; if (u(d.plotY) && null !== d.y) {
                                                        h = d.shapeArgs; f && d.hasNewShapeType() && (f = f.destroy()); b.enabledDataSorting && (d.startXPos = b.xAxis.reversed ? -(h ? h.width || 0 : 0) : b.xAxis.width); f || (d.graphic = f = e[d.shapeType](h).add(d.group || b.group)) && b.enabledDataSorting && a.hasRendered && a.pointCount < g && (f.attr({ x: d.startXPos }),
                                                            l = !0, k = "animate"); if (f && l) f[k](w(h)); if (c.borderRadius) f[k]({ r: c.borderRadius }); a.styledMode || f[k](b.pointAttribs(d, d.selected && "select")).shadow(!1 !== d.allowShadow && c.shadow, null, c.stacking && !c.borderRadius); f && (f.addClass(d.getClassName(), !0), f.attr({ visibility: d.visible ? "inherit" : "hidden" }))
                                                    } else f && (d.graphic = f.destroy())
                                                })
                                            }; h.prototype.drawTracker = function () {
                                                var b = this, a = b.chart, d = a.pointer, e = function (b) { var a = d.getPointFromEvent(b); "undefined" !== typeof a && (d.isDirectTouch = !0, a.onMouseOver(b)) },
                                                f; b.points.forEach(function (b) { f = v(b.dataLabels) ? b.dataLabels : b.dataLabel ? [b.dataLabel] : []; b.graphic && (b.graphic.element.point = b); f.forEach(function (a) { a.div ? a.div.point = b : a.element.point = b }) }); b._hasTracking || (b.trackerGroups.forEach(function (f) { if (b[f]) { b[f].addClass("highcharts-tracker").on("mouseover", e).on("mouseout", function (b) { d.onTrackerMouseOut(b) }); if (g) b[f].on("touchstart", e); !a.styledMode && b.options.cursor && b[f].css(c).css({ cursor: b.options.cursor }) } }), b._hasTracking = !0); t(this, "afterDrawTracker")
                                            };
                                        h.prototype.remove = function () { var b = this, a = b.chart; a.hasRendered && a.series.forEach(function (a) { a.type === b.type && (a.isDirty = !0) }); y.prototype.remove.apply(b, arguments) }; h.defaultOptions = w(y.defaultOptions, {
                                            borderRadius: 0, centerInCategory: !1, groupPadding: .2, marker: null, pointPadding: .1, minPointLength: 0, cropThreshold: 50, pointRange: null, states: { hover: { halo: !1, brightness: .1 }, select: { color: "#cccccc", borderColor: "#000000" } }, dataLabels: { align: void 0, verticalAlign: void 0, y: void 0 }, startFromThreshold: !0, stickyTracking: !1,
                                            tooltip: { distance: 6 }, threshold: 0, borderColor: "#ffffff"
                                        }); return h
                                    }(y); k(J.prototype, { cropShoulder: 0, directTouch: !0, drawLegendSymbol: F.drawRectangle, getSymbol: a, negStacks: !0, trackerGroups: ["group", "dataLabelsGroup"] }); I.registerSeriesType("column", J); ""; ""; return J
                            }); L(f, "Core/Series/DataLabel.js", [f["Core/Animation/AnimationUtilities.js"], f["Core/FormatUtilities.js"], f["Core/Utilities.js"]], function (a, f, B) {
                                var D = a.getDeferredAnimation, y = f.format, I = B.defined, J = B.extend, C = B.fireEvent, x = B.isArray, p =
                                    B.merge, g = B.objectEach, e = B.pick, c = B.splat, n; (function (a) {
                                        function f(a, b, c, d, g) {
                                            var f = this, h = this.chart, l = this.isCartesian && h.inverted, k = this.enabledDataSorting, m = e(a.dlBox && a.dlBox.centerX, a.plotX), n = a.plotY, p = c.rotation, r = c.align, t = I(m) && I(n) && h.isInsidePlot(m, Math.round(n), { inverted: l, paneCoordinates: !0, series: f }), v = function (c) { k && f.xAxis && !u && f.setDataLabelStartPos(a, b, g, t, c) }, u = "justify" === e(c.overflow, k ? "none" : "justify"), w = this.visible && !1 !== a.visible && (a.series.forceDL || k && !u || t || e(c.inside,
                                                !!this.options.stacking) && d && h.isInsidePlot(m, l ? d.x + 1 : d.y + d.height - 1, { inverted: l, paneCoordinates: !0, series: f })); if (w && I(m) && I(n)) {
                                                    p && b.attr({ align: r }); r = b.getBBox(!0); var A = [0, 0]; var x = h.renderer.fontMetrics(h.styledMode ? void 0 : c.style.fontSize, b).b; d = J({ x: l ? this.yAxis.len - n : m, y: Math.round(l ? this.xAxis.len - m : n), width: 0, height: 0 }, d); J(c, { width: r.width, height: r.height }); p ? (u = !1, A = h.renderer.rotCorr(x, p), m = { x: d.x + (c.x || 0) + d.width / 2 + A.x, y: d.y + (c.y || 0) + { top: 0, middle: .5, bottom: 1 }[c.verticalAlign] * d.height },
                                                        A = [r.x - Number(b.attr("x")), r.y - Number(b.attr("y"))], v(m), b[g ? "attr" : "animate"](m)) : (v(d), b.align(c, void 0, d), m = b.alignAttr); u && 0 <= d.height ? this.justifyDataLabel(b, c, m, r, d, g) : e(c.crop, !0) && (d = m.x, v = m.y, d += A[0], v += A[1], w = h.isInsidePlot(d, v, { paneCoordinates: !0, series: f }) && h.isInsidePlot(d + r.width, v + r.height, { paneCoordinates: !0, series: f })); if (c.shape && !p) b[g ? "attr" : "animate"]({ anchorX: l ? h.plotWidth - a.plotY : a.plotX, anchorY: l ? h.plotHeight - a.plotX : a.plotY })
                                                } g && k && (b.placed = !1); w || k && !u ? b.show() : (b.hide(),
                                                    b.placed = !1)
                                        } function k(a, b) { var c = b.filter; return c ? (b = c.operator, a = a[c.property], c = c.value, ">" === b && a > c || "<" === b && a < c || ">=" === b && a >= c || "<=" === b && a <= c || "==" === b && a == c || "===" === b && a === c ? !0 : !1) : !0 } function n() {
                                            var a = this, b = a.chart, f = a.options, d = a.points, m = a.hasRendered || 0, n = b.renderer, p = f.dataLabels, r, t = p.animation; t = p.defer ? D(b, t, a) : { defer: 0, duration: 0 }; p = A(A(b.options.plotOptions && b.options.plotOptions.series && b.options.plotOptions.series.dataLabels, b.options.plotOptions && b.options.plotOptions[a.type] &&
                                                b.options.plotOptions[a.type].dataLabels), p); C(this, "drawDataLabels"); if (x(p) || p.enabled || a._hasPointLabels) {
                                                    var v = a.plotGroup("dataLabelsGroup", "data-labels", m ? "inherit" : "hidden", p.zIndex || 6); v.attr({ opacity: +m }); !m && (m = a.dataLabelsGroup) && (a.visible && v.show(), m[f.animation ? "animate" : "attr"]({ opacity: 1 }, t)); d.forEach(function (d) {
                                                        r = c(A(p, d.dlOptions || d.options && d.options.dataLabels)); r.forEach(function (c, h) {
                                                            var l = c.enabled && (!d.isNull || d.dataLabelOnNull) && k(d, c), m = d.connectors ? d.connectors[h] : d.connector,
                                                            p = d.dataLabels ? d.dataLabels[h] : d.dataLabel, r = !p, q = e(c.distance, d.labelDistance); if (l) {
                                                                var t = d.getLabelConfig(); var u = e(c[d.formatPrefix + "Format"], c.format); t = I(u) ? y(u, t, b) : (c[d.formatPrefix + "Formatter"] || c.formatter).call(t, c); u = c.style; var w = c.rotation; b.styledMode || (u.color = e(c.color, u.color, a.color, "#000000"), "contrast" === u.color ? (d.contrastColor = n.getContrast(d.color || a.color), u.color = !I(q) && c.inside || 0 > q || f.stacking ? d.contrastColor : "#000000") : delete d.contrastColor, f.cursor && (u.cursor = f.cursor));
                                                                var z = { r: c.borderRadius || 0, rotation: w, padding: c.padding, zIndex: 1 }; b.styledMode || (z.fill = c.backgroundColor, z.stroke = c.borderColor, z["stroke-width"] = c.borderWidth); g(z, function (b, a) { "undefined" === typeof b && delete z[a] })
                                                            } !p || l && I(t) && !!p.div === !!c.useHTML && (p.rotation && c.rotation || p.rotation === c.rotation) || (r = !0, d.dataLabel = p = d.dataLabel && d.dataLabel.destroy(), d.dataLabels && (1 === d.dataLabels.length ? delete d.dataLabels : delete d.dataLabels[h]), h || delete d.dataLabel, m && (d.connector = d.connector.destroy(),
                                                                d.connectors && (1 === d.connectors.length ? delete d.connectors : delete d.connectors[h]))); l && I(t) ? (p ? z.text = t : (d.dataLabels = d.dataLabels || [], p = d.dataLabels[h] = w ? n.text(t, 0, 0, c.useHTML).addClass("highcharts-data-label") : n.label(t, 0, 0, c.shape, null, null, c.useHTML, null, "data-label"), h || (d.dataLabel = p), p.addClass(" highcharts-data-label-color-" + d.colorIndex + " " + (c.className || "") + (c.useHTML ? " highcharts-tracker" : ""))), p.options = c, p.attr(z), b.styledMode || p.css(u).shadow(c.shadow), p.added || p.add(v), c.textPath &&
                                                                    !c.useHTML && (p.setTextPath(d.getDataLabelPath && d.getDataLabelPath(p) || d.graphic, c.textPath), d.dataLabelPath && !c.textPath.enabled && (d.dataLabelPath = d.dataLabelPath.destroy())), a.alignDataLabel(d, p, c, null, r)) : p && p.hide()
                                                        })
                                                    })
                                                } C(this, "afterDrawDataLabels")
                                        } function w(a, b, c, d, e, g) {
                                            var f = this.chart, h = b.align, l = b.verticalAlign, k = a.box ? 0 : a.padding || 0, m = b.x; m = void 0 === m ? 0 : m; var n = b.y; n = void 0 === n ? 0 : n; var p = (c.x || 0) + k; if (0 > p) { "right" === h && 0 <= m ? (b.align = "left", b.inside = !0) : m -= p; var r = !0 } p = (c.x || 0) + d.width - k;
                                            p > f.plotWidth && ("left" === h && 0 >= m ? (b.align = "right", b.inside = !0) : m += f.plotWidth - p, r = !0); p = c.y + k; 0 > p && ("bottom" === l && 0 <= n ? (b.verticalAlign = "top", b.inside = !0) : n -= p, r = !0); p = (c.y || 0) + d.height - k; p > f.plotHeight && ("top" === l && 0 >= n ? (b.verticalAlign = "bottom", b.inside = !0) : n += f.plotHeight - p, r = !0); r && (b.x = m, b.y = n, a.placed = !g, a.align(b, void 0, e)); return r
                                        } function A(a, b) {
                                            var c = [], d; if (x(a) && !x(b)) c = a.map(function (a) { return p(a, b) }); else if (x(b) && !x(a)) c = b.map(function (b) { return p(a, b) }); else if (x(a) || x(b)) for (d =
                                                Math.max(a.length, b.length); d--;)c[d] = p(a[d], b[d]); else c = p(a, b); return c
                                        } function r(a, b, c, d, e) { var g = this.chart, f = g.inverted, h = this.xAxis, l = h.reversed, k = f ? b.height / 2 : b.width / 2; a = (a = a.pointWidth) ? a / 2 : 0; b.startXPos = f ? e.x : l ? -k - a : h.width - k + a; b.startYPos = f ? l ? this.yAxis.height - k + a : -k - a : e.y; d ? "hidden" === b.visibility && (b.show(), b.attr({ opacity: 0 }).animate({ opacity: 1 })) : b.attr({ opacity: 1 }).animate({ opacity: 0 }, void 0, b.hide); g.hasRendered && (c && b.attr({ x: b.startXPos, y: b.startYPos }), b.placed = !0) } var m = []; a.compose =
                                            function (a) { if (-1 === m.indexOf(a)) { var b = a.prototype; m.push(a); b.alignDataLabel = f; b.drawDataLabels = n; b.justifyDataLabel = w; b.setDataLabelStartPos = r } }
                                    })(n || (n = {})); ""; return n
                            }); L(f, "Series/Column/ColumnDataLabel.js", [f["Core/Series/DataLabel.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Utilities.js"]], function (a, f, B) {
                                var D = f.series, y = B.merge, I = B.pick, J; (function (f) {
                                    function x(a, e, c, f, k) {
                                        var g = this.chart.inverted, n = a.series, p = (n.xAxis ? n.xAxis.len : this.chart.plotSizeX) || 0; n = (n.yAxis ? n.yAxis.len : this.chart.plotSizeY) ||
                                            0; var w = a.dlBox || a.shapeArgs, A = I(a.below, a.plotY > I(this.translatedThreshold, n)), r = I(c.inside, !!this.options.stacking); w && (f = y(w), 0 > f.y && (f.height += f.y, f.y = 0), w = f.y + f.height - n, 0 < w && w < f.height && (f.height -= w), g && (f = { x: n - f.y - f.height, y: p - f.x - f.width, width: f.height, height: f.width }), r || (g ? (f.x += A ? 0 : f.width, f.width = 0) : (f.y += A ? f.height : 0, f.height = 0))); c.align = I(c.align, !g || r ? "center" : A ? "right" : "left"); c.verticalAlign = I(c.verticalAlign, g || r ? "middle" : A ? "top" : "bottom"); D.prototype.alignDataLabel.call(this, a,
                                                e, c, f, k); c.inside && a.contrastColor && e.css({ color: a.contrastColor })
                                    } var p = []; f.compose = function (g) { a.compose(D); -1 === p.indexOf(g) && (p.push(g), g.prototype.alignDataLabel = x) }
                                })(J || (J = {})); return J
                            }); L(f, "Series/Bar/BarSeries.js", [f["Series/Column/ColumnSeries.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Utilities.js"]], function (a, f, B) {
                                var D = this && this.__extends || function () {
                                    var a = function (f, x) {
                                        a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, g) { a.__proto__ = g } || function (a, g) {
                                            for (var e in g) g.hasOwnProperty(e) &&
                                                (a[e] = g[e])
                                        }; return a(f, x)
                                    }; return function (f, x) { function p() { this.constructor = f } a(f, x); f.prototype = null === x ? Object.create(x) : (p.prototype = x.prototype, new p) }
                                }(), y = B.extend, I = B.merge; B = function (f) { function y() { var a = null !== f && f.apply(this, arguments) || this; a.data = void 0; a.options = void 0; a.points = void 0; return a } D(y, f); y.defaultOptions = I(a.defaultOptions, {}); return y }(a); y(B.prototype, { inverted: !0 }); f.registerSeriesType("bar", B); ""; return B
                            }); L(f, "Series/Scatter/ScatterSeries.js", [f["Series/Column/ColumnSeries.js"],
                            f["Series/Line/LineSeries.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Utilities.js"]], function (a, f, B, F) {
                                var y = this && this.__extends || function () { var a = function (f, g) { a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, c) { a.__proto__ = c } || function (a, c) { for (var e in c) c.hasOwnProperty(e) && (a[e] = c[e]) }; return a(f, g) }; return function (f, g) { function e() { this.constructor = f } a(f, g); f.prototype = null === g ? Object.create(g) : (e.prototype = g.prototype, new e) } }(), D = F.addEvent, J = F.extend, C = F.merge; F =
                                    function (a) {
                                        function p() { var g = null !== a && a.apply(this, arguments) || this; g.data = void 0; g.options = void 0; g.points = void 0; return g } y(p, a); p.prototype.applyJitter = function () {
                                            var a = this, e = this.options.jitter, c = this.points.length; e && this.points.forEach(function (g, f) {
                                                ["x", "y"].forEach(function (k, n) {
                                                    var p = "plot" + k.toUpperCase(); if (e[k] && !g.isNull) {
                                                        var t = a[k + "Axis"]; var v = e[k] * t.transA; if (t && !t.isLog) {
                                                            var r = Math.max(0, g[p] - v); t = Math.min(t.len, g[p] + v); n = 1E4 * Math.sin(f + n * c); g[p] = r + (t - r) * (n - Math.floor(n)); "x" ===
                                                                k && (g.clientX = g.plotX)
                                                        }
                                                    }
                                                })
                                            })
                                        }; p.prototype.drawGraph = function () { this.options.lineWidth ? a.prototype.drawGraph.call(this) : this.graph && (this.graph = this.graph.destroy()) }; p.defaultOptions = C(f.defaultOptions, { lineWidth: 0, findNearestPointBy: "xy", jitter: { x: 0, y: 0 }, marker: { enabled: !0 }, tooltip: { headerFormat: '<span style="color:{point.color}">\u25cf</span> <span style="font-size: 10px"> {series.name}</span><br/>', pointFormat: "x: <b>{point.x}</b><br/>y: <b>{point.y}</b><br/>" } }); return p
                                    }(f); J(F.prototype, {
                                        drawTracker: a.prototype.drawTracker,
                                        sorted: !1, requireSorting: !1, noSharedTooltip: !0, trackerGroups: ["group", "markerGroup", "dataLabelsGroup"], takeOrdinalPosition: !1
                                    }); D(F, "afterTranslate", function () { this.applyJitter() }); B.registerSeriesType("scatter", F); ""; return F
                            }); L(f, "Series/CenteredUtilities.js", [f["Core/Globals.js"], f["Core/Series/Series.js"], f["Core/Utilities.js"]], function (a, f, B) {
                                var D = a.deg2rad, y = B.fireEvent, I = B.isNumber, J = B.pick, C = B.relativeLength, x; (function (a) {
                                    a.getCenter = function () {
                                        var a = this.options, e = this.chart, c = 2 * (a.slicedOffset ||
                                            0), n = e.plotWidth - 2 * c, k = e.plotHeight - 2 * c, p = a.center, v = Math.min(n, k), u = a.thickness, w = a.size, A = a.innerSize || 0; "string" === typeof w && (w = parseFloat(w)); "string" === typeof A && (A = parseFloat(A)); a = [J(p[0], "50%"), J(p[1], "50%"), J(w && 0 > w ? void 0 : a.size, "100%"), J(A && 0 > A ? void 0 : a.innerSize || 0, "0%")]; !e.angular || this instanceof f || (a[3] = 0); for (p = 0; 4 > p; ++p)w = a[p], e = 2 > p || 2 === p && /%$/.test(w), a[p] = C(w, [n, k, v, a[2]][p]) + (e ? c : 0); a[3] > a[2] && (a[3] = a[2]); I(u) && 2 * u < a[2] && 0 < u && (a[3] = a[2] - 2 * u); y(this, "afterGetCenter", { positions: a });
                                        return a
                                    }; a.getStartAndEndRadians = function (a, e) { a = I(a) ? a : 0; e = I(e) && e > a && 360 > e - a ? e : a + 360; return { start: D * (a + -90), end: D * (e + -90) } }
                                })(x || (x = {})); ""; return x
                            }); L(f, "Series/Pie/PiePoint.js", [f["Core/Animation/AnimationUtilities.js"], f["Core/Series/Point.js"], f["Core/Utilities.js"]], function (a, f, B) {
                                var D = this && this.__extends || function () {
                                    var a = function (e, c) {
                                        a = Object.setPrototypeOf || { __proto__: [] } instanceof Array && function (a, c) { a.__proto__ = c } || function (a, c) { for (var e in c) c.hasOwnProperty(e) && (a[e] = c[e]) };
                                        return a(e, c)
                                    }; return function (e, c) { function g() { this.constructor = e } a(e, c); e.prototype = null === c ? Object.create(c) : (g.prototype = c.prototype, new g) }
                                }(), y = a.setAnimation, I = B.addEvent, J = B.defined; a = B.extend; var C = B.isNumber, x = B.pick, p = B.relativeLength; f = function (a) {
                                    function e() { var c = null !== a && a.apply(this, arguments) || this; c.labelDistance = void 0; c.options = void 0; c.series = void 0; return c } D(e, a); e.prototype.getConnectorPath = function () {
                                        var a = this.labelPosition, e = this.series.options.dataLabels, g = this.connectorShapes,
                                        f = e.connectorShape; g[f] && (f = g[f]); return f.call(this, { x: a.final.x, y: a.final.y, alignment: a.alignment }, a.connectorPosition, e)
                                    }; e.prototype.getTranslate = function () { return this.sliced ? this.slicedTranslation : { translateX: 0, translateY: 0 } }; e.prototype.haloPath = function (a) { var c = this.shapeArgs; return this.sliced || !this.visible ? [] : this.series.chart.renderer.symbols.arc(c.x, c.y, c.r + a, c.r + a, { innerR: c.r - 1, start: c.start, end: c.end }) }; e.prototype.init = function () {
                                        var c = this; a.prototype.init.apply(this, arguments);
                                        this.name = x(this.name, "Slice"); var e = function (a) { c.slice("select" === a.type) }; I(this, "select", e); I(this, "unselect", e); return this
                                    }; e.prototype.isValid = function () { return C(this.y) && 0 <= this.y }; e.prototype.setVisible = function (a, e) {
                                        var c = this, g = this.series, f = g.chart, n = g.options.ignoreHiddenPoint; e = x(e, n); a !== this.visible && (this.visible = this.options.visible = a = "undefined" === typeof a ? !this.visible : a, g.options.data[g.data.indexOf(this)] = this.options, ["graphic", "dataLabel", "connector", "shadowGroup"].forEach(function (e) {
                                            if (c[e]) c[e][a ?
                                                "show" : "hide"](a)
                                        }), this.legendItem && f.legend.colorizeItem(this, a), a || "hover" !== this.state || this.setState(""), n && (g.isDirty = !0), e && f.redraw())
                                    }; e.prototype.slice = function (a, e, g) { var c = this.series; y(g, c.chart); x(e, !0); this.sliced = this.options.sliced = J(a) ? a : !this.sliced; c.options.data[c.data.indexOf(this)] = this.options; this.graphic && this.graphic.animate(this.getTranslate()); this.shadowGroup && this.shadowGroup.animate(this.getTranslate()) }; return e
                                }(f); a(f.prototype, {
                                    connectorShapes: {
                                        fixedOffset: function (a,
                                            e, c) { var g = e.breakAt; e = e.touchingSliceAt; return [["M", a.x, a.y], c.softConnector ? ["C", a.x + ("left" === a.alignment ? -5 : 5), a.y, 2 * g.x - e.x, 2 * g.y - e.y, g.x, g.y] : ["L", g.x, g.y], ["L", e.x, e.y]] }, straight: function (a, e) { e = e.touchingSliceAt; return [["M", a.x, a.y], ["L", e.x, e.y]] }, crookedLine: function (a, e, c) {
                                                e = e.touchingSliceAt; var g = this.series, f = g.center[0], t = g.chart.plotWidth, v = g.chart.plotLeft; g = a.alignment; var u = this.shapeArgs.r; c = p(c.crookDistance, 1); t = "left" === g ? f + u + (t + v - f - u) * (1 - c) : v + (f - u) * c; c = ["L", t, a.y]; f = !0; if ("left" ===
                                                    g ? t > a.x || t < e.x : t < a.x || t > e.x) f = !1; a = [["M", a.x, a.y]]; f && a.push(c); a.push(["L", e.x, e.y]); return a
                                            }
                                    }
                                }); return f
                            }); L(f, "Series/Pie/PieSeries.js", [f["Series/CenteredUtilities.js"], f["Series/Column/ColumnSeries.js"], f["Core/Globals.js"], f["Core/Legend/LegendSymbol.js"], f["Series/Pie/PiePoint.js"], f["Core/Series/Series.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Renderer/SVG/Symbols.js"], f["Core/Utilities.js"]], function (a, f, B, F, y, I, J, C, x) {
                                var p = this && this.__extends || function () {
                                    var a = function (c, e) {
                                        a = Object.setPrototypeOf ||
                                        { __proto__: [] } instanceof Array && function (a, c) { a.__proto__ = c } || function (a, c) { for (var e in c) c.hasOwnProperty(e) && (a[e] = c[e]) }; return a(c, e)
                                    }; return function (c, e) { function g() { this.constructor = c } a(c, e); c.prototype = null === e ? Object.create(e) : (g.prototype = e.prototype, new g) }
                                }(), g = a.getStartAndEndRadians; B = B.noop; var e = x.clamp, c = x.extend, n = x.fireEvent, k = x.merge, t = x.pick, v = x.relativeLength; x = function (a) {
                                    function c() {
                                        var c = null !== a && a.apply(this, arguments) || this; c.center = void 0; c.data = void 0; c.maxLabelDistance =
                                            void 0; c.options = void 0; c.points = void 0; return c
                                    } p(c, a); c.prototype.animate = function (a) { var c = this, e = c.points, g = c.startAngleRad; a || e.forEach(function (a) { var b = a.graphic, d = a.shapeArgs; b && d && (b.attr({ r: t(a.startR, c.center && c.center[3] / 2), start: g, end: g }), b.animate({ r: d.r, start: d.start, end: d.end }, c.options.animation)) }) }; c.prototype.drawEmpty = function () {
                                        var a = this.startAngleRad, c = this.endAngleRad, e = this.options; if (0 === this.total && this.center) {
                                            var g = this.center[0]; var b = this.center[1]; this.graph || (this.graph =
                                                this.chart.renderer.arc(g, b, this.center[1] / 2, 0, a, c).addClass("highcharts-empty-series").add(this.group)); this.graph.attr({ d: C.arc(g, b, this.center[2] / 2, 0, { start: a, end: c, innerR: this.center[3] / 2 }) }); this.chart.styledMode || this.graph.attr({ "stroke-width": e.borderWidth, fill: e.fillColor || "none", stroke: e.color || "#cccccc" })
                                        } else this.graph && (this.graph = this.graph.destroy())
                                    }; c.prototype.drawPoints = function () {
                                        var a = this.chart.renderer; this.points.forEach(function (c) {
                                            c.graphic && c.hasNewShapeType() && (c.graphic =
                                                c.graphic.destroy()); c.graphic || (c.graphic = a[c.shapeType](c.shapeArgs).add(c.series.group), c.delayedRendering = !0)
                                        })
                                    }; c.prototype.generatePoints = function () { a.prototype.generatePoints.call(this); this.updateTotals() }; c.prototype.getX = function (a, c, g) { var f = this.center, b = this.radii ? this.radii[g.index] || 0 : f[2] / 2; a = Math.asin(e((a - f[1]) / (b + g.labelDistance), -1, 1)); return f[0] + (c ? -1 : 1) * Math.cos(a) * (b + g.labelDistance) + (0 < g.labelDistance ? (c ? -1 : 1) * this.options.dataLabels.padding : 0) }; c.prototype.hasData = function () { return !!this.processedXData.length };
                                    c.prototype.redrawPoints = function () {
                                        var a = this, c = a.chart, e = c.renderer, g = a.options.shadow, b, f, d, n; this.drawEmpty(); !g || a.shadowGroup || c.styledMode || (a.shadowGroup = e.g("shadow").attr({ zIndex: -1 }).add(a.group)); a.points.forEach(function (h) {
                                            var l = {}; f = h.graphic; if (!h.isNull && f) {
                                                var m = void 0; n = h.shapeArgs; b = h.getTranslate(); c.styledMode || (m = h.shadowGroup, g && !m && (m = h.shadowGroup = e.g("shadow").add(a.shadowGroup)), m && m.attr(b), d = a.pointAttribs(h, h.selected && "select")); h.delayedRendering ? (f.setRadialReference(a.center).attr(n).attr(b),
                                                    c.styledMode || f.attr(d).attr({ "stroke-linejoin": "round" }).shadow(g, m), h.delayedRendering = !1) : (f.setRadialReference(a.center), c.styledMode || k(!0, l, d), k(!0, l, n, b), f.animate(l)); f.attr({ visibility: h.visible ? "inherit" : "hidden" }); f.addClass(h.getClassName(), !0)
                                            } else f && (h.graphic = f.destroy())
                                        })
                                    }; c.prototype.sortByAngle = function (a, c) { a.sort(function (a, e) { return "undefined" !== typeof a.angle && (e.angle - a.angle) * c }) }; c.prototype.translate = function (a) {
                                        n(this, "translate"); this.generatePoints(); var c = this.options,
                                            e = c.slicedOffset, f = e + (c.borderWidth || 0), b = g(c.startAngle, c.endAngle), l = this.startAngleRad = b.start; b = (this.endAngleRad = b.end) - l; var d = this.points, k = c.dataLabels.distance; c = c.ignoreHiddenPoint; var p = d.length, q, u = 0; a || (this.center = a = this.getCenter()); for (q = 0; q < p; q++) {
                                                var w = d[q]; var y = l + u * b; !w.isValid() || c && !w.visible || (u += w.percentage / 100); var x = l + u * b; var A = { x: a[0], y: a[1], r: a[2] / 2, innerR: a[3] / 2, start: Math.round(1E3 * y) / 1E3, end: Math.round(1E3 * x) / 1E3 }; w.shapeType = "arc"; w.shapeArgs = A; w.labelDistance = t(w.options.dataLabels &&
                                                    w.options.dataLabels.distance, k); w.labelDistance = v(w.labelDistance, A.r); this.maxLabelDistance = Math.max(this.maxLabelDistance || 0, w.labelDistance); x = (x + y) / 2; x > 1.5 * Math.PI ? x -= 2 * Math.PI : x < -Math.PI / 2 && (x += 2 * Math.PI); w.slicedTranslation = { translateX: Math.round(Math.cos(x) * e), translateY: Math.round(Math.sin(x) * e) }; A = Math.cos(x) * a[2] / 2; var D = Math.sin(x) * a[2] / 2; w.tooltipPos = [a[0] + .7 * A, a[1] + .7 * D]; w.half = x < -Math.PI / 2 || x > Math.PI / 2 ? 1 : 0; w.angle = x; y = Math.min(f, w.labelDistance / 5); w.labelPosition = {
                                                        natural: {
                                                            x: a[0] + A +
                                                                Math.cos(x) * w.labelDistance, y: a[1] + D + Math.sin(x) * w.labelDistance
                                                        }, "final": {}, alignment: 0 > w.labelDistance ? "center" : w.half ? "right" : "left", connectorPosition: { breakAt: { x: a[0] + A + Math.cos(x) * y, y: a[1] + D + Math.sin(x) * y }, touchingSliceAt: { x: a[0] + A, y: a[1] + D } }
                                                    }
                                            } n(this, "afterTranslate")
                                    }; c.prototype.updateTotals = function () {
                                        var a = this.points, c = a.length, e = this.options.ignoreHiddenPoint, g, b = 0; for (g = 0; g < c; g++) { var f = a[g]; !f.isValid() || e && !f.visible || (b += f.y) } this.total = b; for (g = 0; g < c; g++)f = a[g], f.percentage = 0 < b && (f.visible ||
                                            !e) ? f.y / b * 100 : 0, f.total = b
                                    }; c.defaultOptions = k(I.defaultOptions, {
                                        center: [null, null], clip: !1, colorByPoint: !0, dataLabels: { allowOverlap: !0, connectorPadding: 5, connectorShape: "fixedOffset", crookDistance: "70%", distance: 30, enabled: !0, formatter: function () { return this.point.isNull ? void 0 : this.point.name }, softConnector: !0, x: 0 }, fillColor: void 0, ignoreHiddenPoint: !0, inactiveOtherPoints: !0, legendType: "point", marker: null, size: null, showInLegend: !1, slicedOffset: 10, stickyTracking: !1, tooltip: { followPointer: !0 }, borderColor: "#ffffff",
                                        borderWidth: 1, lineWidth: void 0, states: { hover: { brightness: .1 } }
                                    }); return c
                                }(I); c(x.prototype, { axisTypes: [], directTouch: !0, drawGraph: void 0, drawLegendSymbol: F.drawRectangle, drawTracker: f.prototype.drawTracker, getCenter: a.getCenter, getSymbol: B, isCartesian: !1, noSharedTooltip: !0, pointAttribs: f.prototype.pointAttribs, pointClass: y, requireSorting: !1, searchPoint: B, trackerGroups: ["group", "dataLabelsGroup"] }); J.registerSeriesType("pie", x); ""; return x
                            }); L(f, "Series/Pie/PieDataLabel.js", [f["Core/Series/DataLabel.js"],
                            f["Core/Globals.js"], f["Core/Renderer/RendererUtilities.js"], f["Core/Series/SeriesRegistry.js"], f["Core/Utilities.js"]], function (a, f, B, F, y) {
                                var D = f.noop, J = B.distribute, C = F.series, x = y.arrayMax, p = y.clamp, g = y.defined, e = y.merge, c = y.pick, n = y.relativeLength, k; (function (f) {
                                    function k() {
                                        var a = this, f = a.data, b = a.chart, l = a.options.dataLabels || {}, d = l.connectorPadding, k = b.plotWidth, n = b.plotHeight, p = b.plotLeft, r = Math.round(b.chartWidth / 3), t = a.center, u = t[2] / 2, v = t[1], w = [[], []], y = [0, 0, 0, 0], A = a.dataLabelPositioners,
                                        D, B, F, I, L, E, T, M, U, V, Y, R; a.visible && (l.enabled || a._hasPointLabels) && (f.forEach(function (a) { a.dataLabel && a.visible && a.dataLabel.shortened && (a.dataLabel.attr({ width: "auto" }).css({ width: "auto", textOverflow: "clip" }), a.dataLabel.shortened = !1) }), C.prototype.drawDataLabels.apply(a), f.forEach(function (a) {
                                            a.dataLabel && (a.visible ? (w[a.half].push(a), a.dataLabel._pos = null, !g(l.style.width) && !g(a.options.dataLabels && a.options.dataLabels.style && a.options.dataLabels.style.width) && a.dataLabel.getBBox().width > r && (a.dataLabel.css({
                                                width: Math.round(.7 *
                                                    r) + "px"
                                            }), a.dataLabel.shortened = !0)) : (a.dataLabel = a.dataLabel.destroy(), a.dataLabels && 1 === a.dataLabels.length && delete a.dataLabels))
                                        }), w.forEach(function (e, f) {
                                            var h = e.length, m = [], q; if (h) {
                                                a.sortByAngle(e, f - .5); if (0 < a.maxLabelDistance) {
                                                    var r = Math.max(0, v - u - a.maxLabelDistance); var w = Math.min(v + u + a.maxLabelDistance, b.plotHeight); e.forEach(function (a) {
                                                        0 < a.labelDistance && a.dataLabel && (a.top = Math.max(0, v - u - a.labelDistance), a.bottom = Math.min(v + u + a.labelDistance, b.plotHeight), q = a.dataLabel.getBBox().height ||
                                                            21, a.distributeBox = { target: a.labelPosition.natural.y - a.top + q / 2, size: q, rank: a.y }, m.push(a.distributeBox))
                                                    }); r = w + q - r; J(m, r, r / 5)
                                                } for (Y = 0; Y < h; Y++) {
                                                    D = e[Y]; E = D.labelPosition; I = D.dataLabel; V = !1 === D.visible ? "hidden" : "inherit"; U = r = E.natural.y; m && g(D.distributeBox) && ("undefined" === typeof D.distributeBox.pos ? V = "hidden" : (T = D.distributeBox.size, U = A.radialDistributionY(D))); delete D.positionIndex; if (l.justify) M = A.justify(D, u, t); else switch (l.alignTo) {
                                                        case "connectors": M = A.alignToConnectors(e, f, k, p); break; case "plotEdges": M =
                                                            A.alignToPlotEdges(I, f, k, p); break; default: M = A.radialDistributionX(a, D, U, r)
                                                    }I._attr = { visibility: V, align: E.alignment }; R = D.options.dataLabels || {}; I._pos = { x: M + c(R.x, l.x) + ({ left: d, right: -d }[E.alignment] || 0), y: U + c(R.y, l.y) - 10 }; E.final.x = M; E.final.y = U; c(l.crop, !0) && (L = I.getBBox().width, r = null, M - L < d && 1 === f ? (r = Math.round(L - M + d), y[3] = Math.max(r, y[3])) : M + L > k - d && 0 === f && (r = Math.round(M + L - k + d), y[1] = Math.max(r, y[1])), 0 > U - T / 2 ? y[0] = Math.max(Math.round(-U + T / 2), y[0]) : U + T / 2 > n && (y[2] = Math.max(Math.round(U + T / 2 - n), y[2])),
                                                        I.sideOverflow = r)
                                                }
                                            }
                                        }), 0 === x(y) || this.verifyDataLabelOverflow(y)) && (this.placeDataLabels(), this.points.forEach(function (d) {
                                            R = e(l, d.options.dataLabels); if (B = c(R.connectorWidth, 1)) {
                                                var g; F = d.connector; if ((I = d.dataLabel) && I._pos && d.visible && 0 < d.labelDistance) {
                                                    V = I._attr.visibility; if (g = !F) d.connector = F = b.renderer.path().addClass("highcharts-data-label-connector  highcharts-color-" + d.colorIndex + (d.className ? " " + d.className : "")).add(a.dataLabelsGroup), b.styledMode || F.attr({
                                                        "stroke-width": B, stroke: R.connectorColor ||
                                                            d.color || "#666666"
                                                    }); F[g ? "attr" : "animate"]({ d: d.getConnectorPath() }); F.attr("visibility", V)
                                                } else F && (d.connector = F.destroy())
                                            }
                                        }))
                                    } function t() {
                                        this.points.forEach(function (a) {
                                            var c = a.dataLabel, b; c && a.visible && ((b = c._pos) ? (c.sideOverflow && (c._attr.width = Math.max(c.getBBox().width - c.sideOverflow, 0), c.css({ width: c._attr.width + "px", textOverflow: (this.options.dataLabels.style || {}).textOverflow || "ellipsis" }), c.shortened = !0), c.attr(c._attr), c[c.moved ? "animate" : "attr"](b), c.moved = !0) : c && c.attr({ y: -9999 }));
                                            delete a.distributeBox
                                        }, this)
                                    } function w(a) { var c = this.center, b = this.options, e = b.center, d = b.minSize || 80, g = null !== b.size; if (!g) { if (null !== e[0]) var f = Math.max(c[2] - Math.max(a[1], a[3]), d); else f = Math.max(c[2] - a[1] - a[3], d), c[0] += (a[3] - a[1]) / 2; null !== e[1] ? f = p(f, d, c[2] - Math.max(a[0], a[2])) : (f = p(f, d, c[2] - a[0] - a[2]), c[1] += (a[0] - a[2]) / 2); f < c[2] ? (c[2] = f, c[3] = Math.min(b.thickness ? Math.max(0, f - 2 * b.thickness) : Math.max(0, n(b.innerSize || 0, f)), f), this.translate(c), this.drawDataLabels && this.drawDataLabels()) : g = !0 } return g }
                                    var y = [], r = { radialDistributionY: function (a) { return a.top + a.distributeBox.pos }, radialDistributionX: function (a, c, b, e) { return a.getX(b < c.top + 2 || b > c.bottom - 2 ? e : b, c.half, c) }, justify: function (a, c, b) { return b[0] + (a.half ? -1 : 1) * (c + a.labelDistance) }, alignToPlotEdges: function (a, c, b, e) { a = a.getBBox().width; return c ? a + e : b - a - e }, alignToConnectors: function (a, c, b, e) { var d = 0, g; a.forEach(function (a) { g = a.dataLabel.getBBox().width; g > d && (d = g) }); return c ? d + e : b - d - e } }; f.compose = function (c) {
                                        a.compose(C); -1 === y.indexOf(c) &&
                                            (y.push(c), c = c.prototype, c.dataLabelPositioners = r, c.alignDataLabel = D, c.drawDataLabels = k, c.placeDataLabels = t, c.verifyDataLabelOverflow = w)
                                    }
                                })(k || (k = {})); return k
                            }); L(f, "Extensions/OverlappingDataLabels.js", [f["Core/Chart/Chart.js"], f["Core/Utilities.js"]], function (a, f) {
                                function D(a, g) {
                                    var e = !1; if (a) {
                                        var c = a.newOpacity; a.oldOpacity !== c && (a.alignAttr && a.placed ? (a[c ? "removeClass" : "addClass"]("highcharts-data-label-hidden"), e = !0, a.alignAttr.opacity = c, a[a.isOld ? "animate" : "attr"](a.alignAttr, null, function () {
                                            g.styledMode ||
                                            a.css({ pointerEvents: c ? "auto" : "none" })
                                        }), y(g, "afterHideOverlappingLabel")) : a.attr({ opacity: c })); a.isOld = !0
                                    } return e
                                } var F = f.addEvent, y = f.fireEvent, I = f.isArray, J = f.isNumber, C = f.objectEach, x = f.pick; F(a, "render", function () {
                                    var a = this, g = []; (this.labelCollectors || []).forEach(function (a) { g = g.concat(a()) }); (this.yAxis || []).forEach(function (a) { a.stacking && a.options.stackLabels && !a.options.stackLabels.allowOverlap && C(a.stacking.stacks, function (a) { C(a, function (a) { a.label && g.push(a.label) }) }) }); (this.series ||
                                        []).forEach(function (e) { var c = e.options.dataLabels; e.visible && (!1 !== c.enabled || e._hasPointLabels) && (c = function (c) { return c.forEach(function (c) { c.visible && (I(c.dataLabels) ? c.dataLabels : c.dataLabel ? [c.dataLabel] : []).forEach(function (e) { var f = e.options; e.labelrank = x(f.labelrank, c.labelrank, c.shapeArgs && c.shapeArgs.height); f.allowOverlap ? (e.oldOpacity = e.opacity, e.newOpacity = 1, D(e, a)) : g.push(e) }) }) }, c(e.nodes || []), c(e.points)) }); this.hideOverlappingLabels(g)
                                }); a.prototype.hideOverlappingLabels = function (a) {
                                    var f =
                                        this, e = a.length, c = f.renderer, n, k, p, v = !1; var u = function (a) {
                                            var e, f = a.box ? 0 : a.padding || 0, b = e = 0, g; if (a && (!a.alignAttr || a.placed)) {
                                                var d = a.alignAttr || { x: a.attr("x"), y: a.attr("y") }; var k = a.parentGroup; a.width || (e = a.getBBox(), a.width = e.width, a.height = e.height, e = c.fontMetrics(null, a.element).h); var n = a.width - 2 * f; (g = { left: "0", center: "0.5", right: "1" }[a.alignValue]) ? b = +g * n : J(a.x) && Math.round(a.x) !== a.translateX && (b = a.x - a.translateX); return {
                                                    x: d.x + (k.translateX || 0) + f - (b || 0), y: d.y + (k.translateY || 0) + f - e, width: a.width -
                                                        2 * f, height: a.height - 2 * f
                                                }
                                            }
                                        }; for (k = 0; k < e; k++)if (n = a[k]) n.oldOpacity = n.opacity, n.newOpacity = 1, n.absoluteBox = u(n); a.sort(function (a, c) { return (c.labelrank || 0) - (a.labelrank || 0) }); for (k = 0; k < e; k++) { var w = (u = a[k]) && u.absoluteBox; for (n = k + 1; n < e; ++n) { var x = (p = a[n]) && p.absoluteBox; !w || !x || u === p || 0 === u.newOpacity || 0 === p.newOpacity || "hidden" === u.visibility || "hidden" === p.visibility || x.x >= w.x + w.width || x.x + x.width <= w.x || x.y >= w.y + w.height || x.y + x.height <= w.y || ((u.labelrank < p.labelrank ? u : p).newOpacity = 0) } } a.forEach(function (a) {
                                            D(a,
                                                f) && (v = !0)
                                        }); v && y(f, "afterHideAllOverlappingLabels")
                                }
                            }); L(f, "Core/Responsive.js", [f["Core/Utilities.js"]], function (a) {
                                var f = a.extend, B = a.find, F = a.isArray, y = a.isObject, I = a.merge, J = a.objectEach, C = a.pick, x = a.splat, p = a.uniqueKey, g; (function (a) {
                                    var c = []; a.compose = function (a) { -1 === c.indexOf(a) && (c.push(a), f(a.prototype, e.prototype)); return a }; var e = function () {
                                        function a() { } a.prototype.currentOptions = function (a) {
                                            function c(a, f, g, h) {
                                                var b; J(a, function (a, d) {
                                                    if (!h && -1 < e.collectionsWithUpdate.indexOf(d) && f[d]) for (a =
                                                        x(a), g[d] = [], b = 0; b < Math.max(a.length, f[d].length); b++)f[d][b] && (void 0 === a[b] ? g[d][b] = f[d][b] : (g[d][b] = {}, c(a[b], f[d][b], g[d][b], h + 1))); else y(a) ? (g[d] = F(a) ? [] : {}, c(a, f[d] || {}, g[d], h + 1)) : g[d] = "undefined" === typeof f[d] ? null : f[d]
                                                })
                                            } var e = this, f = {}; c(a, this.options, f, 0); return f
                                        }; a.prototype.matchResponsiveRule = function (a, c) {
                                            var e = a.condition; (e.callback || function () {
                                                return this.chartWidth <= C(e.maxWidth, Number.MAX_VALUE) && this.chartHeight <= C(e.maxHeight, Number.MAX_VALUE) && this.chartWidth >= C(e.minWidth,
                                                    0) && this.chartHeight >= C(e.minHeight, 0)
                                            }).call(this) && c.push(a._id)
                                        }; a.prototype.setResponsive = function (a, c) {
                                            var e = this, f = this.options.responsive, g = this.currentResponsive, k = []; !c && f && f.rules && f.rules.forEach(function (a) { "undefined" === typeof a._id && (a._id = p()); e.matchResponsiveRule(a, k) }, this); c = I.apply(void 0, k.map(function (a) { return B((f || {}).rules || [], function (c) { return c._id === a }) }).map(function (a) { return a && a.chartOptions })); c.isResponsiveOptions = !0; k = k.toString() || void 0; k !== (g && g.ruleIds) && (g &&
                                                this.update(g.undoOptions, a, !0), k ? (g = this.currentOptions(c), g.isResponsiveOptions = !0, this.currentResponsive = { ruleIds: k, mergedOptions: c, undoOptions: g }, this.update(c, a, !0)) : this.currentResponsive = void 0)
                                        }; return a
                                    }()
                                })(g || (g = {})); ""; ""; return g
                            }); L(f, "masters/highcharts.src.js", [f["Core/Globals.js"], f["Core/Utilities.js"], f["Core/DefaultOptions.js"], f["Core/Animation/Fx.js"], f["Core/Animation/AnimationUtilities.js"], f["Core/Renderer/HTML/AST.js"], f["Core/FormatUtilities.js"], f["Core/Renderer/RendererUtilities.js"],
                            f["Core/Renderer/SVG/SVGElement.js"], f["Core/Renderer/SVG/SVGRenderer.js"], f["Core/Renderer/HTML/HTMLElement.js"], f["Core/Renderer/HTML/HTMLRenderer.js"], f["Core/Axis/Axis.js"], f["Core/Axis/DateTimeAxis.js"], f["Core/Axis/LogarithmicAxis.js"], f["Core/Axis/PlotLineOrBand/PlotLineOrBand.js"], f["Core/Axis/Tick.js"], f["Core/Tooltip.js"], f["Core/Series/Point.js"], f["Core/Pointer.js"], f["Core/MSPointer.js"], f["Core/Legend/Legend.js"], f["Core/Chart/Chart.js"], f["Core/Series/Series.js"], f["Core/Series/SeriesRegistry.js"],
                            f["Series/Column/ColumnSeries.js"], f["Series/Column/ColumnDataLabel.js"], f["Series/Pie/PieSeries.js"], f["Series/Pie/PieDataLabel.js"], f["Core/Series/DataLabel.js"], f["Core/Responsive.js"], f["Core/Color/Color.js"], f["Core/Time.js"]], function (a, f, B, F, y, I, J, C, x, p, g, e, c, n, k, t, v, u, w, A, r, m, h, b, l, d, G, z, q, L, P, S, N) {
                                a.animate = y.animate; a.animObject = y.animObject; a.getDeferredAnimation = y.getDeferredAnimation; a.setAnimation = y.setAnimation; a.stop = y.stop; a.timers = F.timers; a.AST = I; a.Axis = c; a.Chart = h; a.chart = h.chart;
                                a.Fx = F; a.Legend = m; a.PlotLineOrBand = t; a.Point = w; a.Pointer = r.isRequired() ? r : A; a.Series = b; a.SVGElement = x; a.SVGRenderer = p; a.Tick = v; a.Time = N; a.Tooltip = u; a.Color = S; a.color = S.parse; e.compose(p); g.compose(x); a.defaultOptions = B.defaultOptions; a.getOptions = B.getOptions; a.time = B.defaultTime; a.setOptions = B.setOptions; a.dateFormat = J.dateFormat; a.format = J.format; a.numberFormat = J.numberFormat; a.addEvent = f.addEvent; a.arrayMax = f.arrayMax; a.arrayMin = f.arrayMin; a.attr = f.attr; a.clearTimeout = f.clearTimeout; a.correctFloat =
                                    f.correctFloat; a.createElement = f.createElement; a.css = f.css; a.defined = f.defined; a.destroyObjectProperties = f.destroyObjectProperties; a.discardElement = f.discardElement; a.distribute = C.distribute; a.erase = f.erase; a.error = f.error; a.extend = f.extend; a.extendClass = f.extendClass; a.find = f.find; a.fireEvent = f.fireEvent; a.getMagnitude = f.getMagnitude; a.getStyle = f.getStyle; a.inArray = f.inArray; a.isArray = f.isArray; a.isClass = f.isClass; a.isDOMElement = f.isDOMElement; a.isFunction = f.isFunction; a.isNumber = f.isNumber; a.isObject =
                                        f.isObject; a.isString = f.isString; a.keys = f.keys; a.merge = f.merge; a.normalizeTickInterval = f.normalizeTickInterval; a.objectEach = f.objectEach; a.offset = f.offset; a.pad = f.pad; a.pick = f.pick; a.pInt = f.pInt; a.relativeLength = f.relativeLength; a.removeEvent = f.removeEvent; a.seriesType = l.seriesType; a.splat = f.splat; a.stableSort = f.stableSort; a.syncTimeout = f.syncTimeout; a.timeUnits = f.timeUnits; a.uniqueKey = f.uniqueKey; a.useSerialIds = f.useSerialIds; a.wrap = f.wrap; G.compose(d); L.compose(b); n.compose(c); k.compose(c); q.compose(z);
                                t.compose(c); P.compose(h); return a
                            }); f["masters/highcharts.src.js"]._modules = f; return f["masters/highcharts.src.js"]
});
//# sourceMappingURL=highcharts.js.map