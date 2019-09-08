import requests


api_token = '79d88e13c14c98124eedf34afe5b645350ebe53a'
#api_token = '9b44b09199c61bcf9416ad846dd0e4'
headers = {'Authorization': 'Token %s' % api_token}

response = requests.get('https://sandboxapi.b2c2.net/balance/', headers=headers)

print(response)

