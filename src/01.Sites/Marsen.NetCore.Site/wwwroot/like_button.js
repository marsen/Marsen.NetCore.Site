function formatName(user) {
  return user.lastName + ' ' + user.firstName;
}

var user = {
  firstName: 'QQ',
  lastName: 'XX'
};

var element = React.createElement(
  'h1',
  null,
  '\u4F60\u597D, ',
  formatName(user),
  '!'
);

ReactDOM.render(element, document.getElementById('root'));