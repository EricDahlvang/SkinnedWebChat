var themes = {
    "default": "//bootswatch.com/cosmo/bootstrap.min.css",
    "theme2": "//bootswatch.com/readable/bootstrap.min.css",
    "theme3": "//bootswatch.com/cyborg/bootstrap.min.css"
}
var botthemes = {
    "default": "/Content/botchat.css",
    "theme2": "/Content/botchat-theme2.css",
    "theme3": "/Content/botchat-theme3.css"    
}
var themesstyles = {};
var botthemestyles = {};
$(function () {
    themesstyles = $('<link href="' + themes['default'] + '" rel="stylesheet" />');
    botthemestyles = $('<link href="' + botthemes['default'] + '" rel="stylesheet" />');
    themesstyles.appendTo('head');
    botthemestyles.appendTo('head');
    $('.theme-link').click(function () {
        changeTheme($(this).attr('data-theme'));
    });
});

function changeTheme(newTheme) {
    var themeurl = themes[newTheme];
    var botthemeurl = botthemes[newTheme];

    themesstyles.attr('href', themeurl);
    botthemestyles.attr('href', botthemeurl + '?d=' + new Date().getTime());
}