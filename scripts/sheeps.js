(function(){
	function generateRandomFourDigitsNumber(){
		var result = Math.floor(Math.random() * 10000);
		if (result < 1000) {
			result = generateRandomFourDigitsNumber();
		};
		return result;
	}

	renderer.render();

	$('#start-btn').on('click', function(){
		var number = generateRandomFourDigitsNumber();
		engine.startGame(number);
		// engine.startGame(1234);
	});
}())