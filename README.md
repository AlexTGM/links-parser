# links-parser

To run the app you should to restore nuget packages and start the server. As default it starts on 5000 port.
To test the app you should send POST request (POST: http://localhost:5000) with the following body:

`{
  "url: "url you want to parse",
  "maxDepth": "how much pages you want to parse",
  "ContentValidationRules": "rules which depends on content response headers",
	"ResponseValidationRules": "rules which depends on response headers",
	"parsingRules": "parsing rules"
}`

At this moment you can use:

ContentValidationRules:
`ContentValidationLengthRule:min,max` - this rule will check the content lenght and filter sites which are out of bounds. (You can set the min value without max if you want to)

ResponseValidationRules:
`ServerValidationRule:server` - this rule will check if the server which runs the site is the same which you are expect (nginx, iis or so on)

parsingRules:
`tags:tag1,tag2,...` - the list of tags you want to parse (at this moment only `a` and `img` are supported) `tags:a,img`,
`exclude:word1,word2,word3` - the list of words you don't want to include in links (exclude:com,jpg)
`include:word1,word2,word3` - the list of words you do want to include in links (include:promoo,content)

to combine the rules sets you should join them with `;`: `"parsingRules": "tags:a,img;exclude:http://"

*Example*

`{
	"url": "https://nytimes.com",
	"ContentValidationRules": "ContentValidationLengthRule:100,100000000",
	"ResponseValidationRules": "ServerValidationRule:nginx",
	"maxDepth": 1,
	"parsingRules": "tags:img;exclude:jpg;include:opinion"
}`

this will return the list of non-jpg images from http://nytimes.com

`{
    "page": "https://nytimes.com/",
    "links": [
        "https://static01.nyt.com/images/2018/04/02/opinion/charles-m-blow/charles-m-blow-thumbLarge.png?quality=75&auto=webp&disable=upscale",
        "https://static01.nyt.com/images/2015/03/16/opinion/Tufekci-Zeynep-circular/Tufekci-Zeynep-circular-thumbLarge-v3.png?quality=75&auto=webp&disable=upscale",
        "https://static01.nyt.com/images/2017/08/15/opinion/bryce-covert/bryce-covert-thumbLarge-v2.png?quality=75&auto=webp&disable=upscale",
        "https://static01.nyt.com/images/2018/07/12/opinion/maeve-higgins/maeve-higgins-thumbLarge.png?quality=75&auto=webp&disable=upscale"
    ],
    "pages": null
}`
