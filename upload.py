import requests

url = 'http://localhost:5000/api/GuideUpdate'

xmldata = ''
with open('guide.xml', 'r', encoding='utf8') as file:
    xmldata = file.read()

response = requests.post(url, xmldata.encode('utf8'))
print(response)