set -x
git add .
#git commit -c user.name='Jesus Picornell' -c user.email='jesus.picornell@gmail.com' `date +%y%m%d%H%M%S`
git commit -m `date +%y%m%d%H%M%S`
git push -u origin master
