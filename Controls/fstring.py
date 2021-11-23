# -*- coding: utf-8 -*-
"""Python2 f-string like behavior"""
from __future__ import print_function
import inspect
import re


class F(object):
    """String formatter based on Python 3.6 'f' strings
    `F` will automatically format anything between two
    braces (ie: {{ ... }}) when printed. The original
    representation of the string is kept as well and
    printed with `print(repr(f_string))`.
    There is also a stand alone method which takes a
    `regex` and a `string` for input and returns the
    string with all pattern matches replaced.
    Attributes:
        _string: the string to be formatted
        text: the newly formatted string
    """
    _regex = re.compile("\{\{([^}]+)\}\}", re.S)

    def __init__(self, s, regex=None):
        """Init `F` with string `s`"""
        self.regex = regex or self._regex
        self._string = s
        self.f_locals = self.original_caller.f_locals
        self.f_globals = self.original_caller.f_globals
        self.text = self._find_and_replace(s)

    @property
    def original_caller(self):
        names = []
        frames = []
        frame = inspect.currentframe()
        while True:
            try:
                frame = frame.f_back
                name = frame.f_code.co_name
                names.append(name)
                frames.append(frame)
            except:
                break
        return frames[-2]

    def _find_and_replace(self, s):
        """Evaluates and returns all occurrences of `regex` in `s`"""
        return re.sub(self._regex, self._clean_and_eval, s)

    def _clean_and_eval(self, m):
        """Remove surrounding braces and whitespace from regex match `m`,
            evaluate, and return the result as a string.
        """
        replaced = m.group()[2:][:-2].strip()
        try:
            result = str(eval(replaced))
            print(result)
            return result
        except (TypeError, NameError, SyntaxError):
            try:
                result = str(eval(replaced, self.f_locals, self.f_globals))
                return result
            except (TypeError, NameError, SyntaxError):
                raise ValueError("Can't find replacement for {{ %s }}, sorry." % replaced)

    def __str__(self):
        return str(self.text)

    def __repr__(self):
        return str(self._string)


def demo(lines):
    def print_slow(line):
        text_on_screen = ">>> "
        for c in line:
            time.sleep(0.2 * random.random())
            text_on_screen += str(c)
            sys.stdout.write("\r{} ".format(text_on_screen))
            sys.stdout.flush()
        sys.stdout.write("\n")

    for i in lines:
        print_slow(i)
        exec(i, globals(), locals())
        time.sleep(0.8 * random.random())


if __name__ == '__main__':
    """Run this file as a script to see a demo!"""
    import random
    import collections
    User = collections.namedtuple("User", "name")
    guide = """
user = User(name="traBpUkciP")
print("User name:", user.name)
quality = ["lack of faith", "hairstyle", "table manners", "sweatpants"]
insult = ["disturbing", "a mess", "like a hamster", "rude"]
print(F("I find your {{ random.choice(quality) }} {{ random.choice(insult) }}.."))
which = ["original", "knockoff"]
thing = "copy"
f_string = F("You can always access the {{ which[0] }} {{ thing }} with repr.")
print(repr(f_string))
print("The which thing with the what??")
print(f_string)
print("OH!")
"""
    import sys
    import time
    lines = [i for i in guide.splitlines()]
    lines = [i for i in lines if len(i) and not i.startswith("#")]

    demo(lines)
