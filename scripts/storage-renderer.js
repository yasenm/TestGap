var renderer = (function(){

	function renderStorage(){
		var users = [],
			sortedUsers = [];
		
		users = getUsers();
		sortedUsers = sortUsers(users);
		renderScoreBoard(sortedUsers);
	}

	function getUsers(){
		var result = [];
		for(var user in localStorage){
			console.log(user + ' ---> ' + localStorage.getItem(user));
			result.push({
				user: user,
				score: parseInt(localStorage.getItem(user))
			})
		}
		return result;
	}

	function sortUsers(users){
		var result = _.chain(users)
			.sortBy('score')
			.value();
		return result;
	}

	function renderScoreBoard(users){
		var $list = $('#high-scores').html('');

		for (var user in users){
			var item = $('<div>').addClass('panel-body');
			item.text(users[user].user + ' : ' + users[user].score);
			console.log(item);
			$list.append(item);
		}
	}

	return {
		render: renderStorage
	}
}());