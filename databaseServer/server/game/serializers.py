from rest_framework import serializers
from django.contrib.auth.models import User
from .models import Info

"""
    Serializers allow complex data such as querysets and model instances to be converted to
    native Python datatypes that can then be easily rendered into JSON, XML or other content
    types. Serializers also provide deserialization, allowing parsed data to be converted
    back into complex types, after first validating the incoming data.

    api-guide: http://www.django-rest-framework.org/api-guide/serializers/
    by Django-rest-framework
"""


class UserSerializer(serializers.ModelSerializer):

    class Meta:
        model = User
        fields = ('pk', 'password', 'username',)


class InfoSerializer(serializers.ModelSerializer):

    class Meta:
        model = Info
        fields = ('rank',)