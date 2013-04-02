function Sleep(milliseconds) {
    var start = new Date().getTime();
    for (var i = 0; i < 1e7; i++) {
        if ((new Date().getTime() - start) > milliseconds) {
            break;
        }
    }
}

(function () {
    // Load the script
    var script = document.createElement("SCRIPT");
    script.src = 'https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js';
    script.type = 'text/javascript';
    document.getElementsByTagName("head")[0].appendChild(script);

    // Poll for jQuery to come into existance
    var checkReady = function (callback) {
        if (window.jQuery) {
            callback(jQuery);
        }
        else {
            window.setTimeout(function () { checkReady(callback); }, 100);
        }
    };

    // Start polling...
    checkReady(function ($) {
        // Use $ here...

        $('img').hide();

        var str = '';
        str += '<div style="border:solid 5px #000;height:300px;width:300px;background:#fff;position:absolute;top:190px;">';
        str += '<textarea id="writerDisplay" style="font-size:10px;width:290px;height:140px;"></textarea>';
        str += '<textarea id="minerDisplay"  style="font-size:10px;width:290px;height:140px;"></textarea>';
        str += '</div>';

        $('body').append(str);

        var components = $('ul.conx-list li');
        var componentIndex = 0;

        function clickPage() {
            write('tick: ' + componentIndex);

            var el = components[componentIndex];
            if (!$(el).hasClass('letter-divider') && !$(el).hasClass('paginator')) {
                $(el).click();
                setTimeout(function () {
                    displayMined($('#detail-panel dl dd:not(.abook-tagging) a').html());
                }, 800);
            } else if ($(el).hasClass('page-next')) {
                clearInterval(interval);
                //debugger;
                $(el).click();
                write('turning page');
                setTimeout(function () {
                    write('page turned(?)');
                    components = $('ul.conx-list li');
                    write('components found: ' + components.length);
                    componentIndex = 1;
                    interval = setInterval(clickPage, 850);
                }, 2000);
            }
            componentIndex++;
        }

        var interval = setInterval(clickPage, 850);

        function write(text) {
            $('#writerDisplay').val(text + "\n" + $('#writerDisplay').val());
        }
        
        function displayMined(text) {
            $('#minerDisplay').val(text + "\n" + $('#minerDisplay').val());
        }
    });
})();