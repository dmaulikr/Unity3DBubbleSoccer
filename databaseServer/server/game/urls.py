from django.conf.urls import url
from rest_framework.urlpatterns import format_suffix_patterns
from game import views

"""server URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/1.9/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  url(r'^$', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  url(r'^$', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.conf.urls import url, include
    2. Add a URL to urlpatterns:  url(r'^blog/', include('blog.urls'))
"""

urlpatterns = [
    url(r'^users/$', views.UserList.as_view()),
    url(r'^users/(?P<pk>[0-9]+)/$', views.UserDetail.as_view()),
    url(r'^users/register/$', views.register),
    url(r'^users/(?P<username>[0-9A-z]+)/$', views.userhandler),
    url(r'^users/rank/(?P<username>[0-9A-z]+)/$', views.rank),

]

urlpatterns = format_suffix_patterns(urlpatterns)