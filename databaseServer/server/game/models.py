from __future__ import unicode_literals

from django.db import models
from django.contrib.auth.models import User


class Info(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE)
    rank = models.CharField(max_length=10000, default=1)