var inputChecker = (function(){
	function GetSheepsAndRams(number, inputNumber){
		var numberStr = number.toString(),
			inputNumberStr = inputNumber.toString(),
			result = {
				sheeps: 0,
				rams: 0
			};

		checkForRams(numberStr, inputNumberStr, result);
		
		return result;
	}

	function checkForRams(numberStr, inputNumberStr, result){
		var i,
			len,
			newNum = '',
			newInputNum = '';

		// Checking for rams
		for (i = 0, len = numberStr.length; i < len; i+=1) {
			if (numberStr[i] === inputNumberStr[i]) {
				result.rams++;
				newNum += '-';
				newInputNum += '+';
			}
			else {
				newNum += numberStr[i];
				newInputNum += inputNumberStr[i];
			}
		};

		// reseting number string for check for sheeps
		numberStr = newNum;

		checkForSheeps(newNum, newInputNum, result);
	}

	function checkForSheeps(numberStr, inputNumberStr, result){
		var i,
			j,
			len,
			inputArr = inputNumberStr.split('');

		// checking for sheeps
		for (i = 0, len = numberStr.length; i < len; i+=1) {
			for (j = 0; j < len; j+=1) {
				if (numberStr[i] === inputArr[j]) {
					result.sheeps++;
					inputArr[j] = '*'
				};
			};
		};
	}

	return {
		getSheepsAndRams: GetSheepsAndRams
	}
}());