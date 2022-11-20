$(document).ready(function () {
    console.log("ready!");
    bindSearch();
    bindQuizForm();
});
var savedquestions = [];
function bindSearch() {
    //let $search = $('.search-btn');
   // let $query = $('.query');
    $('.search-btn').click(search);
}
function bindQuizForm() {
    var $form = $('.add');
    $form.click(function () {
        var $questions = $('.questions');
        var q = {};
        q.Question = $('.new-question').val();
        q.CorrectAnswer = $('.new-correct-answer').val();
        q.Wrong1Answer = $('.new-wrong-answer1').val();
        q.Wrong2Answer = $('.new-wrong-answer2').val();
        q.Wrong3Answer = $('.new-wrong-answer3').val();
        q.Difficulty = $('.new-difficulty').val();
        savedquestions.push(q);
        var $div = $('<div class="question">' + q.Question + '</div>');
        $div.appendTo($questions);
    });
}
function search() {

    let $query = $('.query');
    var url = "api/search/" + $query.val();
    $.get(url, function (data) {
       
        var $resultDiv = $('.results');
        $resultDiv.html('');
        $(data).each(function (index) {
            var text = $(this)[0].Text;
            var link = $(this)[0].Link;
            var $div = $('<div>' + text + '</div>');
            $div.on("click", openIframe(link));
            $div.appendTo($resultDiv);
        });
    });
}
function openIframe(link) {
    $("#frame").attr("src", link);
}