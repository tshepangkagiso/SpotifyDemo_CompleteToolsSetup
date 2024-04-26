const request = require('request');

var client_id = '23926cc71fc641e7a81d0009d636ac33';
var client_secret = '2d089efff43443129ca83abe97569e59';

var authOptions = {
  url: 'https://accounts.spotify.com/api/token',
  headers: {
    'Authorization': 'Basic ' + (new Buffer.from(client_id + ':' + client_secret).toString('base64'))
  },
  form: {
    grant_type: 'client_credentials'
  },
  json: true
};

request.post(authOptions, function(error, response, body) {
  if (!error && response.statusCode === 200) {
    var token = body.access_token;
    console.log(token);
  }
});

