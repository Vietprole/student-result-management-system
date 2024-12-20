import { Alert, FlatList, ScrollView, StyleSheet, Text, TouchableOpacity, View } from 'react-native';

type TableRow = {
  TT: string;
  MonHoc: string;
  BT: string;
  CK: string;
  GK: string;
  QT: string;
  TongKet: string;
};

const App = () => {
  const tableHeaders = ['TT', 'Môn học', 'BT', 'CK', 'GK', 'QT', 'Tổng kết', 'Xem thêm'];
  const tableData: TableRow[] = [
    { TT: '1', MonHoc: 'PBL6', BT: '', CK: '9', GK: '9', QT: '8', TongKet: '8.5' },
    { TT: '2', MonHoc: 'Kiểm thử phần mềm', BT: '8', CK: '7', GK: '8', QT: '9', TongKet: '8.0' },
  ];

  const handleViewMore = (row: TableRow) => {
    Alert.alert('Xem thêm', `Details for ${row.MonHoc}`);
  };

  const getColumnWidth = (text: string) => {
    const baseWidth = 100;
    const additionalWidth = Math.max(text.length * 5, 70);
    return baseWidth + additionalWidth;
  };

  const renderHeader = () => (
    <View style={[styles.row, styles.headerRow]}>
      {tableHeaders.map((header, index) => (
        <Text key={index} style={[styles.cell, styles.headerCell, { minWidth: getColumnWidth(header) }]}>
          {header}
        </Text>
      ))}
    </View>
  );

  const renderRow = ({ item, index }: { item: TableRow; index: number }) => (
    <View style={[styles.row, index % 2 === 0 ? styles.evenRow : styles.oddRow]}>
      <Text style={[styles.cell, { minWidth: getColumnWidth(item.TT) }]}>{item.TT}</Text>
      <Text style={[styles.cell, { minWidth: getColumnWidth(item.MonHoc) }]}>{item.MonHoc}</Text>
      <Text style={[styles.cell, { minWidth: getColumnWidth(item.BT) }]}>{item.BT}</Text>
      <Text style={[styles.cell, { minWidth: getColumnWidth(item.CK) }]}>{item.CK}</Text>
      <Text style={[styles.cell, { minWidth: getColumnWidth(item.GK) }]}>{item.GK}</Text>
      <Text style={[styles.cell, { minWidth: getColumnWidth(item.QT) }]}>{item.QT}</Text>
      <Text style={[styles.cell, { minWidth: getColumnWidth(item.TongKet) }]}>{item.TongKet}</Text>
      <TouchableOpacity style={[styles.cell, { minWidth: getColumnWidth('Xem thêm') }]} onPress={() => handleViewMore(item)}>
        <Text style={styles.link}>Chi tiết</Text>
      </TouchableOpacity>
    </View>
  );

  return (
    <ScrollView horizontal>
      <View style={styles.table}>
        {renderHeader()}
        <FlatList
          data={tableData}
          renderItem={renderRow}
          keyExtractor={(item, index) => index.toString()}
        />
      </View>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  table: {
    borderWidth: 1,
    borderColor: '#ccc',
    marginHorizontal: 16,
  },
  row: {
    flexDirection: 'row',
  },
  cell: {
    flex: 1,
    padding: 10,
    textAlign: 'center',
    borderWidth: 1,
    borderColor: '#ccc',
    minWidth: 100,
    alignSelf: 'auto',
  },
  headerRow: {
    backgroundColor: '#f1f1f1',
  },
  headerCell: {
    fontWeight: 'bold',
    fontSize: 14,
  },
  evenRow: {
    backgroundColor: '#f9f9f9',
  },
  oddRow: {
    backgroundColor: '#ffffff',
  },
  link: {
    color: 'blue',
    textDecorationLine: 'underline',
  },
});

export default App;