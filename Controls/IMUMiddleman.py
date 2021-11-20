import socket

serv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
serv.bind(('0.0.0.0', 8080))
serv.listen(5)
sender = socket.socket()
sender.connect(('172.18.144.1', 8081))

while True:
    conn, addr = serv.accept()
    from_client = ''
    while True:
        data = conn.recv(4096)
        if not data: break
        sender.send(data)
        #client_buffer = from_client.split(';')
        #for item in client_buffer:
        #    if item != "":
        #        print(item)
        #        sender.send(item.encode())
        #print(from_client)
        #conn.send('I am SERVER'.encode())
    conn.close()
    print('client disconnected')