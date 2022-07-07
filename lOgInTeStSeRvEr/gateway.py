from nameko.web.handlers import http
from werkzeug.wrappers import Request, Response
import json
from urllib.parse import unquote

user_pass = {f'player{x}@sexy.com': f'player{x}' for x in range(10)}

user_team = {f'player{x}@sexy.com': x//2 for x in range(10)}

print(*user_team.items(), sep='\n')
print()
print(*user_pass.items(), sep='\n')


def generate_return_json(success, message, id_team=None):
    if id_team:
        return json.dumps({'success': success, 'message': message, 'id_team': id_team})
    return json.dumps({'success': success, 'message': message})


class GatewayService:
    name = "gateway_service"

    @http('POST', "/login")
    def login(self, request):
        print(unquote(request.get_data()))
        data = json.loads(unquote(request.get_data()))
        if data['email'] not in user_pass:
            return Response(status=401, mimetype='application/json', response=generate_return_json(False, 'User not found'))

        if data['password'] != user_pass[data['email']]:
            return Response(status=401, mimetype='application/json', response=generate_return_json(False, 'Wrong password'))

        return Response(status=200, mimetype='application/json', response=generate_return_json(True, 'Login successful', user_team[data['email']]))
