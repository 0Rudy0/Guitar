﻿var internetLinks = {};
var currentSongLink = "";
var previousSongId = -1;

function initSplitter() {
	$("#splitter").jqxSplitter({ width: $('.mainHolder').width(), height: 600, panels: [{ size: '40%' }] });
	// prepare the data
	var data = new Array();
	var firstNames = ["Nancy", "Andrew", "Janet", "Margaret", "Steven", "Michael", "Robert", "Laura", "Anne"];
	var lastNames = ["Davolio", "Fuller", "Leverling", "Peacock", "Buchanan", "Suyama", "King", "Callahan", "Dodsworth"];
	var titles = ["Sales Representative", "Vice President, Sales", "Sales Representative", "Sales Representative", "Sales Manager", "Sales Representative", "Sales Representative", "Inside Sales Coordinator", "Sales Representative"];
	var titleofcourtesy = ["Ms.", "Dr.", "Ms.", "Mrs.", "Mr.", "Mr.", "Mr.", "Ms.", "Ms."];
	var birthdate = ["08-Dec-48", "19-Feb-52", "30-Aug-63", "19-Sep-37", "04-Mar-55", "02-Jul-63", "29-May-60", "09-Jan-58", "27-Jan-66"];
	var hiredate = ["01-May-92", "14-Aug-92", "01-Apr-92", "03-May-93", "17-Oct-93", "17-Oct-93", "02-Jan-94", "05-Mar-94", "15-Nov-94"];
	var address = ["507 - 20th Ave. E. Apt. 2A", "908 W. Capital Way", "722 Moss Bay Blvd.", "4110 Old Redmond Rd.", "14 Garrett Hill", "Coventry House", "Miner Rd.", "Edgeham Hollow", "Winchester Way", "4726 - 11th Ave. N.E.", "7 Houndstooth Rd."];
	var city = ["Seattle", "Tacoma", "Kirkland", "Redmond", "London", "London", "London", "Seattle", "London"];
	var postalcode = ["98122", "98401", "98033", "98052", "SW1 8JR", "EC2 7JR", "RG1 9SP", "98105", "WG2 7LT"];
	var country = ["USA", "USA", "USA", "USA", "UK", "UK", "UK", "USA", "UK"];
	var homephone = ["(206) 555-9857", "(206) 555-9482", "(206) 555-3412", "(206) 555-8122", "(71) 555-4848", "(71) 555-7773", "(71) 555-5598", "(206) 555-1189", "(71) 555-4444"];
	var notes = ["Education includes a BA in psychology from Colorado State University in 1970.  She also completed 'The Art of the Cold Call.'  Nancy is a member of Toastmasters International.",
		"Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing from the University of Dallas in 1981.  He is fluent in French and Italian and reads German.  He joined the company as a sales representative, was promoted to sales manager in January 1992 and to vice president of sales in March 1993.  Andrew is a member of the Sales Management Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association.",
		"Janet has a BS degree in chemistry from Boston College (1984).  She has also completed a certificate program in food retailing management.  Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992.",
		"Margaret holds a BA in English literature from Concordia College (1958) and an MA from the American Institute of Culinary Arts (1966).  She was assigned to the London office temporarily from July through November 1992.",
		"Steven Buchanan graduated from St. Andrews University, Scotland, with a BSC degree in 1976.  Upon joining the company as a sales representative in 1992, he spent 6 months in an orientation program at the Seattle office and then returned to his permanent post in London.  He was promoted to sales manager in March 1993.  Mr. Buchanan has completed the courses 'Successful Telemarketing' and 'International Sales Management.'  He is fluent in French.",
		"Michael is a graduate of Sussex University (MA, economics, 1983) and the University of California at Los Angeles (MBA, marketing, 1986).  He has also taken the courses 'Multi-Cultural Selling' and 'Time Management for the Sales Professional.'  He is fluent in Japanese and can read and write French, Portuguese, and Spanish.",
		"Robert King served in the Peace Corps and traveled extensively before completing his degree in English at the University of Michigan in 1992, the year he joined the company.  After completing a course entitled 'Selling in Europe,' he was transferred to the London office in March 1993.",
		"Laura received a BA in psychology from the University of Washington.  She has also completed a course in business French.  She reads and writes French.",
		"Anne has a BA degree in English from St. Lawrence College.  She is fluent in French and German."];
	var k = 0;
	for (var i = 0; i < firstNames.length; i++) {
		var row = {};
		row["firstname"] = firstNames[k];
		row["lastname"] = lastNames[k];
		row["title"] = titles[k];
		row["titleofcourtesy"] = titleofcourtesy[k];
		row["birthdate"] = birthdate[k];
		row["hiredate"] = hiredate[k];
		row["address"] = address[k];
		row["city"] = city[k];
		row["postalcode"] = postalcode[k];
		row["country"] = country[k];
		row["homephone"] = homephone[k];
		row["notes"] = notes[k];
		data[i] = row;
		k++;
	}
	var source =
	{
		localdata: data,
		datatype: "array"
	};
	var dataAdapter = new $.jqx.dataAdapter(source);
	var updatePanel = function (index) {
		var container = $('<div style="margin: 5px;"></div>')
		var leftcolumn = $('<div style="float: left; width: 45%;"></div>');
		var rightcolumn = $('<div style="float: left; width: 40%;"></div>');
		container.append(leftcolumn);
		container.append(rightcolumn);
		var datarecord = data[index];
		var firstname = "<div style='margin: 10px;'><b>First Name:</b> " + datarecord.firstname + "</div>";
		var lastname = "<div style='margin: 10px;'><b>Last Name:</b> " + datarecord.lastname + "</div>";
		var title = "<div style='margin: 10px;'><b>Title:</b> " + datarecord.title + "</div>";
		var address = "<div style='margin: 10px;'><b>Address:</b> " + datarecord.address + "</div>";
		$(leftcolumn).append(firstname);
		$(leftcolumn).append(lastname);
		$(leftcolumn).append(title);
		$(leftcolumn).append(address);
		var postalcode = "<div style='margin: 10px;'><b>Postal Code:</b> " + datarecord.postalcode + "</div>";
		var city = "<div style='margin: 10px;'><b>City:</b> " + datarecord.city + "</div>";
		var phone = "<div style='margin: 10px;'><b>Phone:</b> " + datarecord.homephone + "</div>";
		var hiredate = "<div style='margin: 10px;'><b>Hire Date:</b> " + datarecord.hiredate + "</div>";
		$(rightcolumn).append(postalcode);
		$(rightcolumn).append(city);
		$(rightcolumn).append(phone);
		$(rightcolumn).append(hiredate);
		var education = "<div style='clear: both; margin: 10px;'><div><b>Education</b></div><div>" + $('#listbox').jqxListBox('getItem', index).value + "</div></div>";
		container.append(education);
		$("#ContentPanel").html(container.html());
	}
	$('#listbox').on('select', function (event) {
		updatePanel(event.args.index);
	});

	// Create jqxListBox
	$('#listbox').jqxListBox({
		selectedIndex: 0, source: dataAdapter, displayMember: "firstname", valueMember: "notes", itemHeight: 70, height: '100%', width: '100%',
		renderer: function (index, label, value) {
			var datarecord = data[index];
			var imgurl = 'https://cdn4.iconfinder.com/data/icons/dragon/128/User.png';
			var img = '<img height="50" width="40" src="' + imgurl + '"/>';
			var table = '<table style="min-width: 130px;"><tr><td style="width: 40px;" rowspan="2">' + img + '</td><td>' + datarecord.firstname + " " + datarecord.lastname + '</td></tr><tr><td>' + datarecord.title + '</td></tr></table>';
			return table;
		}
	});
	updatePanel(0);
}

