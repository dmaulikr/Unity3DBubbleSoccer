from rest_framework import generics
from django.contrib.auth.models import User
from game.serializers import UserSerializer, InfoSerializer
from rest_framework import status
from rest_framework import permissions
from permissions import IsOwner
from rest_framework.decorators import api_view, permission_classes
from rest_framework.permissions import IsAuthenticated, IsAdminUser
from rest_framework.response import Response
from models import Info


# generate all the user in list and return the request in json
# permissions only superuser
class UserList(generics.ListAPIView):
    permission_classes = (permissions.IsAdminUser,)
    queryset = User.objects.all()
    serializer_class = UserSerializer


# generate the user detail by pk in list and return the request in json
# permissions superuser or owner
class UserDetail(generics.RetrieveAPIView):
    permission_classes = (IsAdminUser,
                          IsOwner,)
    queryset = User.objects.all()
    serializer_class = UserSerializer


# user register POST handler, return HTTP/1.0 201 Created for successful Request
# For the username is created or invalid input return HTTP/1.0 400 Bad Request
# permissions: anyone
@api_view(['POST'])
def register(request, format=None):
    serialized = UserSerializer(data=request.data)
    if serialized.is_valid():
        user = User.objects.create_user(
            serialized.data['username'],
            '',
            serialized.data['password']
        )
        i = Info(user=user)
        i.save()
        return Response(serialized.data, status=status.HTTP_201_CREATED)
    else:
        return Response(serialized.errors, status=status.HTTP_400_BAD_REQUEST)


# user login/update/delete GET/PUT/DELETE handler, return HTTP/1.0 200 for successful Request
# return 403 Forbidden for authentication fail
# permissions: authenticated user
@api_view(['GET', 'PUT', 'DELETE'])
@permission_classes((IsAuthenticated, ))
# return 403 Forbidden if is authenticated.
def userhandler(request, username, format=None):
    """
    Retrieve, update or delete a snippet instance.
    """
    try:
        user = User.objects.get(username=username)
    except User.DoesNotExist:
        return Response(status=status.HTTP_404_NOT_FOUND)

    if request.method == 'GET':
        serializer = UserSerializer(user)
        return Response(serializer.data)

    elif request.method == 'PUT':
        serializer = UserSerializer(user, data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)

    elif request.method == 'DELETE':
        user.delete()
        return Response(status=status.HTTP_204_NO_CONTENT)


# user rank GET/POST handler, return HTTP/1.0 200 for successful Request
# return 403 Forbidden for authentication fail
# permissions: authenticated user
@api_view(['GET', 'PUT'])
@permission_classes((IsAuthenticated, ))
# return 403 Forbidden if is authenticated.
def rank(request, username, format=None):
    """
    get or update the player's rank.
    :param request: request
    :param username: username
    :param format: accepted format
    :return: http response, http 200 for a successful request.
    """
    try:
        user = User.objects.get(username=username)
        info = Info.objects.get(user=user)
    except (User.DoesNotExist, Info.DoesNotExist):
        return Response(status=status.HTTP_404_NOT_FOUND)

    if request.method == 'GET':
        serializer = InfoSerializer(info)
        return Response(serializer.data)

    if request.method == 'PUT':
        serializer = InfoSerializer(info, data=request.data)
        if serializer.is_valid():
            serializer.save()
            return Response(serializer.data)
        return Response(serializer.errors, status=status.HTTP_400_BAD_REQUEST)


