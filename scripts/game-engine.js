var engine = (function(){
	var currentScore = 0,
		currentNumber,
		currentSheepsAndRams = {
			rams: 0,
			sheeps: 0
		};

	function isGameWon(currentResult){
		currentScore++;
		if (currentResult.rams === 4) {
			EndGame();
		} else {
			$('#current-input-result').text(currentResult.sheeps + ' : sheeps,   ' + currentResult.rams + ' : rams');
		}
		return false;
	}

	function StartGame(number){
		currentNumber = number;
		console.log(number);
		hideStartBtn();
		addGameBtn();
		addEndGameBtn();
	}

	function EndGame(){
		var player = prompt("Enter your player name : ");
		var result = currentScore;
		if (currentSheepsAndRams.rams !== 4) {
			result = 1000000;
		};
		currentScore = 0;
		currentSheepsAndRams = {};
		localStorage.setItem(player, result);

		$('#current-input-result').text(' ');
		renderer.render();
		showStartBtn();
		hideGameBtn();
		hideEndGameBtn();
	}

	function hideStartBtn(){
		$('#start-btn').addClass('disabled');
	}

	function showStartBtn(){
		$('#start-btn').removeClass('disabled');
	}

	function hideEndGameBtn(){
		$('#end-btn').addClass('disabled');
	}

	function addEndGameBtn(){
		var endGameBtn = $('#end-btn');
		endGameBtn.removeClass('disabled');
		addEndGameBtnClick(endGameBtn);
	}

	function addEndGameBtnClick(btn){
		btn.on('click', function(){
			EndGame();
		})
	}

	function hideGameBtn(){
		$('#game-btn').addClass('disabled');
	}

	function addGameBtn(){
		var inputCheckerBtn = $('#game-btn');
		inputCheckerBtn.removeClass('disabled');
		addGameBtnClick(inputCheckerBtn);
	}

	function addGameBtnClick(btn){
		btn.on('click', function(){
			var userInput = $('#user-entry').val();
			if (userInput.length !== 4 || userInput === 'undefined' || userInput[0] === '0') {
				alert('Invalid input. Must be exactly 4 digits and not containing any chars or start with 0.')
			}
			else {
				currentSheepsAndRams = inputChecker.getSheepsAndRams(currentNumber, userInput)
				isGameWon(currentSheepsAndRams);
			}
		})
	}

	return {
		startGame: StartGame,
		endGame: EndGame
	}
}());