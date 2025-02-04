﻿/*
 Highcharts JS v10.1.0 (2022-05-20)

 Exporting module

 (c) 2010-2021 Torstein Honsi

 License: www.highcharts.com/license
*/
(function (a) { "object" === typeof module && module.exports ? (a["default"] = a, module.exports = a) : "function" === typeof define && define.amd ? define("highcharts/modules/exporting", ["highcharts"], function (h) { a(h); a.Highcharts = h; return a }) : a("undefined" !== typeof Highcharts ? Highcharts : void 0) })(function (a) {
    function h(a, b, t, m) { a.hasOwnProperty(b) || (a[b] = m.apply(null, t), "function" === typeof CustomEvent && window.dispatchEvent(new CustomEvent("HighchartsModuleLoaded", { detail: { path: b, module: a[b] } }))) } a = a ? a._modules : {}; h(a,
        "Extensions/FullScreen.js", [a["Core/Chart/Chart.js"], a["Core/Globals.js"], a["Core/Renderer/HTML/AST.js"], a["Core/Utilities.js"]], function (a, b, t, m) {
            var l = m.addEvent, g = m.fireEvent; m = function () {
                function a(c) {
                    this.chart = c; this.isOpen = !1; c = c.renderTo; this.browserProps || ("function" === typeof c.requestFullscreen ? this.browserProps = { fullscreenChange: "fullscreenchange", requestFullscreen: "requestFullscreen", exitFullscreen: "exitFullscreen" } : c.mozRequestFullScreen ? this.browserProps = {
                        fullscreenChange: "mozfullscreenchange",
                        requestFullscreen: "mozRequestFullScreen", exitFullscreen: "mozCancelFullScreen"
                    } : c.webkitRequestFullScreen ? this.browserProps = { fullscreenChange: "webkitfullscreenchange", requestFullscreen: "webkitRequestFullScreen", exitFullscreen: "webkitExitFullscreen" } : c.msRequestFullscreen && (this.browserProps = { fullscreenChange: "MSFullscreenChange", requestFullscreen: "msRequestFullscreen", exitFullscreen: "msExitFullscreen" }))
                } a.prototype.close = function () {
                    var c = this, e = c.chart, a = e.options.chart; g(e, "fullscreenClose", null,
                        function () { if (c.isOpen && c.browserProps && e.container.ownerDocument instanceof Document) e.container.ownerDocument[c.browserProps.exitFullscreen](); c.unbindFullscreenEvent && (c.unbindFullscreenEvent = c.unbindFullscreenEvent()); e.setSize(c.origWidth, c.origHeight, !1); c.origWidth = void 0; c.origHeight = void 0; a.width = c.origWidthOption; a.height = c.origHeightOption; c.origWidthOption = void 0; c.origHeightOption = void 0; c.isOpen = !1; c.setButtonText() })
                }; a.prototype.open = function () {
                    var c = this, e = c.chart, a = e.options.chart;
                    g(e, "fullscreenOpen", null, function () { a && (c.origWidthOption = a.width, c.origHeightOption = a.height); c.origWidth = e.chartWidth; c.origHeight = e.chartHeight; if (c.browserProps) { var g = l(e.container.ownerDocument, c.browserProps.fullscreenChange, function () { c.isOpen ? (c.isOpen = !1, c.close()) : (e.setSize(null, null, !1), c.isOpen = !0, c.setButtonText()) }), H = l(e, "destroy", g); c.unbindFullscreenEvent = function () { g(); H() }; var b = e.renderTo[c.browserProps.requestFullscreen](); if (b) b["catch"](function () { alert("Full screen is not supported inside a frame.") }) } })
                };
                a.prototype.setButtonText = function () { var c = this.chart, a = c.exportDivElements, g = c.options.exporting, b = g && g.buttons && g.buttons.contextButton.menuItems; c = c.options.lang; g && g.menuItemDefinitions && c && c.exitFullscreen && c.viewFullscreen && b && a && (a = a[b.indexOf("viewFullscreen")]) && t.setElementHTML(a, this.isOpen ? c.exitFullscreen : g.menuItemDefinitions.viewFullscreen.text || c.viewFullscreen) }; a.prototype.toggle = function () { this.isOpen ? this.close() : this.open() }; return a
            }(); b.Fullscreen = m; l(a, "beforeRender", function () {
                this.fullscreen =
                new b.Fullscreen(this)
            }); ""; return b.Fullscreen
        }); h(a, "Core/Chart/ChartNavigationComposition.js", [], function () { var a; (function (a) { a.compose = function (a) { a.navigation || (a.navigation = new b(a)); return a }; var b = function () { function a(a) { this.updates = []; this.chart = a } a.prototype.addUpdate = function (a) { this.chart.navigation.updates.push(a) }; a.prototype.update = function (a, g) { var b = this; this.updates.forEach(function (c) { c.call(b.chart, a, g) }) }; return a }(); a.Additions = b })(a || (a = {})); return a }); h(a, "Extensions/Exporting/ExportingDefaults.js",
            [a["Core/Globals.js"]], function (a) {
                a = a.isTouchDevice; return {
                    exporting: {
                        type: "image/png", url: "https://export.highcharts.com/", pdfFont: { normal: void 0, bold: void 0, bolditalic: void 0, italic: void 0 }, printMaxWidth: 780, scale: 2, buttons: { contextButton: { className: "highcharts-contextbutton", menuClassName: "highcharts-contextmenu", symbol: "menu", titleKey: "contextButtonTitle", menuItems: "viewFullscreen printChart separator downloadPNG downloadJPEG downloadPDF downloadSVG".split(" ") } }, menuItemDefinitions: {
                            viewFullscreen: {
                                textKey: "viewFullscreen",
                                onclick: function () { this.fullscreen.toggle() }
                            }, printChart: { textKey: "printChart", onclick: function () { this.print() } }, separator: { separator: !0 }, downloadPNG: { textKey: "downloadPNG", onclick: function () { this.exportChart() } }, downloadJPEG: { textKey: "downloadJPEG", onclick: function () { this.exportChart({ type: "image/jpeg" }) } }, downloadPDF: { textKey: "downloadPDF", onclick: function () { this.exportChart({ type: "application/pdf" }) } }, downloadSVG: { textKey: "downloadSVG", onclick: function () { this.exportChart({ type: "image/svg+xml" }) } }
                        }
                    },
                    lang: { viewFullscreen: "View in full screen", exitFullscreen: "Exit from full screen", printChart: "Print chart", downloadPNG: "Download PNG image", downloadJPEG: "Download JPEG image", downloadPDF: "Download PDF document", downloadSVG: "Download SVG vector image", contextButtonTitle: "Chart context menu" }, navigation: {
                        buttonOptions: { symbolSize: 14, symbolX: 12.5, symbolY: 10.5, align: "right", buttonSpacing: 3, height: 22, verticalAlign: "top", width: 24, symbolFill: "#666666", symbolStroke: "#666666", symbolStrokeWidth: 3, theme: { padding: 5 } },
                        menuStyle: { border: "1px solid ".concat("#999999"), background: "#ffffff", padding: "5px 0" }, menuItemStyle: { padding: "0.5em 1em", color: "#333333", background: "none", fontSize: a ? "14px" : "11px", transition: "background 250ms, color 250ms" }, menuItemHoverStyle: { background: "#335cad", color: "#ffffff" }
                    }
                }
            }); h(a, "Extensions/Exporting/ExportingSymbols.js", [], function () {
                var a; (function (a) {
                    function b(a, b, c, e) { return [["M", a, b + 2.5], ["L", a + c, b + 2.5], ["M", a, b + e / 2 + .5], ["L", a + c, b + e / 2 + .5], ["M", a, b + e - 1.5], ["L", a + c, b + e - 1.5]] } function l(a,
                        b, c, e) { a = e / 3 - 2; e = []; return e = e.concat(this.circle(c - a, b, a, a), this.circle(c - a, b + a + 4, a, a), this.circle(c - a, b + 2 * (a + 4), a, a)) } var h = []; a.compose = function (a) { -1 === h.indexOf(a) && (h.push(a), a = a.prototype.symbols, a.menu = b, a.menuball = l.bind(a)) }
                })(a || (a = {})); return a
            }); h(a, "Core/HttpUtilities.js", [a["Core/Globals.js"], a["Core/Utilities.js"]], function (a, b) {
                var l = a.doc, h = b.createElement, z = b.discardElement, g = b.merge, p = b.objectEach, c = {
                    ajax: function (a) {
                        var c = g(!0, {
                            url: !1, type: "get", dataType: "json", success: !1, error: !1,
                            data: !1, headers: {}
                        }, a); a = { json: "application/json", xml: "application/xml", text: "text/plain", octet: "application/octet-stream" }; var b = new XMLHttpRequest; if (!c.url) return !1; b.open(c.type.toUpperCase(), c.url, !0); c.headers["Content-Type"] || b.setRequestHeader("Content-Type", a[c.dataType] || a.text); p(c.headers, function (a, c) { b.setRequestHeader(c, a) }); c.responseType && (b.responseType = c.responseType); b.onreadystatechange = function () {
                            if (4 === b.readyState) {
                                if (200 === b.status) {
                                    if ("blob" !== c.responseType) {
                                        var a = b.responseText;
                                        if ("json" === c.dataType) try { a = JSON.parse(a) } catch (x) { if (x instanceof Error) { c.error && c.error(b, x); return } }
                                    } return c.success && c.success(a, b)
                                } c.error && c.error(b, b.responseText)
                            }
                        }; try { c.data = JSON.stringify(c.data) } catch (A) { } b.send(c.data || !0)
                    }, getJSON: function (a, b) { c.ajax({ url: a, success: b, dataType: "json", headers: { "Content-Type": "text/plain" } }) }, post: function (a, c, b) {
                        var e = h("form", g({ method: "post", action: a, enctype: "multipart/form-data" }, b), { display: "none" }, l.body); p(c, function (a, c) {
                            h("input", {
                                type: "hidden",
                                name: c, value: a
                            }, null, e)
                        }); e.submit(); z(e)
                    }
                }; ""; return c
            }); h(a, "Extensions/Exporting/Exporting.js", [a["Core/Renderer/HTML/AST.js"], a["Core/Chart/Chart.js"], a["Core/Chart/ChartNavigationComposition.js"], a["Core/DefaultOptions.js"], a["Extensions/Exporting/ExportingDefaults.js"], a["Extensions/Exporting/ExportingSymbols.js"], a["Core/Globals.js"], a["Core/HttpUtilities.js"], a["Core/Utilities.js"]], function (a, b, h, m, z, g, p, c, e) {
                b = m.defaultOptions; var l = p.doc, t = p.SVG_NS, A = p.win, x = e.addEvent, v = e.css, B = e.createElement,
                    K = e.discardElement, C = e.extend, O = e.find, D = e.fireEvent, P = e.isObject, n = e.merge, Q = e.objectEach, y = e.pick, R = e.removeEvent, S = e.uniqueKey, E; (function (b) {
                        function m(a) {
                            var d = this, c = d.renderer, b = n(d.options.navigation.buttonOptions, a), e = b.onclick, q = b.menuItems, r = b.symbolSize || 12; d.btnCount || (d.btnCount = 0); d.exportDivElements || (d.exportDivElements = [], d.exportSVGElements = []); if (!1 !== b.enabled && b.theme) {
                                var f = b.theme, J; d.styledMode || (f.fill = y(f.fill, "#ffffff"), f.stroke = y(f.stroke, "none")); e ? J = function (a) {
                                    a && a.stopPropagation();
                                    e.call(d, a)
                                } : q && (J = function (a) { a && a.stopPropagation(); d.contextMenu(u.menuClassName, q, u.translateX, u.translateY, u.width, u.height, u); u.setState(2) }); b.text && b.symbol ? f.paddingLeft = y(f.paddingLeft, 30) : b.text || C(f, { width: b.width, height: b.height, padding: 0 }); d.styledMode || (f["stroke-linecap"] = "round", f.fill = y(f.fill, "#ffffff"), f.stroke = y(f.stroke, "none")); var u = c.button(b.text, 0, 0, J, f).addClass(a.className).attr({ title: y(d.options.lang[b._titleKey || b.titleKey], "") }); u.menuClassName = a.menuClassName || "highcharts-menu-" +
                                    d.btnCount++; if (b.symbol) { var l = c.symbol(b.symbol, b.symbolX - r / 2, b.symbolY - r / 2, r, r, { width: r, height: r }).addClass("highcharts-button-symbol").attr({ zIndex: 1 }).add(u); d.styledMode || l.attr({ stroke: b.symbolStroke, fill: b.symbolFill, "stroke-width": b.symbolStrokeWidth || 1 }) } u.add(d.exportingGroup).align(C(b, { width: u.width, x: y(b.x, d.buttonOffset) }), !0, "spacingBox"); d.buttonOffset += (u.width + b.buttonSpacing) * ("right" === b.align ? -1 : 1); d.exportSVGElements.push(u, l)
                            }
                        } function z() {
                            if (this.printReverseInfo) {
                                var a = this.printReverseInfo,
                                b = a.childNodes, c = a.origDisplay; a = a.resetParams; this.moveContainers(this.renderTo);[].forEach.call(b, function (a, d) { 1 === a.nodeType && (a.style.display = c[d] || "") }); this.isPrinting = !1; a && this.setSize.apply(this, a); delete this.printReverseInfo; F = void 0; D(this, "afterPrint")
                            }
                        } function E() {
                            var a = l.body, b = this.options.exporting.printMaxWidth, c = { childNodes: a.childNodes, origDisplay: [], resetParams: void 0 }; this.isPrinting = !0; this.pointer.reset(null, 0); D(this, "beforePrint"); b && this.chartWidth > b && (c.resetParams = [this.options.chart.width,
                            void 0, !1], this.setSize(b, void 0, !1));[].forEach.call(c.childNodes, function (a, d) { 1 === a.nodeType && (c.origDisplay[d] = a.style.display, a.style.display = "none") }); this.moveContainers(a); this.printReverseInfo = c
                        } function H(a) { a.renderExporting(); x(a, "redraw", a.renderExporting); x(a, "destroy", a.destroyExport) } function T(d, b, c, I, g, q, r) {
                            var f = this, w = f.options.navigation, G = f.chartWidth, h = f.chartHeight, L = "cache-" + d, n = Math.max(g, q), k = f[L]; if (!k) {
                                f.exportContextMenu = f[L] = k = B("div", { className: d }, {
                                    position: "absolute",
                                    zIndex: 1E3, padding: n + "px", pointerEvents: "auto"
                                }, f.fixedDiv || f.container); var p = B("ul", { className: "highcharts-menu" }, { listStyle: "none", margin: 0, padding: 0 }, k); f.styledMode || v(p, C({ MozBoxShadow: "3px 3px 10px #888", WebkitBoxShadow: "3px 3px 10px #888", boxShadow: "3px 3px 10px #888" }, w.menuStyle)); k.hideMenu = function () { v(k, { display: "none" }); r && r.setState(0); f.openMenu = !1; v(f.renderTo, { overflow: "hidden" }); v(f.container, { overflow: "hidden" }); e.clearTimeout(k.hideTimer); D(f, "exportMenuHidden") }; f.exportEvents.push(x(k,
                                    "mouseleave", function () { k.hideTimer = A.setTimeout(k.hideMenu, 500) }), x(k, "mouseenter", function () { e.clearTimeout(k.hideTimer) }), x(l, "mouseup", function (a) { f.pointer.inClass(a.target, d) || k.hideMenu() }), x(k, "click", function () { f.openMenu && k.hideMenu() })); b.forEach(function (d) {
                                        "string" === typeof d && (d = f.options.exporting.menuItemDefinitions[d]); if (P(d, !0)) {
                                            var b = void 0; d.separator ? b = B("hr", void 0, void 0, p) : ("viewData" === d.textKey && f.isDataTableVisible && (d.textKey = "hideData"), b = B("li", {
                                                className: "highcharts-menu-item",
                                                onclick: function (a) { a && a.stopPropagation(); k.hideMenu(); d.onclick && d.onclick.apply(f, arguments) }
                                            }, void 0, p), a.setElementHTML(b, d.text || f.options.lang[d.textKey]), f.styledMode || (b.onmouseover = function () { v(this, w.menuItemHoverStyle) }, b.onmouseout = function () { v(this, w.menuItemStyle) }, v(b, C({ cursor: "pointer" }, w.menuItemStyle || {})))); f.exportDivElements.push(b)
                                        }
                                    }); f.exportDivElements.push(p, k); f.exportMenuWidth = k.offsetWidth; f.exportMenuHeight = k.offsetHeight
                            } b = { display: "block" }; c + f.exportMenuWidth > G ? b.right =
                                G - c - g - n + "px" : b.left = c - n + "px"; I + q + f.exportMenuHeight > h && "top" !== r.alignOptions.verticalAlign ? b.bottom = h - I - n + "px" : b.top = I + q - n + "px"; v(k, b); v(f.renderTo, { overflow: "" }); v(f.container, { overflow: "" }); f.openMenu = !0; D(f, "exportMenuShown")
                        } function U(a) {
                            var d = a ? a.target : this, b = d.exportSVGElements, c = d.exportDivElements; a = d.exportEvents; var l; b && (b.forEach(function (a, c) { a && (a.onclick = a.ontouchstart = null, l = "cache-" + a.menuClassName, d[l] && delete d[l], b[c] = a.destroy()) }), b.length = 0); d.exportingGroup && (d.exportingGroup.destroy(),
                                delete d.exportingGroup); c && (c.forEach(function (a, d) { a && (e.clearTimeout(a.hideTimer), R(a, "mouseleave"), c[d] = a.onmouseout = a.onmouseover = a.ontouchstart = a.onclick = null, K(a)) }), c.length = 0); a && (a.forEach(function (a) { a() }), a.length = 0)
                        } function V(a, b) { b = this.getSVGForExport(a, b); a = n(this.options.exporting, a); c.post(a.url, { filename: a.filename ? a.filename.replace(/\//g, "-") : this.getFilename(), type: a.type, width: a.width || 0, scale: a.scale, svg: b }, a.formAttributes) } function W() {
                            this.styledMode && this.inlineStyles();
                            return this.container.innerHTML
                        } function X() { var a = this.userOptions.title && this.userOptions.title.text, b = this.options.exporting.filename; if (b) return b.replace(/\//g, "-"); "string" === typeof a && (b = a.toLowerCase().replace(/<\/?[^>]+(>|$)/g, "").replace(/[\s_]+/g, "-").replace(/[^a-z0-9\-]/g, "").replace(/^[\-]+/g, "").replace(/[\-]+/g, "-").substr(0, 24).replace(/[\-]+$/g, "")); if (!b || 5 > b.length) b = "chart"; return b } function Y(a) {
                            var b, c = n(this.options, a); c.plotOptions = n(this.userOptions.plotOptions, a && a.plotOptions);
                            c.time = n(this.userOptions.time, a && a.time); var d = B("div", null, { position: "absolute", top: "-9999em", width: this.chartWidth + "px", height: this.chartHeight + "px" }, l.body), e = this.renderTo.style.width; var g = this.renderTo.style.height; e = c.exporting.sourceWidth || c.chart.width || /px$/.test(e) && parseInt(e, 10) || (c.isGantt ? 800 : 600); g = c.exporting.sourceHeight || c.chart.height || /px$/.test(g) && parseInt(g, 10) || 400; C(c.chart, { animation: !1, renderTo: d, forExport: !0, renderer: "SVGRenderer", width: e, height: g }); c.exporting.enabled =
                                !1; delete c.data; c.series = []; this.series.forEach(function (a) { b = n(a.userOptions, { animation: !1, enableMouseTracking: !1, showCheckbox: !1, visible: a.visible }); b.isInternal || c.series.push(b) }); var h = {}; this.axes.forEach(function (a) { a.userOptions.internalKey || (a.userOptions.internalKey = S()); a.options.isInternal || (h[a.coll] || (h[a.coll] = !0, c[a.coll] = []), c[a.coll].push(n(a.userOptions, { visible: a.visible }))) }); var f = new this.constructor(c, this.callback); a && ["xAxis", "yAxis", "series"].forEach(function (b) {
                                    var c =
                                        {}; a[b] && (c[b] = a[b], f.update(c))
                                }); this.axes.forEach(function (a) { var b = O(f.axes, function (b) { return b.options.internalKey === a.userOptions.internalKey }), c = a.getExtremes(), d = c.userMin; c = c.userMax; b && ("undefined" !== typeof d && d !== b.min || "undefined" !== typeof c && c !== b.max) && b.setExtremes(d, c, !0, !1) }); g = f.getChartHTML(); D(this, "getSVG", { chartCopy: f }); g = this.sanitizeSVG(g, c); c = null; f.destroy(); K(d); return g
                        } function Z(a, b) {
                            var c = this.options.exporting; return this.getSVG(n({ chart: { borderRadius: 0 } }, c.chartOptions,
                                b, { exporting: { sourceWidth: a && a.sourceWidth || c.sourceWidth, sourceHeight: a && a.sourceHeight || c.sourceHeight } }))
                        } function aa(a) { return a.replace(/([A-Z])/g, function (a, b) { return "-" + b.toLowerCase() }) } function ba() {
                            function a(b) {
                                var d = {}, f, w; if (r && 1 === b.nodeType && -1 === ca.indexOf(b.nodeName)) {
                                    var l = A.getComputedStyle(b, null); var G = "svg" === b.nodeName ? {} : A.getComputedStyle(b.parentNode, null); if (!g[b.nodeName]) {
                                        h = r.getElementsByTagName("svg")[0]; var k = r.createElementNS(b.namespaceURI, b.nodeName); h.appendChild(k);
                                        g[b.nodeName] = n(A.getComputedStyle(k, null)); "text" === b.nodeName && delete g.text.fill; h.removeChild(k)
                                    } for (var q in l) if (p.isFirefox || p.isMS || p.isSafari || Object.hasOwnProperty.call(l, q)) { var t = l[q], m = q; k = f = !1; if (e.length) { for (w = e.length; w-- && !f;)f = e[w].test(m); k = !f } "transform" === m && "none" === t && (k = !0); for (w = c.length; w-- && !k;)k = c[w].test(m) || "function" === typeof t; k || G[m] === t && "svg" !== b.nodeName || g[b.nodeName][m] === t || (M && -1 === M.indexOf(m) ? "parentRule" !== m && (d[m] = t) : t && b.setAttribute(aa(m), t)) } v(b, d);
                                    "svg" === b.nodeName && b.setAttribute("stroke-width", "1px"); "text" !== b.nodeName && [].forEach.call(b.children || b.childNodes, a)
                                }
                            } var c = da, e = b.inlineWhitelist, g = {}, h, q = l.createElement("iframe"); v(q, { width: "1px", height: "1px", visibility: "hidden" }); l.body.appendChild(q); var r = q.contentWindow && q.contentWindow.document; r && r.body.appendChild(r.createElementNS(t, "svg")); a(this.container.querySelector("svg")); h.parentNode.removeChild(h); q.parentNode.removeChild(q)
                        } function ea(a) {
                            (this.fixedDiv ? [this.fixedDiv, this.scrollingContainer] :
                                [this.container]).forEach(function (b) { a.appendChild(b) })
                        } function fa() { var a = this; a.exporting = { update: function (b, c) { a.isDirtyExporting = !0; n(!0, a.options.exporting, b); y(c, !0) && a.redraw() } }; h.compose(a).navigation.addUpdate(function (b, c) { a.isDirtyExporting = !0; n(!0, a.options.navigation, b); y(c, !0) && a.redraw() }) } function ha() { var a = this; a.isPrinting || (F = a, p.isSafari || a.beforePrint(), setTimeout(function () { A.focus(); A.print(); p.isSafari || setTimeout(function () { a.afterPrint() }, 1E3) }, 1)) } function ia() {
                            var a =
                                this, b = a.options.exporting, c = b.buttons, e = a.isDirtyExporting || !a.exportSVGElements; a.buttonOffset = 0; a.isDirtyExporting && a.destroyExport(); e && !1 !== b.enabled && (a.exportEvents = [], a.exportingGroup = a.exportingGroup || a.renderer.g("exporting-group").attr({ zIndex: 3 }).add(), Q(c, function (b) { a.addButton(b) }), a.isDirtyExporting = !1)
                        } function ja(a, b) {
                            var c = a.indexOf("</svg>") + 6, d = a.substr(c); a = a.substr(0, c); b && b.exporting && b.exporting.allowHTML && d && (d = '<foreignObject x="0" y="0" width="' + b.chart.width + '" height="' +
                                b.chart.height + '"><body xmlns="http://www.w3.org/1999/xhtml">' + d.replace(/(<(?:img|br).*?(?=>))>/g, "$1 />") + "</body></foreignObject>", a = a.replace("</svg>", d + "</svg>")); a = a.replace(/zIndex="[^"]+"/g, "").replace(/symbolName="[^"]+"/g, "").replace(/jQuery[0-9]+="[^"]+"/g, "").replace(/url\(("|&quot;)(.*?)("|&quot;);?\)/g, "url($2)").replace(/url\([^#]+#/g, "url(#").replace(/<svg /, '<svg xmlns:xlink="http://www.w3.org/1999/xlink" ').replace(/ (|NS[0-9]+:)href=/g, " xlink:href=").replace(/\n/, " ").replace(/(fill|stroke)="rgba\(([ 0-9]+,[ 0-9]+,[ 0-9]+),([ 0-9\.]+)\)"/g,
                                    '$1="rgb($2)" $1-opacity="$3"').replace(/&nbsp;/g, "\u00a0").replace(/&shy;/g, "\u00ad"); this.ieSanitizeSVG && (a = this.ieSanitizeSVG(a)); return a
                        } var N = [], da = [/-/, /^(clipPath|cssText|d|height|width)$/, /^font$/, /[lL]ogical(Width|Height)$/, /perspective/, /TapHighlightColor/, /^transition/, /^length$/], M = "fill stroke strokeLinecap strokeLinejoin strokeWidth textAnchor x y".split(" "); b.inlineWhitelist = []; var ca = ["clipPath", "defs", "desc"], F; b.compose = function (a, b) {
                            g.compose(b); -1 === N.indexOf(a) && (N.push(a),
                                b = a.prototype, b.afterPrint = z, b.exportChart = V, b.inlineStyles = ba, b.print = ha, b.sanitizeSVG = ja, b.getChartHTML = W, b.getSVG = Y, b.getSVGForExport = Z, b.getFilename = X, b.moveContainers = ea, b.beforePrint = E, b.contextMenu = T, b.addButton = m, b.destroyExport = U, b.renderExporting = ia, b.callbacks.push(H), x(a, "init", fa), p.isSafari && p.win.matchMedia("print").addListener(function (a) { F && (a.matches ? F.beforePrint() : F.afterPrint()) }))
                        }
                    })(E || (E = {})); b.exporting = n(z.exporting, b.exporting); b.lang = n(z.lang, b.lang); b.navigation = n(z.navigation,
                        b.navigation); ""; ""; return E
            }); h(a, "masters/modules/exporting.src.js", [a["Core/Globals.js"], a["Extensions/Exporting/Exporting.js"], a["Core/HttpUtilities.js"]], function (a, b, h) { a.HttpUtilities = h; a.ajax = h.ajax; a.getJSON = h.getJSON; a.post = h.post; b.compose(a.Chart, a.Renderer) })
});
//# sourceMappingURL=exporting.js.map