function authorClick(authorId) {
	currentSongLink = "";

	console.log(authorId);
	var authorItems = $('#autorsContent a.collection-item');
	for (var i = 0; i < authorItems.length; i++) {
		$(authorItems[i]).removeClass('brown darken-1 white-text');
	}
	$('#author_' + authorId).addClass('brown darken-1 white-text');
	getAuthorSongs(authorId);

	$('#tabsHolder').html("");
	$('#tutorialsHolder').html("");
	$('#coversHolder').html("");
	previousSongId = -1;
}

function getAuthorSongs(authorId) {
	$.ajax({
		url: appName + '/Data/GetAuthorSongs',
		type: 'POST',
		contentType: 'application/json; charset=utf-8',
		dataType: 'json',
		data: JSON.stringify({ authorId: authorId }),
		success: function (msg) {
			refreshSongs(msg);
		}
	});
}

function refreshSongs(songList) {
	var innerHtml = "";
	internetLinks = {};
	for (var i = 0; i < songList.length; i++) {
		innerHtml += '<a href="#!" onclick="onSongClick(' + songList[i].songID + ')" class="collection-item black-text text-accent-4" id=song_' + songList[i].songID + '>' + songList[i].songName + "</a>";
		internetLinks[songList[i].songID] = { 
			youtube: songList[i].youtubeLink, 
			InternetLinks: songList[i].InternetLinks};
	}
	$('#songsHolder').html(innerHtml);
}

function onSongClick(songId) {
	currentSongLink = internetLinks[songId].youtube;
	if (previousSongId == songId) {
		window.open(currentSongLink, '_blank');
	}
	previousSongId = songId;

	var tabsInnerHtml = "";
	var tutorialsInnerHtml = "";
	var coversInnerHtml = "";
	for (var i = 0; i < internetLinks[songId].InternetLinks.length; i++) {
		if (internetLinks[songId].InternetLinks[i].isTab) {
			tabsInnerHtml += '<a href="#!" onclick="onLinkClick(' + songId + ',' + i + ')" class="collection-item" id=link_' + internetLinks[songId].InternetLinks[i].linkID + '>' + internetLinks[songId].InternetLinks[i].linkName + "</a>";
		}
		else if (internetLinks[songId].InternetLinks[i].isTutorial) {
			tutorialsInnerHtml += '<a href="#!" onclick="onLinkClick(' + songId + ',' + i + ')" class="collection-item" id=link_' + internetLinks[songId].InternetLinks[i].linkID + '>' + internetLinks[songId].InternetLinks[i].linkName + "</a>";
		}
		else {
			coversInnerHtml += '<a href="#!" onclick="onLinkClick(' + songId + ',' + i + ')" class="collection-item" id=link_' + internetLinks[songId].InternetLinks[i].linkID + '>' + internetLinks[songId].InternetLinks[i].linkName + "</a>";
		}
	}
	$('#tabsHolder').html(tabsInnerHtml);
	$('#tutorialsHolder').html(tutorialsInnerHtml);
	$('#coversHolder').html(coversInnerHtml);

	var songsItems = $('#songsContent a.collection-item');
	for (var i = 0; i < songsItems.length; i++) {
		$(songsItems[i]).removeClass('black darken-1 white-text');
	}
	$('#song_' + songId).addClass('black darken-1 white-text');

}

function onLinkClick(songId, index) {
	window.open(internetLinks[songId].InternetLinks[index].linkAdress, '_blank');
}

$(document).ready(function () {
	$('.btn-youtube').click(function () {
		if (currentSongLink != "") {
			window.open(currentSongLink, '_blank');
		}
	});
	
});