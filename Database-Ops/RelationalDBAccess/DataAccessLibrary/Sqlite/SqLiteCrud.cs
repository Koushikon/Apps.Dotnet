using DataAccessLibrary.Models;

namespace DataAccessLibrary.Sqlite;

public class SqLiteCrud
{
    private readonly string _connectionString;
    private readonly SqLiteDataAccess _db = new();

    public SqLiteCrud(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<BasicContact> GetAllContacts()
    {
        const string sqlCommand = "SELECT Id, FirstName, LastName FROM Contact";
        return _db.LoadData<BasicContact, dynamic>(sqlCommand, new { }, _connectionString);
    }

    public FullContact GetFullContactsById(int id)
    {
        if (id == 0) return new FullContact();

        FullContact output = new();

        const string sqlCommand1 = "SELECT Id, FirstName, LastName FROM Contact WHERE Id = @Id";
        output.BasicInfo = _db.LoadData<BasicContact, dynamic>(sqlCommand1, new { Id = id }, _connectionString).First();

        if (output.BasicInfo == null) return new FullContact();

        const string sqlCommand2 = @"SELECT E.Id, E.EmailAddress
                FROM Contact C
	                INNER JOIN ContactEmail CE ON CE.[ContactId] = C.Id
	                INNER JOIN Email E ON E.Id = CE.EmailId
                WHERE CE.[ContactId] = @Id";
        output.EmailInfo = _db.LoadData<Email, dynamic>(sqlCommand2, new { Id = id }, _connectionString);

        const string sqlCommand3 = @"SELECT P.Id, P.PhoneNumber
                FROM Contact C
	                INNER JOIN ContactPhone CP ON CP.[ContactId] = C.Id
	                INNER JOIN Phone P ON P.Id = CP.PhoneId
                WHERE CP.[ContactId] = @Id";
        output.PhoneInfo = _db.LoadData<Phone, dynamic>(sqlCommand3, new { Id = id }, _connectionString);

        return output;
    }

    public int CreateContact(FullContact contact)
    {
        // Saving Basic Contact
        string sqlCommandC = @"INSERT INTO [Contact]
                ([FirstName], [LastName])
                VALUES (@FirstName, @LastName);
				SELECT last_insert_rowid()";
        var contactId = _db.SaveDataWithReurnId<int, dynamic>(sqlCommandC, new
        {
            contact.BasicInfo.FirstName,
            contact.BasicInfo.LastName,
        },
        _connectionString);


        // Saving Contact Email
        foreach (var item in contact.EmailInfo)
        {
            sqlCommandC = @"SELECT E.Id
                    FROM Contact C
	                    INNER JOIN ContactEmail CE ON CE.[ContactId] = C.Id
	                    INNER JOIN Email E ON E.Id = CE.EmailId
                    WHERE E.EmailAddress = @EmailAddress";
            var lookupData = _db.LoadData<IdLookup, dynamic>(sqlCommandC, new { item.EmailAddress }, _connectionString).FirstOrDefault();

            if (lookupData != null) item.Id = lookupData.Id;

            if (item.Id == 0)
            {
                sqlCommandC = @"INSERT INTO [Email]
                        ([EmailAddress])
                        VALUES (@EmailAddress);
						SELECT last_insert_rowid()";
                item.Id = _db.SaveDataWithReurnId<int, dynamic>(sqlCommandC, new { item.EmailAddress }, _connectionString);
            }

            sqlCommandC = @"INSERT INTO [ContactEmail]
                    ([ContactId], [EmailId])
                    VALUES (@ContactId, @EmailId);
					SELECT last_insert_rowid()";
            _db.SaveData<dynamic>(sqlCommandC, new
            {
                ContactId = contactId,
                EmailId = item.Id,
            },
            _connectionString);
        }


        // Saving Contact Phone
        foreach (var item in contact.PhoneInfo)
        {
            sqlCommandC = @"SELECT P.Id
                    FROM Contact C
	                    INNER JOIN ContactPhone CP ON CP.[ContactId] = C.Id
	                    INNER JOIN Phone P ON P.Id = CP.PhoneId
                    WHERE P.PhoneNumber = @PhoneNumber";
            var lookupData = _db.LoadData<IdLookup, dynamic>(sqlCommandC, new { item.PhoneNumber }, _connectionString).FirstOrDefault();

            if (lookupData != null) item.Id = lookupData.Id;

            if (item.Id == 0)
            {
                sqlCommandC = @"INSERT INTO [Phone]
                        ([PhoneNumber])
                        VALUES (@PhoneNumber);
						SELECT last_insert_rowid()";
                item.Id = _db.SaveDataWithReurnId<int, dynamic>(sqlCommandC, new { item.PhoneNumber }, _connectionString);
            }

            sqlCommandC = @"INSERT INTO [ContactPhone]
                    ([ContactId], [PhoneId])
                    VALUES (@ContactId, @PhoneId);
					SELECT last_insert_rowid()";
            _db.SaveData<dynamic>(sqlCommandC, new
            {
                ContactId = contactId,
                PhoneId = item.Id,
            },
            _connectionString);
        }

        return contactId;
    }

    public int UpdateContactName(BasicContact contact)
    {
        const string sqlCommandC = @"UPDATE [Contact]
                SET [FirstName] = @FirstName,
	                [LastName] = @LastName
                WHERE Id = @Id;
				SELECT @Id";

        return _db.SaveDataWithReurnId<int, dynamic>(sqlCommandC, new
        {
            contact.FirstName,
            contact.LastName,
            contact.Id
        },
        _connectionString);
    }

    public void RemovePhoneNumber(int contactId, int phoneId)
    {
        string sqlCommandC = @"SELECT CP.Id
                FROM ContactPhone CP
	                INNER JOIN Phone P ON P.Id = CP.PhoneId
                WHERE P.[Id] = @Id";
        var findlinkedContact = _db.LoadData<IdLookup, dynamic>(sqlCommandC, new { Id = phoneId }, _connectionString);

        if (findlinkedContact?.Count > 0)
        {
            sqlCommandC = @"DELETE FROM ContactPhone
                WHERE ContactId = @ContactId AND PhoneId = @PhoneId";

            _db.SaveData<dynamic>(sqlCommandC, new
            {
                ContactId = contactId,
                PhoneId = phoneId
            }, _connectionString);
        }

        if (findlinkedContact?.Count == 1)
        {
            sqlCommandC = @"DELETE FROM Phone
                WHERE Id = @PhoneId";

            _db.SaveData<dynamic>(sqlCommandC, new
            {
                PhoneId = phoneId
            }, _connectionString);
        }
    }
}